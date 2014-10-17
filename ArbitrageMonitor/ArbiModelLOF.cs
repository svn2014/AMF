using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Security;
using System.Data;
using System.Diagnostics;

namespace ArbitrageMonitor
{
    class ArbiModelLOF : ArbiModel
    {
        public ArbiModelLOF()
        {
            this.initializeTable();

            FundSelector fs = new FundSelector();
            _FundGroup = fs.GetLOF();
            _FundGroup.LoadData(DataInfoType.SecurityInfo);
        }

        protected override void initializeTable()
        {
            base.initializeTable();

            _ViewData.Columns.Add(C_ColName_Price, typeof(double));
            _ViewData.Columns.Add(C_ColName_NAV, typeof(double));
            _ViewData.Columns.Add(C_ColName_Amount, typeof(double));
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
                    ListedFund lf = (ListedFund)_FundGroup.SecurityList[i];
                    if (lf.Code == "160220")
                    { }

                    //不符合设置要求的去掉
                    if (!this._TradeOption.ShowActiveBondFunds
                        && lf.Category.AssetCategory == FundAssetCategory.Bond
                        && lf.Category.InvestmentCategory == FundInvestmentCategory.Active)
                        continue;
                    if (!this._TradeOption.ShowActiveEquityFunds
                        && lf.Category.AssetCategory == FundAssetCategory.Equity
                        && lf.Category.InvestmentCategory == FundInvestmentCategory.Active)
                        continue;
                    if (!this._TradeOption.ShowPassiveBondFunds
                        && lf.Category.AssetCategory == FundAssetCategory.Bond
                        && lf.Category.InvestmentCategory == FundInvestmentCategory.Passive)
                        continue;
                    if (!this._TradeOption.ShowPassiveEquityFunds
                        && lf.Category.AssetCategory == FundAssetCategory.Equity
                        && lf.Category.InvestmentCategory == FundInvestmentCategory.Passive)
                        continue;
                    if (!this._TradeOption.ShowActiveHybridFunds
                        && lf.Category.AssetCategory == FundAssetCategory.Hybrid
                        && lf.Category.InvestmentCategory == FundInvestmentCategory.Active)
                        continue;

                    if (!this._TradeOption.ShowOthers
                        && (lf.Category.AssetCategory == FundAssetCategory.QDII
                            || lf.Category.AssetCategory == FundAssetCategory.Monetory
                            || lf.Category.AssetCategory == FundAssetCategory.Other
                            || lf.Category.AssetCategory == FundAssetCategory.Undefined
                            )
                        )
                        continue;

                    DataRow row = _ViewData.NewRow();
                    row[C_ColName_Name] = lf.Name;
                    row[C_ColName_Code] = lf.Code;
                    row[C_ColName_FundType] = lf.Category.Name3;
                    row[C_ColName_SubscribeRate] = lf.MaxSubscribeRate;
                    row[C_ColName_RedeemRate] = lf.MaxRedeemRate;

                    if (lf.FoundedDate > new DateTime(1990, 1, 1))
                        row[C_ColName_FundedDate] = Convert.ToDateTime(lf.FoundedDate);

                    if (lf.Category.AssetCategory == FundAssetCategory.Equity)
                    {
                        if (lf.PrimaryBenchmarkIndex != null)
                        {
                            row[C_ColName_BenchmarkCode] = lf.PrimaryBenchmarkIndex.Code;
                            row[C_ColName_BenchmarkName] = lf.PrimaryBenchmarkIndex.Name;
                        }
                        else
                            row[C_ColName_BenchmarkName] = "**默认指数**";
                    }
                    else
                    {
                        row[C_ColName_BenchmarkCode] = "";
                        row[C_ColName_BenchmarkName] = "";
                    }

                    #region 基金类别
                    string category = "";
                    switch (lf.Category.InvestmentCategory)
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
                    switch (lf.Category.AssetCategory)
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
                    if (lf.RealtimeTradePrice != null)
                    {
                        row[C_ColName_Price] = ((RealtimeTradePrice)lf.RealtimeTradePrice).Close;
                        row[C_ColName_NAV] = lf.RealtimeNAV.RealtimeNAV;
                        row[C_ColName_Amount] = ((RealtimeTradePrice)lf.RealtimeTradePrice).Amount / 10000;
                    }
                    #endregion

                    double discarbi1 = -1, premarbi1 = -1;
                    #region 套利详细
                    for (int j = 1; j <= 5; j++)
                    {
                        double discarbi = this.calDiscArbi(lf, j);
                        double premarbi = this.calPremArbi(lf, j);
                        double discdept = this.calDiscDept(lf, j);
                        double premdept = this.calPremDept(lf, j);

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

        private double calDiscArbi(ListedFund lf, int tickPosition)
        {
            //==========================
            //  买入赎回套利
            //==========================

            //数据问题或者不可交易，无法套利
            if (lf.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "SellPrice" + tickPosition;
            string keynamevol = "SellVolume" + tickPosition;

            //根据卖1-卖5决定买入价
            double buypx = (double)(typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(lf.RealtimeTradePrice));
            double buyvol = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(lf.RealtimeTradePrice);

            //涨停买不到，无法套利
            if (buypx == 0)
                return -999;

            //计算对冲成本
            //  假设对冲比例1:1
            //  仅对股票进行对冲
            double hedgepx = buypx;
            double hedgecost = 0;
            if (this._TradeOption.IsHedge && lf.Category.AssetCategory == FundAssetCategory.Equity)
            {
                //融券交易成本：1买1卖
                hedgecost += hedgepx * this._TradeOption.Commision * 2;
                //融券利息成本：T日在二级市场买入－T+1合并申请－T+2确认－T+2可以赎回－平仓空头头寸
                hedgecost += hedgepx * this._TradeOption.HedgeIntRate / 365 * 2;
            }

            double cost = buypx * (1 + this._TradeOption.Commision) + hedgecost;
            double rev = lf.RealtimeNAV.RealtimeNAV * (1 - lf.MaxRedeemRate);
            return rev / cost - 1;
        }
        private double calDiscDept(ListedFund lf, int tickPosition)
        {
            //==========================
            //  买入赎回套利——市场深度
            //==========================

            //数据问题或者不可交易，无法套利
            if (lf.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "SellPrice" + tickPosition;
            string keynamevol = "SellVolume" + tickPosition;

            double buypx = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(lf.RealtimeTradePrice);
            double buyvol = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(lf.RealtimeTradePrice);

            //涨停买不到，无法套利
            if (buypx == 0)
                return -999;

            //单位：万元
            return buypx * buyvol / 10000;
        }
        private double calPremArbi(ListedFund lf, int tickPosition)
        {
            //==========================
            //  申购卖出套利
            //==========================

            //数据问题或者不可交易，无法套利
            if (lf.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "BuyPrice" + tickPosition;
            string keynamevol = "BuyVolume" + tickPosition;

            double sellpx = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(lf.RealtimeTradePrice);
            double sellvol = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(lf.RealtimeTradePrice);

            //跌停卖不掉，无法套利
            if (sellpx == 0)
                return -999;

            //计算对冲成本
            //  假设对冲比例1:1
            //  仅对股票进行对冲
            double hedgepx = lf.RealtimeNAV.RealtimeNAV;
            double hedgecost = 0;
            if (this._TradeOption.IsHedge && lf.Category.AssetCategory == FundAssetCategory.Equity)
            {
                //融券交易成本：1买1卖
                hedgecost += hedgepx * this._TradeOption.Commision * 2;
                //融券利息成本：T日交易所场内申购－T+1日下午申购确认－T+2日客户可查到份额—T+2拆分申请－T+3在二级市场可卖
                hedgecost += hedgepx * this._TradeOption.HedgeIntRate / 365 * 3;
            }

            double cost = lf.RealtimeNAV.RealtimeNAV * (1 + lf.MaxSubscribeRate) + hedgecost;
            double rev = sellpx * (1 - this._TradeOption.Commision);
            return rev / cost - 1;
        }
        private double calPremDept(ListedFund lf, int tickPosition)
        {
            //==========================
            //  申购卖出套利——市场深度
            //==========================

            //数据问题或者不可交易，无法套利
            if (lf.RealtimeTradePrice == null)
                return -888;

            if (tickPosition > 5)
                tickPosition = 5;
            string keynamepx = "BuyPrice" + tickPosition;
            string keynamevol = "BuyVolume" + tickPosition;

            double sellpx = (double)typeof(RealtimeTradePrice).GetField(keynamepx).GetValue(lf.RealtimeTradePrice);
            double sellvol = (double)typeof(RealtimeTradePrice).GetField(keynamevol).GetValue(lf.RealtimeTradePrice);

            //跌停卖不掉，无法套利
            if (sellpx == 0)
                return -999;

            //单位：万元
            return sellpx * sellvol / 10000;
        }

        protected override string GetTradeLog(string code)
        {
            bool flag = true;

            #region 交易明细
            ListedFund lf = (ListedFund)_FundGroup.GetSecurity(code);
            string html = "";
            double px = 0, nav = 0, vol = 0, amount = 0, cost = 0;
            int hedgedays = 0, redeemdays = 0;
            double profit = 0, totalcost = 0;

            nav = lf.RealtimeNAV.RealtimeNAV;
            if ((double)_TradingRow[C_ColName_Disc + C_ColName_Arbi + "1"] > 0)
            {
                px = ((RealtimeTradePrice)lf.RealtimeTradePrice).SellPrice1;
                vol = ((RealtimeTradePrice)lf.RealtimeTradePrice).SellVolume1;
                amount = px * vol;
                cost = Math.Max(5, amount * this._TradeOption.Commision);

                switch (DateTime.Today.DayOfWeek)
                {
                    case DayOfWeek.Wednesday:
                        hedgedays = 1;
                        redeemdays = 11;
                        break;
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        hedgedays = 3;
                        redeemdays = 11;
                        break;
                    default:
                        hedgedays = 1;
                        redeemdays = 9;
                        break;
                }

                html += @"<tr>
                        <td>买入(实时)</td>
                        <td>" + lf.Code + @"</td>
                        <td align='right'>" + px.ToString("N3") + @"</td>
                        <td align='right'>" + vol.ToString("N0") + @"</td>
                        <td align='right'>" + (-amount).ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；</td>
                      </tr>";
                totalcost += cost;

                amount = nav * vol;
                cost = amount * lf.MaxRedeemRate;
                html += @"<tr>
                        <td>赎回(实时)</td>
                        <td>" + lf.Code + @"</td>
                        <td align='right'>" + nav.ToString("N3") + @"</td>
                        <td align='right'>" + vol.ToString("N0") + @"</td>
                        <td align='right'>" + amount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>赎回费率：" + lf.MaxRedeemRate.ToString("P2") + "；机会成本：" + (redeemdays + hedgedays) + "天=" + (0.04 / 365 * (redeemdays + hedgedays) * amount).ToString("N2") + @"；</td>
                      </tr>";
                totalcost += cost;
                profit = (nav - px) * vol;
            }
            else if ((double)_TradingRow[C_ColName_Prem + C_ColName_Arbi + "1"] > 0)
            {
                px = ((RealtimeTradePrice)lf.RealtimeTradePrice).BuyPrice1;
                vol = ((RealtimeTradePrice)lf.RealtimeTradePrice).BuyVolume1;
                amount = px * vol;

                switch (DateTime.Today.DayOfWeek)
                {
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        hedgedays = 4;
                        break;
                    default:
                        hedgedays = 2;
                        break;
                }

                cost = nav * vol * lf.MaxSubscribeRate;
                html += @"<tr>
                        <td>申购(估算)</td>
                        <td>" + lf.Code + @"</td>
                        <td align='right'>" + nav.ToString("N3") + @"</td>
                        <td align='right'>" + vol.ToString("N0") + @"</td>
                        <td align='right'>" + (nav * vol).ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>申购费率：" + lf.MaxSubscribeRate.ToString("P2") + @"；实际申购以收盘价计；</td>
                      </tr>";
                totalcost += cost;

                cost = Math.Max(5, amount * this._TradeOption.Commision);
                html += @"<tr>
                        <td>卖出(估算)</td>
                        <td>" + lf.Code + @"</td>
                        <td align='right'>" + px.ToString("N3") + @"</td>
                        <td align='right'>" + vol.ToString("N0") + @"</td>
                        <td align='right'>" + (-amount).ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；实际卖出以当日实时计；</td>
                      </tr>";
                totalcost += cost;
                profit = (px - nav) * vol;
            }
            else
                flag = false;
            #endregion

            #region 对冲合计
            if (flag)
            {
                double hedgepx = ((RealtimeTradePrice)this._FundHedge.RealtimeTradePrice).SellPrice1;
                double hedgeshare = Math.Round(amount / hedgepx, 0);
                double hedgeamount = hedgepx * hedgeshare;
                cost = Math.Max(5, hedgeamount * this._TradeOption.Commision);
                double hedgeinterest = this._TradeOption.HedgeIntRate / 365 * hedgedays * hedgeamount;

                if (lf.Category.AssetCategory == FundAssetCategory.Equity ||
                    lf.Category.AssetCategory == FundAssetCategory.Hybrid)
                {
                    //仅对冲股票型
                    html += @"<tr>
                        <td>卖出(对冲)</td>
                        <td>" + this._TradeOption.HedgeCode + @"</td>
                        <td align='right'>" + ((RealtimeTradePrice)this._FundHedge.RealtimeTradePrice).SellPrice1 + @"</td>
                        <td align='right'>" + hedgeshare.ToString("N0") + @"</td>
                        <td align='right'>*" + hedgeamount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost - hedgeinterest).ToString("N2") + @"</td>
                        <td>佣金费用：" + cost.ToString("N2") + "；利息费用：" + hedgedays + "天 = " + hedgeinterest.ToString("N2") + "；" + @"</td>
                      </tr>";
                    totalcost += cost + hedgeinterest;
                }

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
            px = ((RealtimeTradePrice)lf.RealtimeTradePrice).BuyPrice1;
            vol = 20000;
            amount = nav * vol;
            cost = nav * vol * lf.MaxRedeemRate;

            html += @"<tr><td colspan='" + _TradeLogTableColSpan + "'><hr /></td></tr>";
            html += @"<tr><td colspan='" + _TradeLogTableColSpan + "'>持有期决策</td></tr>";
            html += @"<tr>
                        <td>赎回(估算)</td>
                        <td>" + lf.Code + @"</td>
                        <td align='right'>" + nav.ToString("N3") + @"</td>
                        <td align='right'>" + vol.ToString("N0") + @"</td>
                        <td align='right'>" + amount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>净收入：" + (amount - cost).ToString("N2") + @"；赎回费率：" + lf.MaxRedeemRate.ToString("P2") + @"；</td>
                      </tr>";

            amount = px * vol;
            cost = Math.Max(5, amount * this._TradeOption.Commision);
            html += @"<tr>
                        <td>卖出(实时)</td>
                        <td>" + lf.Code + @"</td>
                        <td align='right'>" + px.ToString("N3") + @"</td>
                        <td align='right'>" + vol.ToString("N0") + @"</td>
                        <td align='right'>" + amount.ToString("N2") + @"</td>
                        <td></td>
                        <td align='right'>" + (-cost).ToString("N2") + @"</td>
                        <td>净收入：" + (amount - cost).ToString("N2") + @"；佣金费率：" + this._TradeOption.Commision.ToString("P2") + @"；</td>
                      </tr>";
            
            html += @"<tr><td colspan='" + _TradeLogTableColSpan + "'><hr /></td></tr>";
            #endregion

            return html;
        }
    }
}
