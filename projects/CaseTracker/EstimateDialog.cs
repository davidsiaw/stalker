using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FogBugzNet
{
    public partial class EstimateDialog : Form
    {
        public String UserEstimate
        {
            get
            {
                return txtUserEstimate.Text;
            }
        }

        public EstimateDialog()
        {
            InitializeComponent();
        }

        private void txtUserEstimate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && txtUserEstimate.Text.Length != 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
