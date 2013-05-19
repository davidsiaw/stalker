namespace Stalker {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn_someoneelse = new System.Windows.Forms.Button();
			this.txt_search = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmb_project = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.combo_people = new System.Windows.Forms.ComboBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.data_branches = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.txt_commitMessage = new System.Windows.Forms.TextBox();
			this.btn_commit = new System.Windows.Forms.Button();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.lbl_incoming = new System.Windows.Forms.Label();
			this.lbl_outgoing = new System.Windows.Forms.Label();
			this.lbl_wcuptodate = new System.Windows.Forms.Label();
			this.lbl_branchname = new System.Windows.Forms.Label();
			this.txt_workingCopy = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.data_branches)).BeginInit();
			this.panel1.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(820, 526);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			// 
			// progressBar1
			// 
			this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBar1.Location = new System.Drawing.Point(0, 753);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(820, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btn_someoneelse);
			this.flowLayoutPanel1.Controls.Add(this.txt_search);
			this.flowLayoutPanel1.Controls.Add(this.label3);
			this.flowLayoutPanel1.Controls.Add(this.cmb_project);
			this.flowLayoutPanel1.Controls.Add(this.label2);
			this.flowLayoutPanel1.Controls.Add(this.combo_people);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(820, 28);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// btn_someoneelse
			// 
			this.btn_someoneelse.Location = new System.Drawing.Point(3, 3);
			this.btn_someoneelse.Name = "btn_someoneelse";
			this.btn_someoneelse.Size = new System.Drawing.Size(108, 22);
			this.btn_someoneelse.TabIndex = 0;
			this.btn_someoneelse.Text = "LOGOUT";
			this.btn_someoneelse.UseVisualStyleBackColor = true;
			this.btn_someoneelse.Click += new System.EventHandler(this.btn_someoneelse_Click);
			// 
			// txt_search
			// 
			this.txt_search.Location = new System.Drawing.Point(117, 3);
			this.txt_search.Name = "txt_search";
			this.txt_search.Size = new System.Drawing.Size(135, 20);
			this.txt_search.TabIndex = 1;
			this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
			this.txt_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_search_KeyDown);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(258, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Project:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmb_project
			// 
			this.cmb_project.FormattingEnabled = true;
			this.cmb_project.Location = new System.Drawing.Point(326, 3);
			this.cmb_project.Name = "cmb_project";
			this.cmb_project.Size = new System.Drawing.Size(194, 21);
			this.cmb_project.TabIndex = 5;
			this.cmb_project.SelectedIndexChanged += new System.EventHandler(this.cmb_project_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(526, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Assigned To:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// combo_people
			// 
			this.combo_people.FormattingEnabled = true;
			this.combo_people.Location = new System.Drawing.Point(613, 3);
			this.combo_people.Name = "combo_people";
			this.combo_people.Size = new System.Drawing.Size(194, 21);
			this.combo_people.TabIndex = 3;
			this.combo_people.SelectedIndexChanged += new System.EventHandler(this.combo_people_SelectedIndexChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 28);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.data_branches);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(820, 725);
			this.splitContainer1.SplitterDistance = 526;
			this.splitContainer1.TabIndex = 3;
			// 
			// data_branches
			// 
			this.data_branches.AllowUserToAddRows = false;
			this.data_branches.AllowUserToDeleteRows = false;
			this.data_branches.AllowUserToOrderColumns = true;
			this.data_branches.AllowUserToResizeRows = false;
			this.data_branches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.data_branches.Dock = System.Windows.Forms.DockStyle.Fill;
			this.data_branches.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.data_branches.Location = new System.Drawing.Point(0, 88);
			this.data_branches.Name = "data_branches";
			this.data_branches.RowHeadersVisible = false;
			this.data_branches.Size = new System.Drawing.Size(820, 107);
			this.data_branches.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.flowLayoutPanel3);
			this.panel1.Controls.Add(this.flowLayoutPanel2);
			this.panel1.Controls.Add(this.txt_workingCopy);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(820, 88);
			this.panel1.TabIndex = 1;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.txt_commitMessage);
			this.flowLayoutPanel3.Controls.Add(this.btn_commit);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 59);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(820, 29);
			this.flowLayoutPanel3.TabIndex = 3;
			// 
			// txt_commitMessage
			// 
			this.txt_commitMessage.Location = new System.Drawing.Point(3, 3);
			this.txt_commitMessage.Name = "txt_commitMessage";
			this.txt_commitMessage.Size = new System.Drawing.Size(255, 20);
			this.txt_commitMessage.TabIndex = 1;
			// 
			// btn_commit
			// 
			this.btn_commit.Location = new System.Drawing.Point(264, 3);
			this.btn_commit.Name = "btn_commit";
			this.btn_commit.Size = new System.Drawing.Size(75, 23);
			this.btn_commit.TabIndex = 0;
			this.btn_commit.Text = "Commit";
			this.btn_commit.UseVisualStyleBackColor = true;
			this.btn_commit.Click += new System.EventHandler(this.btn_commit_Click);
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.lbl_incoming);
			this.flowLayoutPanel2.Controls.Add(this.lbl_outgoing);
			this.flowLayoutPanel2.Controls.Add(this.lbl_wcuptodate);
			this.flowLayoutPanel2.Controls.Add(this.lbl_branchname);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 33);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(820, 26);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// lbl_incoming
			// 
			this.lbl_incoming.AutoSize = true;
			this.lbl_incoming.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_incoming.Location = new System.Drawing.Point(3, 3);
			this.lbl_incoming.Margin = new System.Windows.Forms.Padding(3);
			this.lbl_incoming.Name = "lbl_incoming";
			this.lbl_incoming.Padding = new System.Windows.Forms.Padding(3);
			this.lbl_incoming.Size = new System.Drawing.Size(117, 21);
			this.lbl_incoming.TabIndex = 0;
			this.lbl_incoming.Text = "Incoming Changesets";
			// 
			// lbl_outgoing
			// 
			this.lbl_outgoing.AutoSize = true;
			this.lbl_outgoing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_outgoing.Location = new System.Drawing.Point(126, 3);
			this.lbl_outgoing.Margin = new System.Windows.Forms.Padding(3);
			this.lbl_outgoing.Name = "lbl_outgoing";
			this.lbl_outgoing.Padding = new System.Windows.Forms.Padding(3);
			this.lbl_outgoing.Size = new System.Drawing.Size(117, 21);
			this.lbl_outgoing.TabIndex = 3;
			this.lbl_outgoing.Text = "Outgoing Changesets";
			// 
			// lbl_wcuptodate
			// 
			this.lbl_wcuptodate.AutoSize = true;
			this.lbl_wcuptodate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_wcuptodate.Location = new System.Drawing.Point(249, 3);
			this.lbl_wcuptodate.Margin = new System.Windows.Forms.Padding(3);
			this.lbl_wcuptodate.Name = "lbl_wcuptodate";
			this.lbl_wcuptodate.Padding = new System.Windows.Forms.Padding(3);
			this.lbl_wcuptodate.Size = new System.Drawing.Size(125, 21);
			this.lbl_wcuptodate.TabIndex = 2;
			this.lbl_wcuptodate.Text = "Working Copy Modified";
			// 
			// lbl_branchname
			// 
			this.lbl_branchname.AutoSize = true;
			this.lbl_branchname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_branchname.Location = new System.Drawing.Point(380, 3);
			this.lbl_branchname.Margin = new System.Windows.Forms.Padding(3);
			this.lbl_branchname.Name = "lbl_branchname";
			this.lbl_branchname.Padding = new System.Windows.Forms.Padding(3);
			this.lbl_branchname.Size = new System.Drawing.Size(49, 21);
			this.lbl_branchname.TabIndex = 1;
			this.lbl_branchname.Text = "Branch";
			// 
			// txt_workingCopy
			// 
			this.txt_workingCopy.Dock = System.Windows.Forms.DockStyle.Top;
			this.txt_workingCopy.Location = new System.Drawing.Point(0, 13);
			this.txt_workingCopy.Name = "txt_workingCopy";
			this.txt_workingCopy.ReadOnly = true;
			this.txt_workingCopy.Size = new System.Drawing.Size(820, 20);
			this.txt_workingCopy.TabIndex = 1;
			this.txt_workingCopy.Click += new System.EventHandler(this.txt_workingCopy_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Main Repository";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 776);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(820, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// statuslabel
			// 
			this.statuslabel.Name = "statuslabel";
			this.statuslabel.Size = new System.Drawing.Size(26, 17);
			this.statuslabel.Text = "Idle";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(820, 798);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "Form1";
			this.Text = "Stalker";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.data_branches)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btn_someoneelse;
		private System.Windows.Forms.TextBox txt_search;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView data_branches;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Button btn_commit;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Label lbl_incoming;
		private System.Windows.Forms.Label lbl_wcuptodate;
		private System.Windows.Forms.Label lbl_branchname;
		private System.Windows.Forms.TextBox txt_workingCopy;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statuslabel;
		private System.Windows.Forms.TextBox txt_commitMessage;
		private System.Windows.Forms.Label lbl_outgoing;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox combo_people;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cmb_project;
	}
}

