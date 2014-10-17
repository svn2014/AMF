using System;
using System.Data;
using Security;
using System.Diagnostics;

namespace ArbitrageMonitor
{
    class ArbiModelSOF: ArbiModel
    {
        public ArbiModelSOF()
        {
            this.initializeTable();

            FundSelector fs = new FundSelector();
            _FundGroup = fs.GetSOF();
            _FundGroup.LoadData(DataInfoType.SecurityInfo);
        }

        protected override void initializeTable()
        {
            base.initializeTable();
            
            _ViewData.Columns.Add(C_ColName_CodeA, typeof(string));
            _ViewData.Columns.Add(C_ColName_CodeB, typeof(string));
            _ViewData.Columns.Add(C_ColName_ShareARatio, typeof(double));

            _ViewData.Columns.Add(C_ColName_PriceA, typeof(double));
            _ViewData.Columns.Add(C_ColName_DiscA, typeof(double));
            _ViewData.Columns.Add(C_ColName_PremA, typeof(double));
            _ViewData.Columns.Add(C_ColName_AmountA, typeof(double));
            _ViewData.Columns.Add(C_ColName_PriceB, typeof(double));
            _ViewData.Columns.Add(C_ColName_DiscB, typeof(double));
            _ViewData.Columns.Add(C_ColName_PremB, typeof(double));
            _ViewData.Columns.Add(C_ColName_AmountB, typeof(double));
            _ViewData.Columns.Add(C_ColName_NAV, typeof(double));
            _ViewData.Columns.Add(C_ColName_AmountAB, typeof(double));
        }

