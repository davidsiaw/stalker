using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FogBugzNet;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow : Form
    {

        private void Initialize()
        {
            loadSettings();

            SetState(new StateLoggedOff(this));

            trayIcon.ShowBalloonTip(2000);

            _autoUpdate = new AutoUpdater(ConfigurationManager.AppSettings["AutoUpdateURL"],
                                            new TimeSpan(int.Parse(ConfigurationManager.AppSettings["VersionUpdateCheckIntervalHours"]), 0, 0));

            _autoUpdate.Run();

            MoveWindowToCenter();

            loginWithPrompt();
        }

        private void updateCasesTimer_Click(object sender, EventArgs e)
        {
            if (_fb.IsLoggedIn)
                updateCases(true);
        }

        private void HoverWindow_Load(object sender, EventArgs e)
        {
            Initialize();
        }


        private void HoverWindow_MouseDown(object sender, MouseEventArgs e)
        {
            startDragging(e);
        }

        private void HoverWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
                dragWindow(e);
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblWorkingOn_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
                dragWindow(e);
        }

        private void lblWorkingOn_MouseDown(object sender, MouseEventArgs e)
        {
            startDragging(e);
        }

        private void listCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTrackedItem();
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                menuMain.Show();
        }

        private void trayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = !Visible;
                btnShowHide.Text = Visible ? "Hide" : "Show"; // TODO: should be handled by the state classes
            }

        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            loginWithPrompt(true);
        }

        private void HoverWindow_FormClosed(object sender, FormClosedEventArgs e)
        {

            CloseApplication();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            updateCases();
            backgroundPic.Focus();
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            _fb.ResolveCase(TrackedCase.ID);
            updateCases();
        }

        private void btnViewCase_Click(object sender, EventArgs e)
        {
            Process.Start(_fb.CaseEditURL(((Case)dropCaseList.SelectedItem).ID));
        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {
            Visible = !Visible;
            btnShowHide.Text = Visible ? "Hide" : "Show";
        }

        private void listCases_DropDown(object sender, EventArgs e)
        {
            timerUpdateCases.Enabled = false;
        }

        private void listCases_DropDownClosed(object sender, EventArgs e)
        {
            timerUpdateCases.Enabled = true;
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            timerUpdateCases.Enabled = false;
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            timerUpdateCases.Enabled = true;
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            ShowMainMenu();

        }

        private void ShowMainMenu()
        {
            Point p = new Point(Location.X + btnMain.Location.X,
                                Location.Y + btnMain.Location.Y + btnMain.Height);
            menuMain.Show(p);
            backgroundPic.Focus();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ShowFilterDialog();
            backgroundPic.Focus();
        }

        private void grip_MouseDown(object sender, MouseEventArgs e)
        {
            _resizing = true;
            _gripStartX = Cursor.Position.X;
        }

        private void grip_MouseUp(object sender, MouseEventArgs e)
        {
            _resizing = false;

        }

        private void grip_MouseMove(object sender, MouseEventArgs e)
        {
            if (_resizing)
                ResizeWidth();

        }

        private void btnResolveClose_Click(object sender, EventArgs e)
        {
            _fb.ResolveCase(TrackedCase.ID);
            updateCases();
        }

        private void HoverWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void backgroundPic_MouseDown(object sender, MouseEventArgs e)
        {
            startDragging(e);
        }

        private void backgroundPic_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
                dragWindow(e);
        }

        private void backgroundPic_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void backgroundPic_Click(object sender, EventArgs e)
        {

        }

        private void btnNewCase_Click(object sender, EventArgs e)
        {
            Process.Start(_fb.NewCaseURL);

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();

        }

        private void timerRetryLogin_Tick(object sender, EventArgs e)
        {

            RetryLogin();

        }

        private void btnExportFreeMind_Click(object sender, EventArgs e)
        {
            ExportToFreeMind();
        }

        
        private void btnImportFreeMind_Click(object sender, EventArgs e)
        {
            DoImport();
            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseWork();
            backgroundPic.Focus();
        }

        private void lblImBack_Click(object sender, EventArgs e)
        {
            if (!_dragging)
                ResumeWork();
            backgroundPic.Focus();
        }

        private void btnNewSubcase_Click(object sender, EventArgs e)
        {
            Process.Start(_fb.NewSubCaseURL(_trackedCase.ID));
        }

        private void btnViewCaseOutline_Click(object sender, EventArgs e)
        {
            Process.Start(_fb.ViewOutlineURL(_trackedCase.ID));
        }

        private void btnNewEstimate_Click(object sender, EventArgs e)
        {
            ObtainUserEstimate(_trackedCase.ID);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDlg dlg = new AboutDlg();
            dlg.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

            ShowSettingsDialog();
        }

        private bool UserIsAway()
        {
            if (_settings.MinutesBeforeAway == 0)
                return false;
            return Interop.GetTimeSinceLastInput().TotalMinutes > _settings.MinutesBeforeAway;
        }

        private void timerAway_Tick(object sender, EventArgs e)
        {
            if ((_currentState.GetType() == typeof(StateTrackingCase)) && UserIsAway())
                PauseWork();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopWork();
            backgroundPic.Focus();

        }

        private void pnlPaused_MouseDown(object sender, MouseEventArgs e)
        {
            startDragging(e);

        }

        private void pnlPaused_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
                dragWindow(e);

        }

        private void pnlPaused_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
            
        }

        private void pnlPaused_Resize(object sender, EventArgs e)
        {
            lblImBack.Left = (pnlPaused.Width - lblImBack.Width) / 2;
        }

        private void busyPicture_Click(object sender, EventArgs e)
        {
            ShowMainMenu();
        }

    } // Class HoverWindow
}
