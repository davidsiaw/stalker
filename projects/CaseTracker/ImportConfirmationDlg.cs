using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FogBugzNet;

namespace FogBugzCaseTracker
{
    public partial class ImportConfirmationDlg : Form
    {
        private ImportAnalysis _analysis;

        public ImportConfirmationDlg(ImportAnalysis analysis)
        {
            _analysis = analysis;
            InitializeComponent();
        }

        private void DescribeChanges()
        {
            foreach (string s in _analysis.Describe())
            {
                lstChanges.Items.Add(s);
            }

        }

        private void ImportConfirmationDlg_Load(object sender, EventArgs e)
        {
            DescribeChanges();
        }
    }
}