        public override DataTable GetData()
        {
            try
            {
                _FundGroup.LoadData(DataInfoType.RealtimeTradePrice);
                _FundGroup.LoadData(DataInfoType.RealtimeFundNAV);
                _ViewData.Clear();

                _FundHedge.LoadData(DataInfoType.RealtimeTradePrice);

                for (int i = 0; i < _FundGroup.SecurityList.Count; i++)
                {
                    StructuredFund sf = (StructuredFund)_FundGroup.SecurityList[i];

                    //不能配对转换的去掉
                    if (!sf.IsArbitrageEnabled)
                        continue;

                    //不符合设置要求的去掉
                    if (!this._TradeOption.ShowActiveBondFunds 
                        && sf.Category.AssetCategory == FundAssetCategory.Bond 
                        && sf.Category.InvestmentCategory == FundInvestmentCategory.Active)
                        continue;
                    if (!this._TradeOption.ShowActiveEquityFunds
                        && sf.Category.AssetCategory == FundAssetCategory.Equity
                        && sf.Category.InvestmentCategory == FundInvestmentCategory.Active)
                        continue;
                    if (!this._TradeOption.ShowPassiveBondFunds
                        && sf.Category.AssetCategory == FundAssetCategory.Bond
                        && sf.Category.InvestmentCategory == FundInvestmentCategory.Passive)
                        continue;
                    if (!this._TradeOption.ShowPassiveEquityFunds
                        && sf.Category.AssetCategory == FundAssetCategory.Equity
                        && sf.Category.InvestmentCategory == FundInvestmentCategory.Passive)
                        continue;
                    if (!this._TradeOption.ShowActiveHybridFunds
                        && sf.Category.AssetCategory == FundAssetCategory.Hybrid
                        && sf.Category.InvestmentCategory == FundInvestmentCategory.Active)
                        continue;

                    if (!this._TradeOption.ShowOthers
                        && ( sf.Category.AssetCategory  == FundAssetCategory.QDII
                            || sf.Category.AssetCategory  == FundAssetCategory.Monetory
                            || sf.Category.AssetCategory  == FundAssetCategory.Other
                            || sf.Category.AssetCategory  == FundAssetCategory.Undefined
                            )
                        )
                        continue;

                    DataRow row = _ViewData.NewRow();
                    row[C_ColName_Name] = sf.Name;
                    row[C_ColName_Code] = sf.Code;
                    row[C_ColName_FundType] = sf.Category.Name3;
                    row[C_ColName_SubscribeRate] = sf.MaxSubscribeRate;
                    row[C_ColName_RedeemRate] = sf.MaxRedeemRate;
                    row[C_ColName_ShareARatio] = sf.ShareARatio;

                    if (sf.FoundedDate > new DateTime(1990, 1, 1))
                        row[C_ColName_FundedDate] = Convert.ToDateTime(sf.FoundedDate);

                    if (sf.Category.AssetCategory == FundAssetCategory.Equity)
                    {
                        if (sf.PrimaryBenchmarkIndex != null)
                        {
                            row[C_ColName_BenchmarkCode] = sf.PrimaryBenchmarkIndex.Code;
                            row[C_ColName_BenchmarkName] = sf.PrimaryBenchmarkIndex.Name;
                        }
                        else
                            row[C_ColName_BenchmarkName] = "**默认指数**";
                    }
                    else
                    {
                        row[C_ColName_BenchmarkCode] = "";
                        row[C_ColName_BenchmarkName] = "";
                    }

                    #region Category
                    string category = "";
                    switch (sf.Category.InvestmentCategory)
                    {
                        case FundInvestmentCategory.Active:
                            category += "主动";
                            break;
                        case FundInvestmentCategory.Passive:
                            category += "被动";
                            break;
                        default:
                            category += "";
                            break;
                    }
                    switch (sf.Category.AssetCategory)
                    {
                        case FundAssetCategory.Equity:
                            category += "-股票";
                            break;
                        case FundAssetCategory.Hybrid:
                            category += "-混合";
                            break;
                        case FundAssetCategory.Bond:
                            category += "-债券";
                            break;
                        default:
                            category += "-其他";
                            break;
                    }
                    row[C_ColName_Category] = category;
                    #endregion

                    #region 实时信息
                    double amountA=-1, amountB=-1;
                    double navA = -1, navB = -1;
                    double pxDiscA = -1, pxDiscB = -1, pxPremA = -1, pxPremB = -1;
                    if (sf.ShareA != null)
                    {
                        row[C_ColName_CodeA] = sf.ShareA.Code;
                        if ((RealtimeTradePrice)sf.ShareA.RealtimeTradePrice != null)
                        {
                            row[C_ColName_PriceA] = ((RealtimeTradePrice)sf.ShareA.RealtimeTradePrice).Close;
                            amountA = ((RealtimeTradePrice)sf.ShareA.RealtimeTradePrice).Amount;
                            pxDiscA = ((RealtimeTradePrice)sf.ShareA.RealtimeTradePrice).BuyPrice1;
                            pxPremA = ((RealtimeTradePrice)sf.ShareA.RealtimeTradePrice).SellPrice1;

                            if((RealtimeNetAssetValue)sf.ShareA.RealtimeNAV != null)
                            {
                                //A份额nav使用昨日收盘净值，不做指数调整
                                navA = ((RealtimeNetAssetValue)sf.ShareA.RealtimeNAV).PreCloseNAV;
                                row[C_ColName_DiscA] = pxDiscA / navA - 1;
                                row[C_ColName_PremA] = pxPremA / navA - 1;
                            }                            
                        }
                    }

                    if (sf.ShareB != null)
                    {
                        row[C_ColName_CodeB] = sf.ShareB.Code;
                        if ((RealtimeTradePrice)sf.ShareB.RealtimeTradePrice != null)
                        {
                            row[C_ColName_PriceB] = ((RealtimeTradePrice)sf.ShareB.RealtimeTradePrice).Close;
                            amountB = ((RealtimeTradePrice)sf.ShareB.RealtimeTradePrice).Amount;
                            pxDiscB = ((RealtimeTradePrice)sf.ShareB.RealtimeTradePrice).BuyPrice1;
                            pxPremB = ((RealtimeTradePrice)sf.ShareB.RealtimeTradePrice).SellPrice1;

                            if ((RealtimeNetAssetValue)sf.ShareB.RealtimeNAV != null)
                            {
                                //A份额nav使用指数调整，估计实时净值
                                navB = sf.ShareB.RealtimeNAV.RealtimeNAV;
                                row[C_ColName_DiscB] = pxDiscB / navB - 1;
                                row[C_ColName_PremB] = pxPremB / navB - 1;
                            }
                        }
                    }

                    if (amountA >= 0 && amountB >= 0)
                    {
                        row[C_ColName_NAV] = sf.RealtimeNAV.RealtimeNAV;
                        row[C_ColName_AmountA] = amountA / 10000;
                        row[C_ColName_AmountB] = amountB / 10000;
                        row[C_ColName_AmountAB] = Math.Min(amountA / sf.ShareARatio, amountB / (1 - sf.ShareARatio)) / 10000;
                    }
                    #endregion

                    double discarbi1 = -1, premarbi1 = -1;
                    #region 套利详细
                    for (int j = 1; j <= 5; j++)
                    {
                        double discarbi = this.calDiscArbi(sf, j);
                        double premarbi = this.calPremArbi(sf, j);
                        double discdept = this.calDiscDept(sf, j);
                        double premdept = this.calPremDept(sf, j);

                        if (discarbi > -100)
                            row[C_ColName_Disc + C_ColName_Arbi + j] = discarbi;    //折价套利
                        if (premarbi > -100)
                            row[C_ColName_Prem + C_ColName_Arbi + j] = premarbi;    //溢价套利
                        if (discdept > -100)
                            row[C_ColName_Disc + C_ColName_Dept + j] = discdept;    //折价套利市场深度
                        if (premdept > -100)
                            row[C_ColName_Prem + C_ColName_Dept + j] = premdept;    //溢价套利市场深度

                        if (j == 1)
                        {
                            discarbi1 = discarbi;
                            premarbi1 = premarbi;
                        }
                    }

                    //排序列
                    row[C_ColName_OrderValue] = Math.Max(discarbi1, premarbi1);
                    #endregion
                    
                    _ViewData.Rows.Add(row);
                }

                return _ViewData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double calDiscArbi(StructuredFund sf, int tickPosition)
        {
            //==========================
            //  买入赎回套利
            //==========================

            //数据问题或者不可交易，无法套利
            if (sf.ShareA == null || sf.ShareB == null
                || sf.ShareA.RealtimeTradePrice == null || sf.ShareB.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "SellPrice" + tickPosition;
            string keynamevol = "SellVolume" + tickPosition;

            //根据卖1-卖5决定买入价
            double buypxA = (double)(typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareA.RealtimeTradePrice));
            double buyvolA = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareA.RealtimeTradePrice);
            double buypxB = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareB.RealtimeTradePrice);
            double buyvolB = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareB.RealtimeTradePrice);

