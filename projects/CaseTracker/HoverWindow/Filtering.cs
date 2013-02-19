using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using FogBugzNet;
using System.Configuration;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private FilterModel _filter = new FilterModel();

        private void ShowFilterDialog()
        {
            Utils.Log.Debug("Showing filter dialog");
            FilterDialog f = new FilterDialog(this, _fb);
            f.LoadModel(_filter);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _filter = f.SaveModel();
                _filter.History.PushSearch(_filter.UserSearch);
                if (_filter.Cases != null)
                    updateCaseDropdown(_filter.Cases);
                else
                    updateCases();

                _filter.History.Save();
            }
            Utils.Log.Debug("Closing filter dialog");
        }

    }



}
