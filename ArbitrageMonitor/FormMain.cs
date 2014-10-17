using System;
using System.Data;
using System.Windows.Forms;
using Security;
using System.Reflection;
using System.Drawing;

namespace ArbitrageMonitor
{
    public partial class FormMain : Form
    {
        private enum ViewType
        { 
            ShowDiscArbi,
            ShowPremArbi,
            ShowAll
        }

        private enum ObjectType
        {
            SOF,
            LOF,
            ALL
        }

        #region Background Worker
        private void backgroundWorkerMain_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.backgroundWorkerMain.ReportProgress(10);
            this.initialModel();
            this.backgroundWorkerMain.ReportProgress(30);
            this.refreshData();
            this.backgroundWorkerMain.ReportProgress(50);
        }

        private void backgroundWorkerMain_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.initialOptions();
            this.refreshGridView(this._ViewType, this._ObjectType);
            this.initialGridView(this._ObjectType);
            this.refreshViewUI(this._ViewType);

            if (!this.timerMain.Enabled)
            {
                this.timerMain.Start();
                toolStripStatusLabelDesc.Text = "自动";
            }
        }

        private void backgroundWorkerMain_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            toolStripStatusLabelDesc.Text = e.ProgressPercentage + "%";
        }
        #endregion

        #region Actions
        public FormMain()
        {
            InitializeComponent();

            //初始化
            this.loadSettings();
            backgroundWorkerMain.RunWorkerAsync();
        }
        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            this.refreshData();
            this.refreshGridView(this._ViewType, this._ObjectType);
            this.refreshViewUI(this._ViewType);
            this.initialGridView(this._ObjectType);
        }
        private void toolStripButtonShowDisc_Click(object sender, EventArgs e)
        {
            this.refreshGridView(ViewType.ShowDiscArbi, this._ObjectType);
            this.refreshViewUI(this._ViewType);
        }
        private void toolStripButtonShowPrem_Click(object sender, EventArgs e)
        {
            this.refreshGridView(ViewType.ShowPremArbi, this._ObjectType);
            this.refreshViewUI(this._ViewType);
        }
        private void toolStripButtonShowAll_Click(object sender, EventArgs e)
        {
            this.refreshGridView(ViewType.ShowAll, this._ObjectType);
            this.refreshViewUI(this._ViewType);
        }
        private void toolStripButtonSOF_Click(object sender, EventArgs e)
        {
            this.refreshGridView(this._ViewType, ObjectType.SOF);
            this.refreshViewUI(this._ViewType);
        }
        private void toolStripButtonLOF_Click(object sender, EventArgs e)
        {
            this.refreshGridView(this._ViewType, ObjectType.LOF);
            this.refreshViewUI(this._ViewType);
        }
        private void toolStripButtonAll_Click(object sender, EventArgs e)
        {
            this.refreshGridView(this._ViewType, ObjectType.ALL);
            this.refreshViewUI(this._ViewType);
        }

        private DateTime _TradeTimeStart = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " 9:30:00");
        private DateTime _TradeTimeEnd = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " 15:00:00");
        private void timerMain_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now > _TradeTimeStart && DateTime.Now < _TradeTimeEnd)
            {
                this.refreshData();
            }
            else
            {
                timerMain.Stop();
                toolStripStatusLabelDesc.Text = "停止";
            }
        }
        private void toolStripButtonStartTimer_Click(object sender, EventArgs e)
        {
            if (this.timerMain.Enabled)
            {
                this.timerMain.Stop();
                toolStripButtonStartTimer.DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripStatusLabelDesc.Text = "手动";
            }
            else
            {
                this.timerMain.Start();
                toolStripButtonStartTimer.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                toolStripStatusLabelDesc.Text = "自动";
            }
        }
        private void dataGridViewMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            DataGridView dgv = (DataGridView)sender;

            if (dgv.Columns[e.ColumnIndex].Name.Contains(ArbiModel.C_ColName_Arbi))
            {
                try
                {
                    if (e.Value != DBNull.Value)
                    {
                        if ((double)e.Value > this._Threshold)
                            e.CellStyle.BackColor = Color.Red;
                        else if ((double)e.Value > 0)
                            e.CellStyle.BackColor = Color.Pink;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void dataGridViewMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            DataGridView dgv = (DataGridView)sender;
            string code = dgv.Rows[e.RowIndex].Cells[ArbiModel.C_ColName_Code].Value.ToString();
            this.writelog(code);
        }
        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            webBrowserLog.DocumentText = "";
        }
        #endregion

        #region Options
        private bool _IsOptionLoaded = false;
        private ArbiOption _TradeOption = new ArbiOption();
        private void loadSettings()
        {
            string historyconn = Properties.Settings.Default.ConnString;
            HistoryDataVendor hdv = (HistoryDataVendor)Enum.Parse(typeof(HistoryDataVendor), Properties.Settings.Default.HistoryDataVendor, true);
            RealtimeDataVendor rdv = (RealtimeDataVendor)Enum.Parse(typeof(RealtimeDataVendor), Properties.Settings.Default.RealtimeDataVendor, true);
            DatabaseType dt = (DatabaseType)Enum.Parse(typeof(DatabaseType), Properties.Settings.Default.Database, true);
            DataManager.Initiate(hdv, historyconn, rdv, null, dt);
        }
        private void initialModel()
        {
            if (this._SOFModel == null)
                this._SOFModel = new ArbiModelSOF();

            if (this._LOFModel == null)
                this._LOFModel = new ArbiModelLOF();

            if (this._ALLModel == null)
                this._ALLModel = new ArbiModel();
        }
        private void initialOptions()
        {
            try
            {
                if (this._IsOptionLoaded)
                    return;

                //设置Common
                this._TradeOption.LoadSettings();
                this.numericUpDownRefreshFreq.Value = (decimal)this._TradeOption.RefreshFreq;               
                this.numericUpDownCommision.Value = (decimal)_TradeOption.Commision;
                this.numericUpDownShortIntRate.Value = (decimal)_TradeOption.HedgeIntRate;
                this.textBoxShortCode.Text = _TradeOption.HedgeCode;

                //设置SOF                
                this.checkBoxIsHedgeSOF.Checked = _TradeOption.IsHedge;
                this.numericUpDownThresholdSOF.Value = (decimal)_TradeOption.Threshold;
                this.checkBoxSOF_PE.Checked = _TradeOption.ShowPassiveEquityFunds;
                this.checkBoxSOF_PB.Checked = _TradeOption.ShowPassiveBondFunds;
                this.checkBoxSOF_AE.Checked = _TradeOption.ShowActiveEquityFunds;
                this.checkBoxSOF_AH.Checked = _TradeOption.ShowActiveHybridFunds;
                this.checkBoxSOF_AB.Checked = _TradeOption.ShowActiveBondFunds;
                this.checkBoxSOF_OT.Checked = _TradeOption.ShowOthers;
                this._SOFModel.TradeOption = this._TradeOption;

                //设置LOF
                this.checkBoxIsHedgeLOF.Checked = _TradeOption.IsHedge;
                this.numericUpDownThresholdLOF.Value = (decimal)_TradeOption.Threshold;
                this.checkBoxLOF_PE.Checked = _TradeOption.ShowPassiveEquityFunds;
                this.checkBoxLOF_PB.Checked = _TradeOption.ShowPassiveBondFunds;
                this.checkBoxLOF_AE.Checked = _TradeOption.ShowActiveEquityFunds;
                this.checkBoxLOF_AH.Checked = _TradeOption.ShowActiveHybridFunds;
                this.checkBoxLOF_AB.Checked = _TradeOption.ShowActiveBondFunds;
                this.checkBoxLOF_OT.Checked = _TradeOption.ShowOthers;
                this._LOFModel.TradeOption = this._TradeOption;

                //设置面板
                this.panelOptions.Enabled = false;
                this._IsOptionLoaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void toolStripButtonSaveEdit_Click(object sender, EventArgs e)
        {
            if (this.panelOptions.Enabled)
            {
                ((ToolStripButton)sender).Text = "编辑";
                this.panelOptions.Enabled = false;

                //保存SOF
                this._SOFModel.TradeOption.RefreshFreq = (int)numericUpDownRefreshFreq.Value;
                this._SOFModel.TradeOption.Commision = (double)numericUpDownCommision.Value;
                this._SOFModel.TradeOption.HedgeIntRate = (double)numericUpDownShortIntRate.Value;
                this._SOFModel.TradeOption.HedgeCode = textBoxShortCode.Text;

                this._SOFModel.TradeOption.IsHedge = checkBoxIsHedgeSOF.Checked;
                this._SOFModel.TradeOption.Threshold = (double)numericUpDownThresholdSOF.Value;
                this._SOFModel.TradeOption.ShowActiveEquityFunds = checkBoxSOF_AE.Checked;
                this._SOFModel.TradeOption.ShowActiveHybridFunds = checkBoxSOF_AH.Checked;
                this._SOFModel.TradeOption.ShowActiveBondFunds = checkBoxSOF_AB.Checked;
                this._SOFModel.TradeOption.ShowPassiveEquityFunds = checkBoxSOF_PE.Checked;
                this._SOFModel.TradeOption.ShowPassiveBondFunds = checkBoxSOF_PB.Checked;
                this._SOFModel.TradeOption.ShowOthers = checkBoxSOF_OT.Checked;

                this._SOFModel.TradeOption.SaveSettings();
                this._TradeOption = this._SOFModel.TradeOption;

                //保存LOF
                this._LOFModel.TradeOption.IsHedge = checkBoxIsHedgeLOF.Checked;
                this._LOFModel.TradeOption.Threshold = (double)numericUpDownThresholdLOF.Value;
                this._LOFModel.TradeOption.ShowActiveEquityFunds = checkBoxLOF_AE.Checked;
                this._LOFModel.TradeOption.ShowActiveHybridFunds = checkBoxLOF_AH.Checked;
                this._LOFModel.TradeOption.ShowActiveBondFunds = checkBoxLOF_AB.Checked;
                this._LOFModel.TradeOption.ShowPassiveEquityFunds = checkBoxLOF_PE.Checked;
                this._LOFModel.TradeOption.ShowPassiveBondFunds = checkBoxLOF_PB.Checked;
                this._LOFModel.TradeOption.ShowOthers = checkBoxLOF_OT.Checked;
            }
            else
            {
                //编辑设置
                ((ToolStripButton)sender).Text = "保存";
                this.panelOptions.Enabled = true;
            }
        }
        #endregion

        #region Common
        ViewType _ViewType = ViewType.ShowAll;
        ObjectType _ObjectType = ObjectType.ALL;
        double _Threshold = 0.01;

        private void initialGridView(ObjectType objecttype)
        {
            foreach (DataGridViewColumn col in this.dataGridViewMain.Columns)
            {
                //数字格式
                if (col.ValueType == typeof(double))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Format = "N2";
                }

                //日期格式
                if (col.ValueType == typeof(DateTime))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                //价格:N3
                if (col.Name.Contains(ArbiModel.C_ColName_Price))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Format = "N3";
                }

                //套利%
                if (col.Name.Contains(ArbiModel.C_ColName_Arbi))
                {
                    col.DefaultCellStyle.Format = "P2";
                    col.DefaultCellStyle.BackColor = Color.LightYellow;
                }

                switch (col.Name)
                {
                    case ArbiModel.C_ColName_PremA:
                    case ArbiModel.C_ColName_PremB:
                    case ArbiModel.C_ColName_DiscA:
                    case ArbiModel.C_ColName_DiscB:
                        col.DefaultCellStyle.Format = "P2";
                        break;

                    case ArbiModel.C_ColName_OrderValue:
                        col.Visible = false;
                        break;
                    default:
                        break;
                }

                //申购赎回费率
                if (col.Name.Contains(ArbiModel.C_ColName_SubscribeRate) || col.Name.Contains(ArbiModel.C_ColName_RedeemRate))
                    col.DefaultCellStyle.Format = "P2";

                #region 字段名表
                if (col.Name.Contains(ArbiModel.C_ColName_Disc + ArbiModel.C_ColName_Arbi))
                {
                    col.HeaderText = "折" + col.Name.Substring(col.Name.Length - 1, 1);
                    col.HeaderCell.ToolTipText = "折价套利收益率(%)";
                }
                if (col.Name.Contains(ArbiModel.C_ColName_Prem + ArbiModel.C_ColName_Arbi))
                {
                    col.HeaderText = "溢" + col.Name.Substring(col.Name.Length - 1, 1);
                    col.HeaderCell.ToolTipText = "溢价套利收益率(%)";
                }
                if (col.Name.Contains(ArbiModel.C_ColName_Dept))
                {
                    col.HeaderText = "额" + col.Name.Substring(col.Name.Length - 1, 1);
                    col.HeaderCell.ToolTipText = "市场深度（万元）";
                }

                switch (col.Name)
                {
                    case ArbiModel.C_ColName_Category:
                        col.HeaderText = "类别";
                        break;
                    case ArbiModel.C_ColName_Name:
                        col.HeaderText = "基金名称";
                        break;
                    case ArbiModel.C_ColName_Code:
                        col.HeaderText = "代码";
                        break;
                    case ArbiModel.C_ColName_CodeA:
                        col.HeaderText = "A份额";
                        break;
                    case ArbiModel.C_ColName_CodeB:
                        col.HeaderText = "B份额";
                        break;
                    case ArbiModel.C_ColName_ShareARatio:
                        col.HeaderText = "A比例";
                        col.HeaderCell.ToolTipText = "A份额的配对比例";
                        break;
                    case ArbiModel.C_ColName_BenchmarkName:
                        col.HeaderText = "基准名称";
                        break;
                    case ArbiModel.C_ColName_BenchmarkCode:
                        col.HeaderText = "代码";
                        break;
                    case ArbiModel.C_ColName_SubscribeRate:
                        col.HeaderText = "申购费";
                        break;
                    case ArbiModel.C_ColName_RedeemRate:
                        col.HeaderText = "赎回费";
                        break;
                    case ArbiModel.C_ColName_FundedDate:
                        col.HeaderText = "成立日";
                        break;
                    case ArbiModel.C_ColName_AmountA:
                        col.HeaderText = "A成交额";
                        col.HeaderCell.ToolTipText = "A份额的成交额（万元）";
                        break;
                    case ArbiModel.C_ColName_AmountB:
                        col.HeaderText = "B成交额";
                        col.HeaderCell.ToolTipText = "B份额的成交额（万元）";
                        break;
                    case ArbiModel.C_ColName_AmountAB:
                        col.HeaderText = "配对成交额";
                        col.HeaderCell.ToolTipText = "按照配对比例合并的成交额（万元）";
                        break;
                    case ArbiModel.C_ColName_Amount:
                        col.HeaderText = "成交额";
                        break;
                    case ArbiModel.C_ColName_PremA:
                        col.HeaderText = "A溢价率";
                        col.HeaderCell.ToolTipText = "溢价套利中A卖出价对净值(昨收盘)的折溢价率，A份额净值不作实时调整";
                        break;
                    case ArbiModel.C_ColName_PremB:
                        col.HeaderText = "B溢价率";
                        col.HeaderCell.ToolTipText = "溢价套利中B卖出价对净值(实时价)的折溢价率，B份额净值作实时调整";
                        break;
                    case ArbiModel.C_ColName_DiscA:
                        col.HeaderText = "A溢价率";
                        col.HeaderCell.ToolTipText = "折价套利中A买入价对净值(昨收盘)的折溢价率，A份额净值不作实时调整";
                        break;
                    case ArbiModel.C_ColName_DiscB:
                        col.HeaderText = "B溢价率";
                        col.HeaderCell.ToolTipText = "折价套利中B买入价对净值(实时价)的折溢价率，B份额净值作实时调整";
                        break;
                    case ArbiModel.C_ColName_PriceA:
                        col.HeaderText = "A当前价";
                        break;
                    case ArbiModel.C_ColName_PriceB:
                        col.HeaderText = "B当前价";
                        break;
                    case ArbiModel.C_ColName_Price:
                        col.HeaderText = "实时价格";
                        break;
                    case ArbiModel.C_ColName_NAV:
                        col.HeaderText = "实时净值";
                        col.HeaderCell.ToolTipText = "股票指基金根据比较基准指数实时调整的净值";
                        break;
                    case ArbiModel.C_ColName_FundType:
                        col.HeaderText = "基金分类";
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }
        private void refreshViewUI(ViewType type)
        {
            #region Table Columns
            foreach (DataGridViewColumn col in this.dataGridViewMain.Columns)
            {
                //排序及显示
                switch (type)
                {
                    case ViewType.ShowDiscArbi:
                        if (col.Name.Contains(ArbiModel.C_ColName_Disc))
                            col.Visible = true;
                        if (col.Name.Contains(ArbiModel.C_ColName_Prem))
                            col.Visible = false;

                        if (col.Name == ArbiModel.C_ColName_DiscA || col.Name == ArbiModel.C_ColName_DiscB)
                            col.Visible = true;
                        if (col.Name == ArbiModel.C_ColName_PremA || col.Name == ArbiModel.C_ColName_PremB)
                            col.Visible = false;
                        break;

                    case ViewType.ShowPremArbi:
                        if (col.Name.Contains(ArbiModel.C_ColName_Disc))
                            col.Visible = false;
                        if (col.Name.Contains(ArbiModel.C_ColName_Prem))
                            col.Visible = true;

                        if (col.Name == ArbiModel.C_ColName_DiscA || col.Name == ArbiModel.C_ColName_DiscB)
                            col.Visible = false;
                        if (col.Name == ArbiModel.C_ColName_PremA || col.Name == ArbiModel.C_ColName_PremB)
                            col.Visible = true;
                        break;

                    case ViewType.ShowAll:
                        if (col.Name.Contains(ArbiModel.C_ColName_Disc))
                            col.Visible = true;
                        if (col.Name.Contains(ArbiModel.C_ColName_Prem))
                            col.Visible = true;

                        if (col.Name == ArbiModel.C_ColName_DiscA || col.Name == ArbiModel.C_ColName_DiscB)
                            col.Visible = true;
                        if (col.Name == ArbiModel.C_ColName_PremA || col.Name == ArbiModel.C_ColName_PremB)
                            col.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            #endregion            

            #region UI
            switch (this._ViewType)
            {
                case ViewType.ShowDiscArbi:
                    toolStripButtonShowDisc.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    toolStripButtonShowPrem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonShowAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    this.dataGridViewMain.Sort(this.dataGridViewMain.Columns[ArbiModel.C_ColName_Disc + ArbiModel.C_ColName_Arbi + "1"], System.ComponentModel.ListSortDirection.Descending);
                    break;

                case ViewType.ShowPremArbi:
                    toolStripButtonShowDisc.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonShowPrem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    toolStripButtonShowAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    this.dataGridViewMain.Sort(this.dataGridViewMain.Columns[ArbiModel.C_ColName_Prem + ArbiModel.C_ColName_Arbi + "1"], System.ComponentModel.ListSortDirection.Descending);
                    break;

                case ViewType.ShowAll:
                    toolStripButtonShowDisc.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonShowPrem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonShowAll.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    this.dataGridViewMain.Sort(this.dataGridViewMain.Columns[ArbiModel.C_ColName_OrderValue], System.ComponentModel.ListSortDirection.Descending);
                    break;

                default:
                    break;
            }

            switch (this._ObjectType)
            {
                case ObjectType.SOF:
                    toolStripButtonSOF.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    toolStripButtonLOF.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    break;

                case ObjectType.LOF:
                    toolStripButtonSOF.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonLOF.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    toolStripButtonAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    break;

                case ObjectType.ALL:
                    toolStripButtonSOF.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonLOF.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolStripButtonAll.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    break; 

                default:
                    break;
            }
            #endregion
        }
        private void refreshGridView(ViewType view, ObjectType obj)
        {
            this._ViewType = view;
            this._ObjectType = obj;

            switch (obj)
            {
                case ObjectType.SOF:
                    this._Threshold = this._SOFModel.TradeOption.Threshold;
                    this.dataGridViewMain.DataSource = _SOFTable;
                    break;
                case ObjectType.LOF:
                    this._Threshold = this._LOFModel.TradeOption.Threshold;
                    this.dataGridViewMain.DataSource = _LOFTable;
                    break;
                case ObjectType.ALL:
                    this.dataGridViewMain.DataSource = _ALLTable;
                    break;
                default:
                    break;
            }
        }
        private void refreshData()
        {
            this._SOFTable = this._SOFModel.GetData();
            this._LOFTable = this._LOFModel.GetData();
            this._ALLTable = this._ALLModel.GetData(this._SOFTable, this._LOFTable, true);
        }
        private void writelog(string code)
        {
            DataRow[] rows = this._SOFTable.Select(ArbiModel.C_ColName_Code + "='" + code + "'");
            string html = "";
            if (rows.Length > 0)
                html = this._SOFModel.GetLog(code);
            else
                html = this._LOFModel.GetLog(code);

            webBrowserLog.DocumentText += "<P>" + html + "</P>";
            tabControlMain.SelectedTab = tabPageLog;
        }
        #endregion

        #region Attributes
        ArbiModelSOF _SOFModel = null;
        ArbiModelLOF _LOFModel = null;
        ArbiModel _ALLModel = null;
        DataTable _SOFTable = new DataTable();        
        DataTable _LOFTable = new DataTable();
        DataTable _ALLTable = new DataTable();
        #endregion
    }
}
