using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FogBugzCaseTracker
{
    public partial class VersionUpdatePrompt : Form
    {

        public string WhatsNew
        {
            set 
            {
                richWhatsNew.AppendText(value);
            }
        }
        public string CurrentVersion
        {
            set
            {

                lblCurrentVersion.Text = value;
            }
        }

        public string LatestVersion
        {
            set 
            {
                lblNewVersion.Text = value;
            }
        }

        public VersionUpdatePrompt()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.No;
                Close();
            }
        }
    }
}
