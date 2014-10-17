using System;
using System.Data;
using Security;

namespace ArbitrageMonitor
{
    class ArbiModel
    {
        #region Consts
        public const string C_ColName_OrderValue = "Order";
        public const string C_ColName_Category = "Cate";
        public const string C_ColName_Name = "Name";
        public const string C_ColName_Code = "Code";
        //public const string C_ColName_Desc = "Desc";
        public const string C_ColName_CodeA = "CodeA";
        public const string C_ColName_CodeB = "CodeB";
        public const string C_ColName_BenchmarkName = "BenchmarkName";
        public const string C_ColName_BenchmarkCode = "BenchmarkCode";
        public const string C_ColName_Disc = "Discount";   //折价
        public const string C_ColName_Prem = "Premium";   //溢价
        public const string C_ColName_Arbi = "Arbi";   //套利
        public const string C_ColName_Dept = "Depth";   //深度
        public const string C_ColName_SubscribeRate = "SubscribeRate";
        public const string C_ColName_RedeemRate = "RedeemRate";
        public const string C_ColName_FundedDate = "FundedDate";
        public const string C_ColName_AmountA = "AmountA";
        public const string C_ColName_AmountB = "AmountB";
        public const string C_ColName_AmountAB = "AmountAB";
        public const string C_ColName_Amount = "Amount";
        public const string C_ColName_ShareARatio = "ShareARatio";
        public const string C_ColName_DiscA = "DiscA";   //A折价
        public const string C_ColName_DiscB = "DiscB";   //B折价
        public const string C_ColName_PremA = "PremA";   //A溢价
        public const string C_ColName_PremB = "PremB";   //B溢价
        public const string C_ColName_PriceA = "PriceA";
        public const string C_ColName_PriceB = "PriceB";
        public const string C_ColName_NAV = "NAV";
        public const string C_ColName_Price = "Price";
        public const string C_ColName_FundType = "FundType";
        #endregion

        protected ListedFund _FundHedge = new ListedFund("510300");
        protected FundGroup _FundGroup = null;
        protected DataTable _ViewData = new DataTable();
        protected DataRow _TradingRow;

        protected ArbiOption _TradeOption = new ArbiOption();
        public ArbiOption TradeOption 
        {
            get { return _TradeOption; }
            set {
                _TradeOption = TradeOption;
                _FundHedge = new ListedFund(_TradeOption.HedgeCode);
                _FundHedge.LoadData(DataInfoType.RealtimeTradePrice);
            }
        }

        protected virtual void initializeTable()
        {
            _ViewData.Columns.Add(C_ColName_OrderValue, typeof(double));
            _ViewData.Columns.Add(C_ColName_Category, typeof(string));
            _ViewData.Columns.Add(C_ColName_Name, typeof(string));
            _ViewData.Columns.Add(C_ColName_Code, typeof(string));
            _ViewData.Columns.Add(C_ColName_FundedDate, typeof(DateTime));

            for (int i = 1; i <= 5; i++)
            {
                _ViewData.Columns.Add(C_ColName_Disc + C_ColName_Arbi + i, typeof(double)); //折价套利
                _ViewData.Columns.Add(C_ColName_Disc + C_ColName_Dept + i, typeof(double)); //折价套利市场深度
                _ViewData.Columns.Add(C_ColName_Prem + C_ColName_Arbi + i, typeof(double)); //溢价套利
                _ViewData.Columns.Add(C_ColName_Prem + C_ColName_Dept + i, typeof(double)); //溢价套利市场深度
            }

            _ViewData.Columns.Add(C_ColName_SubscribeRate, typeof(double));
            _ViewData.Columns.Add(C_ColName_RedeemRate, typeof(double));
            _ViewData.Columns.Add(C_ColName_BenchmarkName, typeof(string));
            _ViewData.Columns.Add(C_ColName_BenchmarkCode, typeof(string));
            _ViewData.Columns.Add(C_ColName_FundType, typeof(string));
        }

        public virtual DataTable GetData() { return _ViewData; }

