using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace FogBugzCaseTracker
{
    public class CultureAwareBackgroundWorker : BackgroundWorker
    {
        private System.Globalization.CultureInfo _culture;
        
        public CultureAwareBackgroundWorker()
        {
            _culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
            base.OnDoWork(e);
        }

    }
}
