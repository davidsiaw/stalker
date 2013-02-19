namespace FogBugzCaseTracker
{
    partial class FilterDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterDialog));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SearchResultBox = new System.Windows.Forms.GroupBox();
            this.chkIncludeNoEstimate = new System.Windows.Forms.CheckBox();
            this.listTestResults = new System.Windows.Forms.ListBox();
            this.lblBaseSearch = new System.Windows.Forms.Label();
            this.chkIgnoreBaseSearch = new System.Windows.Forms.CheckBox();
            this.txtBaseSearch = new System.Windows.Forms.TextBox();
            this.cmboNarrowSearch = new System.Windows.Forms.ComboBox();
            this.lnkSearchHelp = new System.Windows.Forms.LinkLabel();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblNarrowSearch = new System.Windows.Forms.Label();
            this.SearchResultBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(511, 348);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(592, 348);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // SearchResultBox
            // 
            this.SearchResultBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchResultBox.Controls.Add(this.chkIncludeNoEstimate);
            this.SearchResultBox.Controls.Add(this.listTestResults);
            this.SearchResultBox.Controls.Add(this.lblBaseSearch);
            this.SearchResultBox.Controls.Add(this.chkIgnoreBaseSearch);
            this.SearchResultBox.Controls.Add(this.txtBaseSearch);
            this.SearchResultBox.Location = new System.Drawing.Point(15, 66);
            this.SearchResultBox.Name = "SearchResultBox";
            this.SearchResultBox.Size = new System.Drawing.Size(652, 276);
            this.SearchResultBox.TabIndex = 3;
            this.SearchResultBox.TabStop = false;
            this.SearchResultBox.Text = "Search Results";
            // 
            // chkIncludeNoEstimate
            // 
            this.chkIncludeNoEstimate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIncludeNoEstimate.AutoSize = true;
            this.chkIncludeNoEstimate.Location = new System.Drawing.Point(469, 251);
            this.chkIncludeNoEstimate.Name = "chkIncludeNoEstimate";
            this.chkIncludeNoEstimate.Size = new System.Drawing.Size(171, 17);
            this.chkIncludeNoEstimate.TabIndex = 17;
            this.chkIncludeNoEstimate.Text = "Include cases without estimate";
            this.chkIncludeNoEstimate.UseVisualStyleBackColor = true;
            this.chkIncludeNoEstimate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // listTestResults
            // 
            this.listTestResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listTestResults.DisplayMember = "LongDescription";
            this.listTestResults.FormattingEnabled = true;
            this.listTestResults.Location = new System.Drawing.Point(20, 28);
            this.listTestResults.Name = "listTestResults";
            this.listTestResults.Size = new System.Drawing.Size(614, 186);
            this.listTestResults.TabIndex = 2;
            // 
            // lblBaseSearch
            // 
            this.lblBaseSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBaseSearch.AutoSize = true;
            this.lblBaseSearch.Location = new System.Drawing.Point(17, 229);
            this.lblBaseSearch.Name = "lblBaseSearch";
            this.lblBaseSearch.Size = new System.Drawing.Size(147, 13);
            this.lblBaseSearch.TabIndex = 11;
            this.lblBaseSearch.Text = "Base Search (recommended):";
            // 
            // chkIgnoreBaseSearch
            // 
            this.chkIgnoreBaseSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIgnoreBaseSearch.AutoSize = true;
            this.chkIgnoreBaseSearch.Location = new System.Drawing.Point(469, 228);
            this.chkIgnoreBaseSearch.Name = "chkIgnoreBaseSearch";
            this.chkIgnoreBaseSearch.Size = new System.Drawing.Size(117, 17);
            this.chkIgnoreBaseSearch.TabIndex = 15;
            this.chkIgnoreBaseSearch.Text = "Ignore base search";
            this.chkIgnoreBaseSearch.UseVisualStyleBackColor = true;
            this.chkIgnoreBaseSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // txtBaseSearch
            // 
            this.txtBaseSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaseSearch.Location = new System.Drawing.Point(170, 226);
            this.txtBaseSearch.Name = "txtBaseSearch";
            this.txtBaseSearch.ReadOnly = true;
            this.txtBaseSearch.Size = new System.Drawing.Size(293, 20);
            this.txtBaseSearch.TabIndex = 12;
            this.txtBaseSearch.TabStop = false;
            this.txtBaseSearch.Text = "AssignedTo:\"Me\"";
            // 
            // cmboNarrowSearch
            // 
            this.cmboNarrowSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmboNarrowSearch.FormattingEnabled = true;
            this.cmboNarrowSearch.Location = new System.Drawing.Point(62, 12);
            this.cmboNarrowSearch.Name = "cmboNarrowSearch";
            this.cmboNarrowSearch.Size = new System.Drawing.Size(524, 21);
            this.cmboNarrowSearch.TabIndex = 16;
            this.cmboNarrowSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            this.cmboNarrowSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // lnkSearchHelp
            // 
            this.lnkSearchHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkSearchHelp.AutoSize = true;
            this.lnkSearchHelp.Location = new System.Drawing.Point(428, 36);
            this.lnkSearchHelp.Name = "lnkSearchHelp";
            this.lnkSearchHelp.Size = new System.Drawing.Size(158, 13);
            this.lnkSearchHelp.TabIndex = 14;
            this.lnkSearchHelp.TabStop = true;
            this.lnkSearchHelp.Text = "what\'s the search syntax again?";
            this.lnkSearchHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSearchHelp_LinkClicked);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(592, 10);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Go";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            this.btnTest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // lblNarrowSearch
            // 
            this.lblNarrowSearch.AutoSize = true;
            this.lblNarrowSearch.Location = new System.Drawing.Point(12, 15);
            this.lblNarrowSearch.Name = "lblNarrowSearch";
            this.lblNarrowSearch.Size = new System.Drawing.Size(44, 13);
            this.lblNarrowSearch.TabIndex = 13;
            this.lblNarrowSearch.Text = "Search:";
            // 
            // FilterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 381);
            this.Controls.Add(this.cmboNarrowSearch);
            this.Controls.Add(this.lnkSearchHelp);
            this.Controls.Add(this.lblNarrowSearch);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.SearchResultBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(687, 408);
            this.Name = "FilterDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Filter";
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.SearchResultBox.ResumeLayout(false);
            this.SearchResultBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox SearchResultBox;
        private System.Windows.Forms.ListBox listTestResults;
        private System.Windows.Forms.CheckBox chkIncludeNoEstimate;
        private System.Windows.Forms.ComboBox cmboNarrowSearch;
        private System.Windows.Forms.CheckBox chkIgnoreBaseSearch;
        private System.Windows.Forms.LinkLabel lnkSearchHelp;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtBaseSearch;
        private System.Windows.Forms.Label lblBaseSearch;
        private System.Windows.Forms.Label lblNarrowSearch;
    }
}