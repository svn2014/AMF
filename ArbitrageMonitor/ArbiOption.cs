using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArbitrageMonitor
{
    interface IOptinos
    { 
        void SaveSettings();
        void LoadSettings();
    }

    class ArbiOption: IOptinos
    {
        public int RefreshFreq = 10000;//毫秒
        public double Commision = 0.0003;

        public bool IsHedge = false;
        public double HedgeIntRate = 0.086;
        public string HedgeCode = "510300";

        public double Threshold = 0.01;
        public bool ShowActiveHybridFunds = false;
        public bool ShowActiveEquityFunds = false;
        public bool ShowActiveBondFunds = true;
        public bool ShowPassiveEquityFunds = true;
        public bool ShowPassiveBondFunds = true;
        public bool ShowOthers = false;

        public void SaveSettings()
        {
            Properties.Settings.Default.RefreshFreq = this.RefreshFreq;
            Properties.Settings.Default.Commision = this.Commision;
            Properties.Settings.Default.IsHedge = this.IsHedge;
            Properties.Settings.Default.HedgeCode = this.HedgeCode;
            Properties.Settings.Default.HedgeIntRate = this.HedgeIntRate;
            Properties.Settings.Default.Threshold = this.Threshold;

            Properties.Settings.Default.ShowActiveBond = this.ShowActiveBondFunds;
            Properties.Settings.Default.ShowActiveEquity = this.ShowActiveEquityFunds;
            Properties.Settings.Default.ShowActiveHybrid = this.ShowActiveHybridFunds;
            Properties.Settings.Default.ShowPassiveBond = this.ShowPassiveBondFunds;
            Properties.Settings.Default.ShowPassiveEquity = this.ShowPassiveEquityFunds;
            Properties.Settings.Default.ShowOthers = this.ShowOthers;

            Properties.Settings.Default.Save();
        }

        public void LoadSettings()
        {
            this.RefreshFreq = Properties.Settings.Default.RefreshFreq;
            this.Commision = Properties.Settings.Default.Commision;
            this.IsHedge = Properties.Settings.Default.IsHedge;
            this.HedgeCode = Properties.Settings.Default.HedgeCode;
            this.HedgeIntRate = Properties.Settings.Default.HedgeIntRate;
            this.Threshold = Properties.Settings.Default.Threshold;
            
            this.ShowActiveBondFunds = Properties.Settings.Default.ShowActiveBond;
            this.ShowActiveEquityFunds = Properties.Settings.Default.ShowActiveEquity;
            this.ShowActiveHybridFunds = Properties.Settings.Default.ShowActiveHybrid;
            this.ShowPassiveBondFunds = Properties.Settings.Default.ShowPassiveBond;
            this.ShowPassiveEquityFunds = Properties.Settings.Default.ShowPassiveEquity;
            this.ShowOthers = Properties.Settings.Default.ShowOthers;
        }
    }
}