            //涨停买不到，无法套利
            if (buypxA == 0 || buypxB == 0)
                return -999;

            //计算对冲成本
            //  假设对冲比例1:1
            //  仅对股票进行对冲
            double hedgepx = (buypxA * sf.ShareARatio + buypxB * (1 - sf.ShareARatio));
            double hedgecost = 0;            
            if (this._TradeOption.IsHedge && sf.Category.AssetCategory == FundAssetCategory.Equity)
            { 
                //融券交易成本：1买1卖
                hedgecost += hedgepx * this._TradeOption.Commision * 2;
                //融券利息成本：T日在二级市场买入－T+1合并申请－T+2确认－T+2可以赎回－平仓空头头寸
                hedgecost += hedgepx * this._TradeOption.HedgeIntRate / 365 * 2;
            }

            double cost = hedgepx * (1 + this._TradeOption.Commision) + hedgecost;
            double rev = sf.RealtimeNAV.RealtimeNAV * (1 - sf.MaxRedeemRate);
            return rev / cost - 1;
        }
        private double calDiscDept(StructuredFund sf, int tickPosition)
        {
            //==========================
            //  买入赎回套利——市场深度
            //==========================

            //数据问题或者不可交易，无法套利
            if (sf.ShareA == null || sf.ShareB == null
                || sf.ShareA.RealtimeTradePrice == null || sf.ShareB.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "SellPrice" + tickPosition;
            string keynamevol = "SellVolume" + tickPosition;

            double buypxA = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareA.RealtimeTradePrice);
            double buyvolA = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareA.RealtimeTradePrice);
            double buypxB = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareB.RealtimeTradePrice);
            double buyvolB = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareB.RealtimeTradePrice);

            //涨停买不到，无法套利
            if (buypxA == 0 || buypxB == 0)
                return -999;

            //单位：万元
            return Math.Min(buypxA * buyvolA / sf.ShareARatio, buypxB * buyvolB / (1 - sf.ShareARatio)) / 10000;
        }
        private double calPremArbi(StructuredFund sf, int tickPosition)
        {
            //==========================
            //  申购卖出套利
            //==========================

            //数据问题或者不可交易，无法套利
            if (sf.ShareA == null || sf.ShareB == null
                || sf.ShareA.RealtimeTradePrice == null || sf.ShareB.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "BuyPrice" + tickPosition;
            string keynamevol = "BuyVolume" + tickPosition;

            double sellpxA = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareA.RealtimeTradePrice);
            double sellvolA = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareA.RealtimeTradePrice);
            double sellpxB = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareB.RealtimeTradePrice);
            double sellvolB = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareB.RealtimeTradePrice);

            //跌停卖不掉，无法套利
            if (sellpxA == 0 || sellpxB == 0)
                return -999;

            //计算对冲成本
            //  假设对冲比例1:1
            //  仅对股票进行对冲
            double hedgepx = sf.RealtimeNAV.RealtimeNAV;
            double hedgecost = 0;
            if (this._TradeOption.IsHedge && sf.Category.AssetCategory == FundAssetCategory.Equity)
            {
                //融券交易成本：1买1卖
                hedgecost += hedgepx * this._TradeOption.Commision * 2;
                //融券利息成本：T日交易所场内申购－T+1日下午申购确认－T+2日客户可查到份额—T+2拆分申请－T+3在二级市场可卖
                hedgecost += hedgepx * this._TradeOption.HedgeIntRate / 365 * 3;
            }

            double cost = sf.RealtimeNAV.RealtimeNAV * (1 + sf.MaxSubscribeRate) + hedgecost;
            double rev = (sellpxA * sf.ShareARatio + sellpxB * (1 - sf.ShareARatio)) * (1 - this._TradeOption.Commision);
            return rev / cost - 1;
        }
        private double calPremDept(StructuredFund sf, int tickPosition)
        {
            //==========================
            //  申购卖出套利——市场深度
            //==========================

            //数据问题或者不可交易，无法套利
            if (sf.ShareA == null || sf.ShareB == null
                || sf.ShareA.RealtimeTradePrice == null || sf.ShareB.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "BuyPrice" + tickPosition;
            string keynamevol = "BuyVolume" + tickPosition;

            double sellpxA = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareA.RealtimeTradePrice);
            double sellvolA = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareA.RealtimeTradePrice);
            double sellpxB = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(sf.ShareB.RealtimeTradePrice);
            double sellvolB = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(sf.ShareB.RealtimeTradePrice);

            //跌停卖不掉，无法套利
            if (sellpxA == 0 || sellpxB == 0)
                return -999;

            //单位：万元
            return Math.Min(sellpxA * sellvolA / sf.ShareARatio, sellpxB * sellvolB / (1 - sf.ShareARatio)) / 10000;
        }

        protected override string GetTradeLog(string code)
        {
            bool flag = true;

            #region 交易明细
            StructuredFund sf = (StructuredFund)_FundGroup.GetSecurity(code);
            ListedFund lfA = (ListedFund)sf.ShareA;
            ListedFund lfB = (ListedFund)sf.ShareB;
            string html = "";
            int hedgedays = 0, redeemdays = 0;
            double volA = 0, volB = 0, amountA = 0, amountB = 0, costA = 0, costB = 0, pxA = 0, pxB = 0;
            double nav = 0, amount = 0, cost = 0;
            double profit = 0, totalcost = 0;

            nav = sf.RealtimeNAV.RealtimeNAV;
            if ((double)_TradingRow[C_ColName_Disc + C_ColName_Arbi + "1"] > 0)
            {
                pxA = ((RealtimeTradePrice)lfA.RealtimeTradePrice).SellPrice1;
                pxB = ((RealtimeTradePrice)lfB.RealtimeTradePrice).SellPrice1;
                volA = ((RealtimeTradePrice)lfA.RealtimeTradePrice).SellVolume1;
                volB = ((RealtimeTradePrice)lfB.RealtimeTradePrice).SellVolume1;
                volA = Math.Round(Math.Min(volA / sf.ShareARatio, volB / (1 - sf.ShareARatio)) * sf.ShareARatio, 0);
                volB = Math.Round(Math.Min(volA / sf.ShareARatio, volB / (1 - sf.ShareARatio)) * (1 - sf.ShareARatio), 0);
                amountA = pxA * volA;
                amountB = pxB * volB;
                costA = Math.Max(5, amountA * this._TradeOption.Commision);
                costB = Math.Max(5, amountB * this._TradeOption.Commision);

                switch (DateTime.Today.DayOfWeek)
                {
                    case DayOfWeek.Wednesday:
                        hedgedays = 2;
                        redeemdays = 11;
                        break;
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        hedgedays = 4;
                        redeemdays = 11;
                        break;
                    default:
                        hedgedays = 2;
                        redeemdays = 9;
                        break;
                }

                html += @"<tr>
                        <td>买入(实时)</td>
                        <td>" + lfA.Code + @"</td>
                        <td align='right'>" + pxA.ToString("N3") + @"</td>
                        <td align='right'>" + volA.ToString("N0") + @"</td>
                        <td align='right'>" + (-amountA).ToString("N2") + @"</td>
                        <td align='right'>" + sf.ShareARatio.ToString("N1") + @"</td>
                        <td align='right'>" + (-costA).ToString("N2") + @"</td>
                        <td>佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；</td>
                      </tr>";
                html += @"<tr>
                        <td>买入(实时)</td>
                        <td>" + lfB.Code + @"</td>
                        <td align='right'>" + pxB.ToString("N3") + @"</td>
                        <td align='right'>" + volB.ToString("N0") + @"</td>
                        <td align='right'>" + (-amountB).ToString("N2") + @"</td>
                        <td align='right'>" + (1 - sf.ShareARatio).ToString("N1") + @"</td>
                        <td align='right'>" + (-costB).ToString("N2") + @"</td>
                        <td>佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；</td>
                      </tr>";
                totalcost += costA + costB;

                amount = nav * (volA + volB);
                cost = amount * sf.MaxRedeemRate;
                html += @"<tr>
                        <td>赎回(实时)</td>
                        <td>" + sf.Code + @"</td>
                        <td align='right'>" + nav.ToString("N3") + @"</td>
                        <td align='right'>" + (volA + volB).ToString("N0") + @"</td>
                        <td align='right'>" + amount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>赎回费率：" + sf.MaxRedeemRate.ToString("P2") + "；机会成本：" + (redeemdays + hedgedays) + "天 = " + (0.04 / 365 * (redeemdays + hedgedays) * amount).ToString("N2") + @"；</td>
                      </tr>";
                totalcost += cost;
                profit = (amount - amountA - amountB);
            }
            else if ((double)_TradingRow[C_ColName_Prem + C_ColName_Arbi + "1"] > 0)
            {
                pxA = ((RealtimeTradePrice)lfA.RealtimeTradePrice).BuyPrice1;
                pxB = ((RealtimeTradePrice)lfB.RealtimeTradePrice).BuyPrice1;
                volA = ((RealtimeTradePrice)lfA.RealtimeTradePrice).BuyVolume1;
                volB = ((RealtimeTradePrice)lfB.RealtimeTradePrice).BuyVolume1;
                volA = Math.Min(volA / sf.ShareARatio, volB / (1 - sf.ShareARatio)) * sf.ShareARatio;
                volB = Math.Min(volA / sf.ShareARatio, volB / (1 - sf.ShareARatio)) * (1 - sf.ShareARatio);
                amountA = pxA * volA;
                amountB = pxB * volB;
                costA = Math.Max(5, amountA * this._TradeOption.Commision);
                costB = Math.Max(5, amountB * this._TradeOption.Commision);

                switch (DateTime.Today.DayOfWeek)
                {
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        hedgedays = 5;
                        break;
                    default:
                        hedgedays = 3;
                        break;
                }

                cost = nav * (volA + volB) * sf.MaxSubscribeRate;
                amount = nav * (volA + volB);
                html += @"<tr>
                        <td>申购(估算)</td>
                        <td>" + sf.Code + @"</td>
                        <td align='right'>" + nav.ToString("N3") + @"</td>
                        <td align='right'>" + (volA + volB).ToString("N0") + @"</td>
                        <td align='right'>" + amount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>申购费率：" + sf.MaxSubscribeRate.ToString("P2") + @"；实际申购以收盘价计；</td>                      
                        </tr>";
                totalcost += cost;

                html += @"<tr>
                        <td>卖出(估算)</td>
                        <td>" + lfA.Code + @"</td>
                        <td align='right'>" + pxA.ToString("N3") + @"</td>
                        <td align='right'>" + volA.ToString("N0") + @"</td>
                        <td align='right'>" + (-amountA).ToString("N2") + @"</td>
                        <td align='right'>" + sf.ShareARatio.ToString("N1") + @"</td>
                        <td align='right'>" + (-costA).ToString("N2") + @"</td>
                        <td>佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；实际卖出以当日实时计；</td>
                      </tr>";
                html += @"<tr>
                        <td>卖出(估算)</td>
                        <td>" + lfB.Code + @"</td>
                        <td align='right'>" + pxB.ToString("N3") + @"</td>
                        <td align='right'>" + volB.ToString("N0") + @"</td>
                        <td align='right'>" + (-amountB).ToString("N2") + @"</td>
                        <td align='right'>" + (1 - sf.ShareARatio).ToString("N1") + @"</td>
                        <td align='right'>" + (-costB).ToString("N2") + @"</td>
                        <td>佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；实际卖出以当日实时计；</td>
                      </tr>";
                totalcost += costA + costB;
                profit = (amountA + amountB - amount);
            }
            else
                flag = false;
            #endregion

            #region 对冲合计
            if (flag)
            {
                double hedgepx = ((RealtimeTradePrice)this._FundHedge.RealtimeTradePrice).SellPrice1;
                double hedgeshare = Math.Round((amountA + amountB) / hedgepx, 0);
                double hedgeamount = hedgepx * hedgeshare;
                cost = Math.Max(5, hedgeamount * this._TradeOption.Commision);
                double hedgeinterest = this._TradeOption.HedgeIntRate / 365 * hedgedays * hedgeamount;

                html += @"<tr>
                        <td>卖出(对冲)</td>
                        <td>" + this._TradeOption.HedgeCode + @"</td>
                        <td align='right'>" + ((RealtimeTradePrice)this._FundHedge.RealtimeTradePrice).SellPrice1 + @"</td>
                        <td align='right'>" + hedgeshare.ToString("N0") + @"</td>
                        <td align='right'>*" + hedgeamount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost - hedgeinterest).ToString("N2") + @"</td>
                        <td>佣金费用：" + cost.ToString("N2") + "；利息费用：" + hedgedays + "天 = " + hedgeinterest.ToString("N2") + @"；</td>
                      </tr>";
                totalcost += cost + hedgeinterest;

                html += @"<tr><td colspan='" + _TradeLogTableColSpan + "'><hr /></td></tr>";
                html += @"<tr>
                        <td>合计</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td align='right'>" + profit.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-totalcost).ToString("N2") + @"</td>
                        <td>净利润：" + (profit - totalcost).ToString("N2") + "；毛利率：" + (profit / amount).ToString("P2") + "；净利率：" + ((profit - totalcost) / amount).ToString("P2") + @"；</td>
                      </tr>";
            }
            #endregion

            #region 持有期决策
            pxA = ((RealtimeTradePrice)lfA.RealtimeTradePrice).BuyPrice1;
            pxB = ((RealtimeTradePrice)lfB.RealtimeTradePrice).BuyPrice1;
            volA = 20000;
            volB = 20000;
            amount = nav * (volA + volB);
            cost = amount * sf.MaxRedeemRate;

            html += @"<tr><td colspan='" + _TradeLogTableColSpan + "'><hr /></td></tr>";
            html += @"<tr><td colspan='" + _TradeLogTableColSpan + "'>持有期决策</td></tr>";
            html += @"<tr>
                        <td>赎回(估算)</td>
                        <td>" + sf.Code + @"</td>
                        <td align='right'>" + nav.ToString("N3") + @"</td>
                        <td align='right'>" + (volA + volB).ToString("N0") + @"</td>
                        <td align='right'>" + amount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>净收入：" + (amount - cost).ToString("N2") + @"；赎回费率：" + sf.MaxRedeemRate.ToString("P2") + @"；</td>
                      </tr>";

            amountA = pxA * volA;
            amountB = pxB * volB;
            costA = Math.Max(5, pxA * volA * this._TradeOption.Commision);
            costB = Math.Max(5, pxB * volB * this._TradeOption.Commision);
            html += @"<tr>
                        <td>卖出(实时)</td>
                        <td>" + lfA.Code + @"</td>
                        <td align='right'>" + pxA.ToString("N3") + @"</td>
                        <td align='right'>" + volA.ToString("N0") + @"</td>
                        <td align='right'>" + amountA.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-costA).ToString("N2") + @"</td>
                        <td>佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；</td>
                      </tr>";
            html += @"<tr>
                        <td>卖出(实时)</td>
                        <td>" + lfB.Code + @"</td>
                        <td align='right'>" + pxB.ToString("N3") + @"</td>
                        <td align='right'>" + volB.ToString("N0") + @"</td>
                        <td align='right'>" + amountB.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-costB).ToString("N2") + @"</td>
                        <td>净收入：" + (amountA - costA + amountB - costB).ToString("N2") + @"；佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；</td>
                      </tr>";
            #endregion

            return html;
        }
    }
}
