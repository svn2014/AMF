namespace ArbitrageMonitor
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDesc = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPageOption = new System.Windows.Forms.TabPage();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.groupBoxLOF = new System.Windows.Forms.GroupBox();
            this.checkBoxLOF_OT = new System.Windows.Forms.CheckBox();
            this.checkBoxIsHedgeLOF = new System.Windows.Forms.CheckBox();
            this.numericUpDownThresholdLOF = new System.Windows.Forms.NumericUpDown();
            this.checkBoxLOF_AE = new System.Windows.Forms.CheckBox();
            this.checkBoxLOF_AB = new System.Windows.Forms.CheckBox();
            this.checkBoxLOF_PB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxLOF_PE = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxSOF = new System.Windows.Forms.GroupBox();
            this.checkBoxSOF_OT = new System.Windows.Forms.CheckBox();
            this.checkBoxIsHedgeSOF = new System.Windows.Forms.CheckBox();
            this.numericUpDownThresholdSOF = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSOF_AE = new System.Windows.Forms.CheckBox();
            this.checkBoxSOF_AB = new System.Windows.Forms.CheckBox();
            this.checkBoxSOF_PB = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxSOF_PE = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxCommision = new System.Windows.Forms.GroupBox();
            this.numericUpDownRefreshFreq = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownShortIntRate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownCommision = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxShortCode = new System.Windows.Forms.TextBox();
            this.toolStripOption = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSaveEdit = new System.Windows.Forms.ToolStripButton();
            this.tabPageViewArbi = new System.Windows.Forms.TabPage();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStartTimer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonShowDisc = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowPrem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSOF = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLOF = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAll = new System.Windows.Forms.ToolStripButton();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.webBrowserLog = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.backgroundWorkerMain = new System.ComponentModel.BackgroundWorker();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.checkBoxSOF_AH = new System.Windows.Forms.CheckBox();
            this.checkBoxLOF_AH = new System.Windows.Forms.CheckBox();
            this.statusStripMain.SuspendLayout();
            this.tabPageOption.SuspendLayout();
            this.panelOptions.SuspendLayout();
            this.groupBoxLOF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdLOF)).BeginInit();
            this.groupBoxSOF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdSOF)).BeginInit();
            this.groupBoxCommision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRefreshFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShortIntRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommision)).BeginInit();
            this.toolStripOption.SuspendLayout();
            this.tabPageViewArbi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelDesc});
            this.statusStripMain.Location = new System.Drawing.Point(0, 347);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(683, 22);
            this.statusStripMain.TabIndex = 1;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelDesc
            // 
            this.toolStripStatusLabelDesc.Name = "toolStripStatusLabelDesc";
            this.toolStripStatusLabelDesc.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabelDesc.Text = "加载中...";
            // 
            // tabPageOption
            // 
            this.tabPageOption.Controls.Add(this.panelOptions);
            this.tabPageOption.Controls.Add(this.toolStripOption);
            this.tabPageOption.Location = new System.Drawing.Point(4, 21);
            this.tabPageOption.Name = "tabPageOption";
            this.tabPageOption.Size = new System.Drawing.Size(675, 322);
            this.tabPageOption.TabIndex = 3;
            this.tabPageOption.Text = "参数设置";
            this.tabPageOption.UseVisualStyleBackColor = true;
            // 
            // panelOptions
            // 
            this.panelOptions.Controls.Add(this.groupBoxLOF);
            this.panelOptions.Controls.Add(this.groupBoxSOF);
            this.panelOptions.Controls.Add(this.groupBoxCommision);
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOptions.Location = new System.Drawing.Point(0, 25);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(675, 297);
            this.panelOptions.TabIndex = 12;
            // 
            // groupBoxLOF
            // 
            this.groupBoxLOF.Controls.Add(this.checkBoxLOF_AH);
            this.groupBoxLOF.Controls.Add(this.checkBoxLOF_OT);
            this.groupBoxLOF.Controls.Add(this.checkBoxIsHedgeLOF);
            this.groupBoxLOF.Controls.Add(this.numericUpDownThresholdLOF);
            this.groupBoxLOF.Controls.Add(this.checkBoxLOF_AE);
            this.groupBoxLOF.Controls.Add(this.checkBoxLOF_AB);
            this.groupBoxLOF.Controls.Add(this.checkBoxLOF_PB);
            this.groupBoxLOF.Controls.Add(this.label6);
            this.groupBoxLOF.Controls.Add(this.checkBoxLOF_PE);
            this.groupBoxLOF.Controls.Add(this.label9);
            this.groupBoxLOF.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxLOF.Location = new System.Drawing.Point(340, 0);
            this.groupBoxLOF.Name = "groupBoxLOF";
            this.groupBoxLOF.Size = new System.Drawing.Size(170, 297);
            this.groupBoxLOF.TabIndex = 13;
            this.groupBoxLOF.TabStop = false;
            this.groupBoxLOF.Text = "交易基金";
            // 
            // checkBoxLOF_OT
            // 
            this.checkBoxLOF_OT.AutoSize = true;
            this.checkBoxLOF_OT.Location = new System.Drawing.Point(35, 166);
            this.checkBoxLOF_OT.Name = "checkBoxLOF_OT";
            this.checkBoxLOF_OT.Size = new System.Drawing.Size(72, 16);
            this.checkBoxLOF_OT.TabIndex = 21;
            this.checkBoxLOF_OT.Text = "其他品种";
            this.checkBoxLOF_OT.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsHedgeLOF
            // 
            this.checkBoxIsHedgeLOF.AutoSize = true;
            this.checkBoxIsHedgeLOF.Checked = true;
            this.checkBoxIsHedgeLOF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsHedgeLOF.Location = new System.Drawing.Point(8, 199);
            this.checkBoxIsHedgeLOF.Name = "checkBoxIsHedgeLOF";
            this.checkBoxIsHedgeLOF.Size = new System.Drawing.Size(132, 16);
            this.checkBoxIsHedgeLOF.TabIndex = 20;
            this.checkBoxIsHedgeLOF.Text = "使用融券对冲股票型";
            this.checkBoxIsHedgeLOF.UseVisualStyleBackColor = true;
            // 
            // numericUpDownThresholdLOF
            // 
            this.numericUpDownThresholdLOF.DecimalPlaces = 3;
            this.numericUpDownThresholdLOF.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownThresholdLOF.Location = new System.Drawing.Point(65, 14);
            this.numericUpDownThresholdLOF.Name = "numericUpDownThresholdLOF";
            this.numericUpDownThresholdLOF.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownThresholdLOF.TabIndex = 16;
            this.numericUpDownThresholdLOF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownThresholdLOF.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // checkBoxLOF_AE
            // 
            this.checkBoxLOF_AE.AutoSize = true;
            this.checkBoxLOF_AE.Location = new System.Drawing.Point(35, 100);
            this.checkBoxLOF_AE.Name = "checkBoxLOF_AE";
            this.checkBoxLOF_AE.Size = new System.Drawing.Size(90, 16);
            this.checkBoxLOF_AE.TabIndex = 15;
            this.checkBoxLOF_AE.Text = "主动-股票型";
            this.checkBoxLOF_AE.UseVisualStyleBackColor = true;
            // 
            // checkBoxLOF_AB
            // 
            this.checkBoxLOF_AB.AutoSize = true;
            this.checkBoxLOF_AB.Checked = true;
            this.checkBoxLOF_AB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLOF_AB.Location = new System.Drawing.Point(35, 122);
            this.checkBoxLOF_AB.Name = "checkBoxLOF_AB";
            this.checkBoxLOF_AB.Size = new System.Drawing.Size(90, 16);
            this.checkBoxLOF_AB.TabIndex = 14;
            this.checkBoxLOF_AB.Text = "主动-债券型";
            this.checkBoxLOF_AB.UseVisualStyleBackColor = true;
            // 
            // checkBoxLOF_PB
            // 
            this.checkBoxLOF_PB.AutoSize = true;
            this.checkBoxLOF_PB.Checked = true;
            this.checkBoxLOF_PB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLOF_PB.Location = new System.Drawing.Point(35, 78);
            this.checkBoxLOF_PB.Name = "checkBoxLOF_PB";
            this.checkBoxLOF_PB.Size = new System.Drawing.Size(90, 16);
            this.checkBoxLOF_PB.TabIndex = 13;
            this.checkBoxLOF_PB.Text = "被动-债券型";
            this.checkBoxLOF_PB.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "参与品种";
            // 
            // checkBoxLOF_PE
            // 
            this.checkBoxLOF_PE.AutoSize = true;
            this.checkBoxLOF_PE.Checked = true;
            this.checkBoxLOF_PE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLOF_PE.Location = new System.Drawing.Point(35, 56);
            this.checkBoxLOF_PE.Name = "checkBoxLOF_PE";
            this.checkBoxLOF_PE.Size = new System.Drawing.Size(90, 16);
            this.checkBoxLOF_PE.TabIndex = 11;
            this.checkBoxLOF_PE.Text = "被动-股票型";
            this.checkBoxLOF_PE.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "套利阈值";
            // 
            // groupBoxSOF
            // 
            this.groupBoxSOF.Controls.Add(this.checkBoxSOF_AH);
            this.groupBoxSOF.Controls.Add(this.checkBoxSOF_OT);
            this.groupBoxSOF.Controls.Add(this.checkBoxIsHedgeSOF);
            this.groupBoxSOF.Controls.Add(this.numericUpDownThresholdSOF);
            this.groupBoxSOF.Controls.Add(this.checkBoxSOF_AE);
            this.groupBoxSOF.Controls.Add(this.checkBoxSOF_AB);
            this.groupBoxSOF.Controls.Add(this.checkBoxSOF_PB);
            this.groupBoxSOF.Controls.Add(this.label8);
            this.groupBoxSOF.Controls.Add(this.checkBoxSOF_PE);
            this.groupBoxSOF.Controls.Add(this.label2);
            this.groupBoxSOF.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxSOF.Location = new System.Drawing.Point(170, 0);
            this.groupBoxSOF.Name = "groupBoxSOF";
            this.groupBoxSOF.Size = new System.Drawing.Size(170, 297);
            this.groupBoxSOF.TabIndex = 12;
            this.groupBoxSOF.TabStop = false;
            this.groupBoxSOF.Text = "分级基金";
            // 
            // checkBoxSOF_OT
            // 
            this.checkBoxSOF_OT.AutoSize = true;
            this.checkBoxSOF_OT.Location = new System.Drawing.Point(35, 166);
            this.checkBoxSOF_OT.Name = "checkBoxSOF_OT";
            this.checkBoxSOF_OT.Size = new System.Drawing.Size(72, 16);
            this.checkBoxSOF_OT.TabIndex = 22;
            this.checkBoxSOF_OT.Text = "其他品种";
            this.checkBoxSOF_OT.UseVisualStyleBackColor = true;
            // 
            // checkBoxIsHedgeSOF
            // 
            this.checkBoxIsHedgeSOF.AutoSize = true;
            this.checkBoxIsHedgeSOF.Checked = true;
            this.checkBoxIsHedgeSOF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsHedgeSOF.Location = new System.Drawing.Point(6, 199);
            this.checkBoxIsHedgeSOF.Name = "checkBoxIsHedgeSOF";
            this.checkBoxIsHedgeSOF.Size = new System.Drawing.Size(132, 16);
            this.checkBoxIsHedgeSOF.TabIndex = 20;
            this.checkBoxIsHedgeSOF.Text = "使用融券对冲股票型";
            this.checkBoxIsHedgeSOF.UseVisualStyleBackColor = true;
            // 
            // numericUpDownThresholdSOF
            // 
            this.numericUpDownThresholdSOF.DecimalPlaces = 3;
            this.numericUpDownThresholdSOF.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownThresholdSOF.Location = new System.Drawing.Point(65, 14);
            this.numericUpDownThresholdSOF.Name = "numericUpDownThresholdSOF";
            this.numericUpDownThresholdSOF.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownThresholdSOF.TabIndex = 16;
            this.numericUpDownThresholdSOF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownThresholdSOF.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // checkBoxSOF_AE
            // 
            this.checkBoxSOF_AE.AutoSize = true;
            this.checkBoxSOF_AE.Location = new System.Drawing.Point(35, 100);
            this.checkBoxSOF_AE.Name = "checkBoxSOF_AE";
            this.checkBoxSOF_AE.Size = new System.Drawing.Size(90, 16);
            this.checkBoxSOF_AE.TabIndex = 15;
            this.checkBoxSOF_AE.Text = "主动-股票型";
            this.checkBoxSOF_AE.UseVisualStyleBackColor = true;
            // 
            // checkBoxSOF_AB
            // 
            this.checkBoxSOF_AB.AutoSize = true;
            this.checkBoxSOF_AB.Checked = true;
            this.checkBoxSOF_AB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSOF_AB.Location = new System.Drawing.Point(35, 122);
            this.checkBoxSOF_AB.Name = "checkBoxSOF_AB";
            this.checkBoxSOF_AB.Size = new System.Drawing.Size(90, 16);
            this.checkBoxSOF_AB.TabIndex = 14;
            this.checkBoxSOF_AB.Text = "主动-债券型";
            this.checkBoxSOF_AB.UseVisualStyleBackColor = true;
            // 
            // checkBoxSOF_PB
            // 
            this.checkBoxSOF_PB.AutoSize = true;
            this.checkBoxSOF_PB.Checked = true;
            this.checkBoxSOF_PB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSOF_PB.Location = new System.Drawing.Point(35, 78);
            this.checkBoxSOF_PB.Name = "checkBoxSOF_PB";
            this.checkBoxSOF_PB.Size = new System.Drawing.Size(90, 16);
            this.checkBoxSOF_PB.TabIndex = 13;
            this.checkBoxSOF_PB.Text = "被动-债券型";
            this.checkBoxSOF_PB.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "参与品种";
            // 
            // checkBoxSOF_PE
            // 
            this.checkBoxSOF_PE.AutoSize = true;
            this.checkBoxSOF_PE.Checked = true;
            this.checkBoxSOF_PE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSOF_PE.Location = new System.Drawing.Point(35, 56);
            this.checkBoxSOF_PE.Name = "checkBoxSOF_PE";
            this.checkBoxSOF_PE.Size = new System.Drawing.Size(90, 16);
            this.checkBoxSOF_PE.TabIndex = 11;
            this.checkBoxSOF_PE.Text = "被动-股票型";
            this.checkBoxSOF_PE.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "套利阈值";
            // 
            // groupBoxCommision
            // 
            this.groupBoxCommision.Controls.Add(this.numericUpDownRefreshFreq);
            this.groupBoxCommision.Controls.Add(this.label5);
            this.groupBoxCommision.Controls.Add(this.label4);
            this.groupBoxCommision.Controls.Add(this.numericUpDownShortIntRate);
            this.groupBoxCommision.Controls.Add(this.label3);
            this.groupBoxCommision.Controls.Add(this.label1);
            this.groupBoxCommision.Controls.Add(this.numericUpDownCommision);
            this.groupBoxCommision.Controls.Add(this.label7);
            this.groupBoxCommision.Controls.Add(this.textBoxShortCode);
            this.groupBoxCommision.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxCommision.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCommision.Name = "groupBoxCommision";
            this.groupBoxCommision.Size = new System.Drawing.Size(170, 297);
            this.groupBoxCommision.TabIndex = 11;
            this.groupBoxCommision.TabStop = false;
            this.groupBoxCommision.Text = "通用";
            // 
            // numericUpDownRefreshFreq
            // 
            this.numericUpDownRefreshFreq.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownRefreshFreq.Location = new System.Drawing.Point(71, 16);
            this.numericUpDownRefreshFreq.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownRefreshFreq.Name = "numericUpDownRefreshFreq";
            this.numericUpDownRefreshFreq.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownRefreshFreq.TabIndex = 15;
            this.numericUpDownRefreshFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownRefreshFreq.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "刷新频率";
            // 
            // numericUpDownShortIntRate
            // 
            this.numericUpDownShortIntRate.DecimalPlaces = 4;
            this.numericUpDownShortIntRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownShortIntRate.Location = new System.Drawing.Point(71, 92);
            this.numericUpDownShortIntRate.Name = "numericUpDownShortIntRate";
            this.numericUpDownShortIntRate.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownShortIntRate.TabIndex = 14;
            this.numericUpDownShortIntRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownShortIntRate.Value = new decimal(new int[] {
            86,
            0,
            0,
            196608});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "融券年率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "佣金费率";
            // 
            // numericUpDownCommision
            // 
            this.numericUpDownCommision.DecimalPlaces = 4;
            this.numericUpDownCommision.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownCommision.Location = new System.Drawing.Point(71, 64);
            this.numericUpDownCommision.Name = "numericUpDownCommision";
            this.numericUpDownCommision.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownCommision.TabIndex = 13;
            this.numericUpDownCommision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownCommision.Value = new decimal(new int[] {
            3,
            0,
            0,
            262144});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "融券标的";
            // 
            // textBoxShortCode
            // 
            this.textBoxShortCode.Location = new System.Drawing.Point(71, 119);
            this.textBoxShortCode.Name = "textBoxShortCode";
            this.textBoxShortCode.Size = new System.Drawing.Size(60, 21);
            this.textBoxShortCode.TabIndex = 9;
            this.textBoxShortCode.Text = "510300";
            // 
            // toolStripOption
            // 
            this.toolStripOption.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSaveEdit});
            this.toolStripOption.Location = new System.Drawing.Point(0, 0);
            this.toolStripOption.Name = "toolStripOption";
            this.toolStripOption.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripOption.Size = new System.Drawing.Size(675, 25);
            this.toolStripOption.TabIndex = 13;
            this.toolStripOption.Text = "toolStrip1";
            // 
            // toolStripButtonSaveEdit
            // 
            this.toolStripButtonSaveEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveEdit.Image")));
            this.toolStripButtonSaveEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveEdit.Name = "toolStripButtonSaveEdit";
            this.toolStripButtonSaveEdit.Size = new System.Drawing.Size(49, 22);
            this.toolStripButtonSaveEdit.Text = "编辑";
            this.toolStripButtonSaveEdit.Click += new System.EventHandler(this.toolStripButtonSaveEdit_Click);
            // 
            // tabPageViewArbi
            // 
            this.tabPageViewArbi.Controls.Add(this.dataGridViewMain);
            this.tabPageViewArbi.Controls.Add(this.toolStripMain);
            this.tabPageViewArbi.Location = new System.Drawing.Point(4, 21);
            this.tabPageViewArbi.Name = "tabPageViewArbi";
            this.tabPageViewArbi.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageViewArbi.Size = new System.Drawing.Size(675, 322);
            this.tabPageViewArbi.TabIndex = 1;
            this.tabPageViewArbi.Text = "套利观察";
            this.tabPageViewArbi.UseVisualStyleBackColor = true;
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            this.dataGridViewMain.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridViewMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMain.Location = new System.Drawing.Point(3, 28);
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.ReadOnly = true;
            this.dataGridViewMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridViewMain.RowTemplate.Height = 23;
            this.dataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewMain.Size = new System.Drawing.Size(669, 291);
            this.dataGridViewMain.TabIndex = 0;
            this.dataGridViewMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMain_CellDoubleClick);
            this.dataGridViewMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMain_CellFormatting);
            // 
            // toolStripMain
            // 
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRefresh,
            this.toolStripButtonStartTimer,
            this.toolStripSeparator2,
            this.toolStripButtonShowDisc,
            this.toolStripButtonShowPrem,
            this.toolStripButtonShowAll,
            this.toolStripSeparator3,
            this.toolStripButtonSOF,
            this.toolStripButtonLOF,
            this.toolStripButtonAll});
            this.toolStripMain.Location = new System.Drawing.Point(3, 3);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripMain.Size = new System.Drawing.Size(669, 25);
            this.toolStripMain.TabIndex = 3;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(49, 22);
            this.toolStripButtonRefresh.Text = "刷新";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonStartTimer
            // 
            this.toolStripButtonStartTimer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStartTimer.Image")));
            this.toolStripButtonStartTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStartTimer.Name = "toolStripButtonStartTimer";
            this.toolStripButtonStartTimer.Size = new System.Drawing.Size(49, 22);
            this.toolStripButtonStartTimer.Text = "自动";
            this.toolStripButtonStartTimer.Click += new System.EventHandler(this.toolStripButtonStartTimer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonShowDisc
            // 
            this.toolStripButtonShowDisc.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowDisc.Image")));
            this.toolStripButtonShowDisc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowDisc.Name = "toolStripButtonShowDisc";
            this.toolStripButtonShowDisc.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonShowDisc.Text = "折价套利";
            this.toolStripButtonShowDisc.ToolTipText = "折价套利";
            this.toolStripButtonShowDisc.Click += new System.EventHandler(this.toolStripButtonShowDisc_Click);
            // 
            // toolStripButtonShowPrem
            // 
            this.toolStripButtonShowPrem.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowPrem.Image")));
            this.toolStripButtonShowPrem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowPrem.Name = "toolStripButtonShowPrem";
            this.toolStripButtonShowPrem.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonShowPrem.Text = "溢价套利";
            this.toolStripButtonShowPrem.ToolTipText = "溢价套利";
            this.toolStripButtonShowPrem.Click += new System.EventHandler(this.toolStripButtonShowPrem_Click);
            // 
            // toolStripButtonShowAll
            // 
            this.toolStripButtonShowAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowAll.Image")));
            this.toolStripButtonShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowAll.Name = "toolStripButtonShowAll";
            this.toolStripButtonShowAll.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonShowAll.Text = "全部套利";
            this.toolStripButtonShowAll.Click += new System.EventHandler(this.toolStripButtonShowAll_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSOF
            // 
            this.toolStripButtonSOF.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSOF.Image")));
            this.toolStripButtonSOF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSOF.Name = "toolStripButtonSOF";
            this.toolStripButtonSOF.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonSOF.Text = "分级基金";
            this.toolStripButtonSOF.Click += new System.EventHandler(this.toolStripButtonSOF_Click);
            // 
            // toolStripButtonLOF
            // 
            this.toolStripButtonLOF.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLOF.Image")));
            this.toolStripButtonLOF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLOF.Name = "toolStripButtonLOF";
            this.toolStripButtonLOF.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonLOF.Text = "交易基金";
            this.toolStripButtonLOF.Click += new System.EventHandler(this.toolStripButtonLOF_Click);
            // 
            // toolStripButtonAll
            // 
            this.toolStripButtonAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAll.Image")));
            this.toolStripButtonAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAll.Name = "toolStripButtonAll";
            this.toolStripButtonAll.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonAll.Text = "全部品种";
            this.toolStripButtonAll.Click += new System.EventHandler(this.toolStripButtonAll_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageViewArbi);
            this.tabControlMain.Controls.Add(this.tabPageOption);
            this.tabControlMain.Controls.Add(this.tabPageLog);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(683, 347);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.webBrowserLog);
            this.tabPageLog.Controls.Add(this.toolStrip1);
            this.tabPageLog.Location = new System.Drawing.Point(4, 21);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(675, 322);
            this.tabPageLog.TabIndex = 4;
            this.tabPageLog.Text = "交易流水";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // webBrowserLog
            // 
            this.webBrowserLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserLog.Location = new System.Drawing.Point(0, 25);
            this.webBrowserLog.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserLog.Name = "webBrowserLog";
            this.webBrowserLog.Size = new System.Drawing.Size(675, 297);
            this.webBrowserLog.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(675, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClear.Image")));
            this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Size = new System.Drawing.Size(49, 22);
            this.toolStripButtonClear.Text = "清除";
            this.toolStripButtonClear.Click += new System.EventHandler(this.toolStripButtonClear_Click);
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.WorkerReportsProgress = true;
            this.backgroundWorkerMain.WorkerSupportsCancellation = true;
            this.backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMain_DoWork);
            this.backgroundWorkerMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMain_ProgressChanged);
            this.backgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMain_RunWorkerCompleted);
            // 
            // timerMain
            // 
            this.timerMain.Interval = 5000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // checkBoxSOF_AH
            // 
            this.checkBoxSOF_AH.AutoSize = true;
            this.checkBoxSOF_AH.Location = new System.Drawing.Point(35, 144);
            this.checkBoxSOF_AH.Name = "checkBoxSOF_AH";
            this.checkBoxSOF_AH.Size = new System.Drawing.Size(90, 16);
            this.checkBoxSOF_AH.TabIndex = 23;
            this.checkBoxSOF_AH.Text = "主动-混合型";
            this.checkBoxSOF_AH.UseVisualStyleBackColor = true;
            // 
            // checkBoxLOF_AH
            // 
            this.checkBoxLOF_AH.AutoSize = true;
            this.checkBoxLOF_AH.Location = new System.Drawing.Point(35, 144);
            this.checkBoxLOF_AH.Name = "checkBoxLOF_AH";
            this.checkBoxLOF_AH.Size = new System.Drawing.Size(90, 16);
            this.checkBoxLOF_AH.TabIndex = 24;
            this.checkBoxLOF_AH.Text = "主动-混合型";
            this.checkBoxLOF_AH.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 369);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStripMain);
            this.Name = "FormMain";
            this.Text = "套利观察";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.tabPageOption.ResumeLayout(false);
            this.tabPageOption.PerformLayout();
            this.panelOptions.ResumeLayout(false);
            this.groupBoxLOF.ResumeLayout(false);
            this.groupBoxLOF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdLOF)).EndInit();
            this.groupBoxSOF.ResumeLayout(false);
            this.groupBoxSOF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdSOF)).EndInit();
            this.groupBoxCommision.ResumeLayout(false);
            this.groupBoxCommision.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRefreshFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShortIntRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCommision)).EndInit();
            this.toolStripOption.ResumeLayout(false);
            this.toolStripOption.PerformLayout();
            this.tabPageViewArbi.ResumeLayout(false);
            this.tabPageViewArbi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.TabPage tabPageOption;
        private System.Windows.Forms.Panel panelOptions;
        private System.Windows.Forms.GroupBox groupBoxSOF;
        private System.Windows.Forms.CheckBox checkBoxSOF_AE;
        private System.Windows.Forms.CheckBox checkBoxSOF_AB;
        private System.Windows.Forms.CheckBox checkBoxSOF_PB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxSOF_PE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxCommision;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxShortCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageViewArbi;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowDisc;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowPrem;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.NumericUpDown numericUpDownRefreshFreq;
        private System.Windows.Forms.NumericUpDown numericUpDownShortIntRate;
        private System.Windows.Forms.NumericUpDown numericUpDownCommision;
        private System.Windows.Forms.NumericUpDown numericUpDownThresholdSOF;
        private System.Windows.Forms.CheckBox checkBoxIsHedgeSOF;
        private System.Windows.Forms.ToolStrip toolStripOption;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveEdit;
        private System.Windows.Forms.GroupBox groupBoxLOF;
        private System.Windows.Forms.CheckBox checkBoxIsHedgeLOF;
        private System.Windows.Forms.NumericUpDown numericUpDownThresholdLOF;
        private System.Windows.Forms.CheckBox checkBoxLOF_AE;
        private System.Windows.Forms.CheckBox checkBoxLOF_AB;
        private System.Windows.Forms.CheckBox checkBoxLOF_PB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxLOF_PE;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxLOF_OT;
        private System.Windows.Forms.CheckBox checkBoxSOF_OT;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDesc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonSOF;
        private System.Windows.Forms.ToolStripButton toolStripButtonLOF;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowAll;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonStartTimer;
        private System.Windows.Forms.ToolStripButton toolStripButtonAll;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.WebBrowser webBrowserLog;
        private System.Windows.Forms.ToolStripButton toolStripButtonClear;
        private System.Windows.Forms.CheckBox checkBoxLOF_AH;
        private System.Windows.Forms.CheckBox checkBoxSOF_AH;
    }
}

