using System;
using System.Collections.Generic;
using System.Text;
using FogBugzNet;
using System.Windows.Forms;
using System.ComponentModel;
using System.Configuration;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private Case[] _cases;
        private void updateCases()
        {
            updateCases(false);
        }

        private void updateCaseDropdown(Case[] cases)
        {
            _cases = cases;
            dropCaseList.Items.Clear();
            RepopulateCaseDropdown();
            UpdateStateAccordingToTracking();
        }

        private void updateCases(bool failSilently)
        {
            Utils.Log.DebugFormat("Updating case list (fail silently: {0})", failSilently);
            SetState(new StateUpdatingCases(this));
            Application.DoEvents();

            string search = _filter.FormatSearchQuery();

            GetCasesAsync(search, delegate(Case[] cases, Exception error)
            {
                try
                {
                    if (error != null)
                        throw error;
                    updateCaseDropdown(cases);
                }
                catch (ECommandFailed e)
                {
                    if (e.ErrorCode == (int)ECommandFailed.Code.InvalidSearch)
                    {
                        _filter.UserSearch = ConfigurationManager.AppSettings["DefaultNarrowSearch"];
                        Utils.Log.WarnFormat("Invalid search failed: {0}, reverting to default search: {1}", search, _filter.UserSearch);
                        updateCases(failSilently);
                    }
                    if (!failSilently)
                        throw;
                }
                catch (Exception)
                {
                    SetState(new StateRetryLogin(this));
                    if (!failSilently)
                        throw;
                }
            });
        }


        private void RepopulateCaseDropdown()
        {
            Utils.Log.Debug("Repopulating case drop-down...");
            dropCaseList.Items.Add("(nothing)");
            dropCaseList.Text = "(nothing)";
            foreach (Case c in _cases)
            {
                Application.DoEvents();
                dropCaseList.Items.Add(c);
            }
        }


        public delegate void OnCasesFetched(Case[] cases, Exception error);

        public void GetCasesAsync(string search, OnCasesFetched OnDone)
        {
            BackgroundWorker bw = new CultureAwareBackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(delegate(object sender, DoWorkEventArgs args)
            {
                args.Result = _fb.GetCases(search);
            });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object sender, RunWorkerCompletedEventArgs args)
            {
                if (args.Error != null)
                    OnDone(null, args.Error);
                else
                    OnDone((Case[])args.Result, null);
            });
            bw.RunWorkerAsync();
        }

        public string FormatCurrentCaseToolTip()
        {
            Case c = (Case)dropCaseList.SelectedItem;

            double pctDone = c.Estimate.TotalHours > 0 ? (c.Elapsed.TotalHours / c.Estimate.TotalHours ) : 1;

            return String.Format("Working on: {0} ({1}/{2}, {3}% )", dropCaseList.Text, c.ElapsedTime_h_m, c.EstimatedTime_h_m, (int)(pctDone * 100));
        }


    }
}
