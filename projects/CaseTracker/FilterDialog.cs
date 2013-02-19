using System;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FogBugzNet;


namespace FogBugzCaseTracker
{
    public partial class FilterDialog : Form
    {
        private HoverWindow _parentWindow;
        private FogBugz _fb;
        private FilterModel _model;

        public FilterDialog(HoverWindow parent, FogBugz fb)
        {
            InitializeComponent();
            _fb = fb;
            _parentWindow = parent;

        }

        public void LoadModel(FilterModel model)
        {
            _model = model;
            chkIncludeNoEstimate.Checked = model.IncludeNoEstimate;
            cmboNarrowSearch.Text = model.UserSearch;
            chkIgnoreBaseSearch.Checked = model.IgnoreBaseSearch;
            txtBaseSearch.Enabled = !model.IgnoreBaseSearch;
            txtBaseSearch.Text = model.BaseSearch;
        }

        public FilterModel SaveModel()
        {
            _model.IncludeNoEstimate = chkIncludeNoEstimate.Checked;
            _model.UserSearch = cmboNarrowSearch.Text;
            _model.IgnoreBaseSearch = chkIgnoreBaseSearch.Checked;

            return _model;
        }

        private bool IsDirty()
        {
            return
                _model.UserSearch != cmboNarrowSearch.Text
                ||
                _model.IgnoreBaseSearch != chkIgnoreBaseSearch.Checked
                ||
                _model.IncludeNoEstimate != chkIncludeNoEstimate.Checked;
        }

        private void testSearchAsync(RunWorkerCompletedEventHandler OnTestSearchComplete)
        {
            CultureAwareBackgroundWorker bw = new CultureAwareBackgroundWorker();

            String search = _model.FormatSearchQuery();
            bw.DoWork += new DoWorkEventHandler(delegate(object sender, DoWorkEventArgs args)
            {
                args.Result = _fb.GetCases(search);
            });
            bw.RunWorkerCompleted += OnTestSearchComplete;
            bw.RunWorkerAsync();
        }

        private void DoSearch()
        {
            _model = SaveModel();
            try
            {
                testSearchAsync(new RunWorkerCompletedEventHandler(delegate(object sender, RunWorkerCompletedEventArgs args)
                {
                    if (args.Error == null)
                    {
                        _model.Cases = (Case[])args.Result;
                        listTestResults.Items.Clear();
                        foreach (Case c in _model.Cases)
                            listTestResults.Items.Add(c);
                    }
                    else
                        Utils.ShowErrorMessage("Error while executing search.\n" + args.Error.ToString());
                }));
            }
            catch (ECommandFailed x)
            {
                Utils.Log.Error(x.Message);
                Utils.ShowErrorMessage(x.Message, "Error in search syntax");
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                DoSearch();
            else if (e.KeyChar == (char)Keys.Escape)
                Close();
        }

        private void chkIgnoreBaseSearch_CheckedChanged(object sender, EventArgs e)
        {
            txtBaseSearch.Enabled = !chkIgnoreBaseSearch.Checked;
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            cmboNarrowSearch.Items.Clear();
            if (_model.History.QueryStrings.Count > 0)
                cmboNarrowSearch.Items.AddRange(_model.History.QueryStrings.ToArray());
        }

        private void lnkSearchHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start((string)ConfigurationManager.AppSettings["SearchSyntaxHelpURL"]);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (IsDirty())
                _model.Cases = null; // So that the query is done on the caller dialog and not during closing of this dialog
        }
    }
}