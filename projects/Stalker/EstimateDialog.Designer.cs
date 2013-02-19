namespace FogBugzNet
{
    partial class EstimateDialog
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
			this.txtUserEstimate = new System.Windows.Forms.TextBox();
			this.grpExamples = new System.Windows.Forms.GroupBox();
			this.lblExamples = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.grpExamples.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtUserEstimate
			// 
			this.txtUserEstimate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserEstimate.Location = new System.Drawing.Point(15, 12);
			this.txtUserEstimate.Name = "txtUserEstimate";
			this.txtUserEstimate.Size = new System.Drawing.Size(205, 20);
			this.txtUserEstimate.TabIndex = 0;
			this.txtUserEstimate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserEstimate_KeyPress);
			// 
			// grpExamples
			// 
			this.grpExamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpExamples.Controls.Add(this.lblExamples);
			this.grpExamples.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpExamples.Location = new System.Drawing.Point(15, 38);
			this.grpExamples.Name = "grpExamples";
			this.grpExamples.Size = new System.Drawing.Size(205, 168);
			this.grpExamples.TabIndex = 2;
			this.grpExamples.TabStop = false;
			this.grpExamples.Text = "Examples";
			// 
			// lblExamples
			// 
			this.lblExamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblExamples.Location = new System.Drawing.Point(17, 26);
			this.lblExamples.Name = "lblExamples";
			this.lblExamples.Size = new System.Drawing.Size(169, 139);
			this.lblExamples.TabIndex = 0;
			this.lblExamples.Text = "1 week\r\n2 days\r\n1 day 4 hours\r\n15 minutes\r\n1w\r\n2d\r\n1d4h\r\n15m";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Location = new System.Drawing.Point(64, 212);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(145, 212);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// EstimateDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(232, 247);
			this.ControlBox = false;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.grpExamples);
			this.Controls.Add(this.txtUserEstimate);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EstimateDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Your Estimate:";
			this.grpExamples.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserEstimate;
        private System.Windows.Forms.GroupBox grpExamples;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblExamples;
    }
}