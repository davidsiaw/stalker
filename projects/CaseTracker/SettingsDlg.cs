using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FogBugzCaseTracker
{
    public partial class SettingsDlg : Form
    {
        private SettingsModel _model;

        public void ApplyModel()
        {
            try
            {
                numOpacity.Value = (decimal)(100.0 * _model.Opacity);
                ((Form)Owner).Opacity = _model.Opacity;
            }
            catch (ArgumentOutOfRangeException)
            {
                numOpacity.Value = 80;
                ((Form)Owner).Opacity = 0.8;
            }

            fontDialog1.Font = _model.UserFont;
            lblChosenFont.Text = String.Format("{0} {1}", _model.UserFont.Name, _model.UserFont.SizeInPoints);

            if (_model.MinutesBeforeAway == 0)
                chkAutoPause.Checked = false;
            else
                numPauseMinutes.Value = _model.MinutesBeforeAway;

            numSeconds.Value = _model.CaseListRefreshInterval_Secs;
            chkSwitchToNothinOnExit.Checked = _model.SwitchToNothingWhenClosing;
        }

        public void LoadModel(SettingsModel model)
        {
            _model = model;
            ApplyModel();
        }

        public SettingsModel SaveModel()
        {
            _model.Opacity = (double)numOpacity.Value / 100.0;

            _model.UserFont = fontDialog1.Font;

            if (!chkAutoPause.Checked)
                _model.MinutesBeforeAway = 0;
            else
                _model.MinutesBeforeAway = (int)numPauseMinutes.Value;

            _model.CaseListRefreshInterval_Secs = (int)numSeconds.Value;
            _model.SwitchToNothingWhenClosing = chkSwitchToNothinOnExit.Checked;
            return _model;
        }

        public SettingsDlg()
        {
            InitializeComponent();
        }

        private void numOpacity_ValueChanged(object sender, EventArgs e)
        {
            _model.Opacity = (double)numOpacity.Value / 100.0;
            ApplyModel();
        }

        private void btnChooseFont_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                _model.UserFont = fontDialog1.Font;
                ApplyModel();
            }
        }


        private void chkAutoPause_CheckedChanged(object sender, EventArgs e)
        {
            lblMinutes.Enabled = chkAutoPause.Checked;
            numPauseMinutes.Enabled = chkAutoPause.Checked;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

    }
}
