using System;
using System.Collections.Generic;
using System.Text;
using FogBugzNet;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private void SetState(object state)
        {
            Utils.Log.Debug("Entering state: " + state.GetType().ToString());
            _currentState = state;
            Refresh();
        }

        private class StateLoggedOff
        {
            public StateLoggedOff(HoverWindow frm)
            {
                frm.btnMain.Enabled = true;
                frm.dropCaseList.Text = "(please login)";
                frm.dropCaseList.Enabled = false;
                frm.btnFilter.Enable(false);
                frm.btnRefresh.Enable(false);
                frm.btnNewCase.Enable(false);
                frm.btnNewSubcase.Enable(false);
                frm.menuNew.Enabled = false;
                frm.menuCurrentFilter.Enabled = false;
                frm.menuCurrentCase.Enabled = false;
                frm.btnResolve.Enable(false);
                frm.btnViewCaseOutline.Enable(false);
                frm.btnViewCase.Enable(false);
                frm.btnNewEstimate.Enable(false);
                frm.btnResolveClose.Enable(false);
                frm.timerUpdateCases.Enabled = false;
                frm.btnExportFreeMind.Enable(false);
                frm.btnExportExcel.Enable(false);
                frm.busyPicture.Visible = false;
                frm.btnPause.Enable(false);
                frm.btnStop.Enable(false);
                frm.pnlPaused.Visible = false;
                frm.btnSettings.Enable(false);


            }
        };


        private class StateRetryLogin : StateLoggedOff
        {
            public StateRetryLogin(HoverWindow frm)
                : base(frm)
            {
                frm.dropCaseList.Text = "(FogBugz server disconnection)";
                frm.timerRetryLogin.Enabled = true;
            }
        };


        private class StateLoggingIn : StateLoggedOff
        {
            public StateLoggingIn(HoverWindow frm)
                : base(frm)
            {
                frm.btnMain.Enabled = true;
                frm.timerRetryLogin.Enabled = false;
                frm.busyPicture.Visible = true;


            }
        };

        private class StateLoggedIn : StateLoggingIn
        {
            public StateLoggedIn(HoverWindow frm)
                : base(frm)
            {
                frm.menuNew.Enabled = true;
                frm.menuCurrentFilter.Enabled = true;
                frm.dropCaseList.Enabled = true;
                frm.btnFilter.Enable(true);
                frm.btnRefresh.Enable(true);
                frm.btnNewCase.Enable(true);
                frm.timerUpdateCases.Enabled = true;
                frm.btnMain.Enabled = true;
                frm.btnExportFreeMind.Enable(true);
                frm.btnExportExcel.Enable(true);
                frm.busyPicture.Visible = false;
                frm.btnSettings.Enable(true);
                frm.tooltipCurrentCase.SetToolTip(frm.dropCaseList, "");
   
            }
        };


        private class StateUpdatingCases : StateLoggedOff
        {
            public StateUpdatingCases(HoverWindow frm)
                : base(frm)
            {
                frm.dropCaseList.Text = "(Updating cases...)";
                frm.btnMain.Enabled = false;
                frm.timerUpdateCases.Enabled = false;
                frm.dropCaseList.Enabled = false;
                frm.timerRetryLogin.Enabled = false;
                frm.busyPicture.Visible = true;

            }
        };

        private class StateTrackingCase : StateLoggedIn
        {
            public StateTrackingCase(HoverWindow frm)
                : base(frm)
            {
                frm.btnResolve.Enable(true);

                frm.menuCurrentCase.Enabled = true;
                frm.btnViewCaseOutline.Enable(true);
                frm.btnViewCase.Enable(true);
                frm.btnNewEstimate.Enable(true);
                frm.btnResolveClose.Enable(true);

                frm.tooltipCurrentCase.SetToolTip(frm.dropCaseList, frm.FormatCurrentCaseToolTip());
                frm.timerUpdateCases.Enabled = true;
                frm.busyPicture.Visible = false;
                frm.btnPause.Enable(true);
                frm.btnStop.Enable(true);
                frm.btnNewSubcase.Enable(true);
            }
        };


        private class StatePaused : StateTrackingCase
        {
            public StatePaused(HoverWindow frm)
                : base(frm)
            {
                frm.btnResolve.Enable(false);
                frm.menuNew.Enable(true);
                frm.menuCurrentFilter.Enabled = false;
                frm.menuCurrentCase.Enabled = false;

                frm.btnViewCaseOutline.Enable(false);
                frm.btnViewCase.Enable(false);
                frm.btnNewEstimate.Enable(false);
                frm.btnResolveClose.Enable(false);
                frm.tooltipCurrentCase.SetToolTip(frm.dropCaseList,
                    String.Format("[PAUSED] Working on: {0} (elapsed time: {1})", frm.dropCaseList.Text, ((Case)frm.dropCaseList.SelectedItem).ElapsedTime_h_m));
                frm.timerUpdateCases.Enabled = false;
                frm.busyPicture.Visible = false;
                frm.btnPause.Enable(false);
                frm.btnStop.Enable(false);
                frm.pnlPaused.Visible = true;
                frm.pnlPaused.Top = 1;
                frm.pnlPaused.Left = 1;
                frm.pnlPaused.Width = frm.Width - 2;
                frm.pnlPaused.Height = frm.Height - 2;
                frm.lblImBack.Top = (frm.pnlPaused.Height - frm.lblImBack.Height ) / 2;
                frm.btnNewCase.Enable(true);
                frm.btnExportExcel.Enable(false);
                frm.btnExportFreeMind.Enable(false);
                frm.btnImportFreeMind.Enable(false);
                frm.btnNewSubcase.Enable(true);
            }
        };

    }
}
