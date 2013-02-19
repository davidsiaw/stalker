namespace Stalker {
	partial class CaseDetails {
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txt_comment = new System.Windows.Forms.TextBox();
			this.btn_submit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(562, 471);
			this.flowLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.WrapContents = false;
			this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txt_comment);
			this.splitContainer1.Panel2.Controls.Add(this.btn_submit);
			this.splitContainer1.Size = new System.Drawing.Size(562, 575);
			this.splitContainer1.SplitterDistance = 471;
			this.splitContainer1.TabIndex = 1;
			// 
			// txt_comment
			// 
			this.txt_comment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt_comment.Location = new System.Drawing.Point(0, 0);
			this.txt_comment.Multiline = true;
			this.txt_comment.Name = "txt_comment";
			this.txt_comment.Size = new System.Drawing.Size(562, 77);
			this.txt_comment.TabIndex = 1;
			this.txt_comment.TextChanged += new System.EventHandler(this.txt_comment_TextChanged);
			// 
			// btn_submit
			// 
			this.btn_submit.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btn_submit.Location = new System.Drawing.Point(0, 77);
			this.btn_submit.Name = "btn_submit";
			this.btn_submit.Size = new System.Drawing.Size(562, 23);
			this.btn_submit.TabIndex = 2;
			this.btn_submit.Text = "Send Comment!";
			this.btn_submit.UseVisualStyleBackColor = true;
			this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
			// 
			// CaseDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 575);
			this.Controls.Add(this.splitContainer1);
			this.Name = "CaseDetails";
			this.Text = "Case Details";
			this.Load += new System.EventHandler(this.CaseDetails_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txt_comment;
		private System.Windows.Forms.Button btn_submit;
	}
}