        protected int _TradeLogTableColSpan = 8;
        public string GetLog(string code) 
        {
            if (code.Trim().Length == 0)
                return "";

            DataRow[] rows = this._ViewData.Select(C_ColName_Code + "='" + code + "'");

            if (rows.Length == 0)
                return "";
            else
                _TradingRow = rows[0];

            string title ="";
            if ((double)_TradingRow[C_ColName_Disc + C_ColName_Arbi + "1"] > 0)
            {
                title = "折价套利：" + code + " - " + _TradingRow[C_ColName_Name].ToString();
            }
            else if ((double)_TradingRow[C_ColName_Prem + C_ColName_Arbi + "1"] > 0)
            {
                title = "溢价套利：" + code + " - " + _TradingRow[C_ColName_Name].ToString();
            }
            else
                title = "无法套利：" + code + " - " + _TradingRow[C_ColName_Name].ToString();

            string html = "<table width='100%'>";
            html += "<tr><td colspan='" + _TradeLogTableColSpan + "'><hr /></td></tr>";
            html += "<tr><td colspan='" + _TradeLogTableColSpan + "' bgcolor='Yellow'>" + title + "</td></tr>";
            html += @"<tr>
                        <td width='10%'>操作</td>
                        <td width='10%'>代码</td>
                        <td width='10%' align='right'>价格(元)</td>
                        <td width='10%' align='right'>数量(份)</td>
                        <td width='10%' align='right'>金额(元)</td>
                        <td width='5%' align='right'>比例</td>
                        <td width='10%' align='right'>成本(元)</td>
                        <td>备注</td>
                      </tr>";

            html += GetTradeLog(code);

            html += "</table>";

            return html;
        }
        protected virtual string GetTradeLog(string code)
        {
            return "<tr><td colspan='" + _TradeLogTableColSpan + "'><hr /></td></tr>";
        }

        public DataTable GetData(DataTable dtSOF, DataTable dtLOF, bool positiveOnly)
        {
            if (this._ViewData.Columns.Count == 0)
                this.initializeTable();

            //填充数据
            this._ViewData.Clear();
            DataTable[] aryDTs = { dtSOF, dtLOF };
            for (int idx = 0; idx < aryDTs.Length; idx++)
            {
                DataTable dt = aryDTs[idx];

                foreach (DataRow r in dt.Rows)
                {
                    if (positiveOnly)
                        if (Convert.ToDouble(r[C_ColName_OrderValue]) < 0)
                            continue;

                    DataRow newRow = _ViewData.NewRow();
                    newRow[C_ColName_OrderValue] = r[C_ColName_OrderValue];
                    newRow[C_ColName_Category] = r[C_ColName_Category];
                    newRow[C_ColName_Name] = r[C_ColName_Name];
                    newRow[C_ColName_Code] = r[C_ColName_Code];
                    
                    for (int i = 1; i <= 5; i++)
                    {
                        newRow[C_ColName_Disc + C_ColName_Arbi + i] = r[C_ColName_Disc + C_ColName_Arbi + i];
                        newRow[C_ColName_Disc + C_ColName_Dept + i] = r[C_ColName_Disc + C_ColName_Dept + i];
                        newRow[C_ColName_Prem + C_ColName_Arbi + i] = r[C_ColName_Prem + C_ColName_Arbi + i];
                        newRow[C_ColName_Prem + C_ColName_Dept + i] = r[C_ColName_Prem + C_ColName_Dept + i];
                    }

                    newRow[C_ColName_SubscribeRate] = r[C_ColName_SubscribeRate];
                    newRow[C_ColName_RedeemRate] = r[C_ColName_RedeemRate];
                    newRow[C_ColName_BenchmarkName] = r[C_ColName_BenchmarkName];
                    newRow[C_ColName_BenchmarkCode] = r[C_ColName_BenchmarkCode];
                    newRow[C_ColName_FundedDate] = r[C_ColName_FundedDate];

                    switch (idx)
                    {
                        case 0:
                            newRow[C_ColName_FundType] = "SOF";
                            break;
                        case 1:
                            newRow[C_ColName_FundType] = "LOF";
                            break;
                        default:
                            break;
                    }

                    _ViewData.Rows.Add(newRow);
                }
            }

            return _ViewData;
        }
    }
}
