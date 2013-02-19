namespace FogBugzCaseTracker
{
    partial class VersionUpdatePrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionUpdatePrompt));
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.picNew = new System.Windows.Forms.PictureBox();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.grpWhatsNew = new System.Windows.Forms.GroupBox();
            this.richWhatsNew = new System.Windows.Forms.RichTextBox();
            this.lblCurrentVersionTitle = new System.Windows.Forms.Label();
            this.lblInstall = new System.Windows.Forms.Label();
            this.lblTakes4Seconds = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnHellsYea = new System.Windows.Forms.Button();
            this.btnLater = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picNew)).BeginInit();
            this.grpWhatsNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(69, 21);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(238, 13);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Yey! A new version of Case Tracker is available: ";
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVersion.Location = new System.Drawing.Point(306, 21);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(47, 13);
            this.lblNewVersion.TabIndex = 1;
            this.lblNewVersion.Text = "2.3.4.5";
            // 
            // picNew
            // 
            this.picNew.Image = global::FogBugzCaseTracker.Properties.Resources.new_icon;
            this.picNew.Location = new System.Drawing.Point(12, 12);
            this.picNew.Name = "picNew";
            this.picNew.Size = new System.Drawing.Size(44, 45);
            this.picNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picNew.TabIndex = 2;
            this.picNew.TabStop = false;
            this.picNew.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(306, 41);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(40, 13);
            this.lblCurrentVersion.TabIndex = 3;
            this.lblCurrentVersion.Text = "1.2.3.4";
            // 
            // grpWhatsNew
            // 
            this.grpWhatsNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpWhatsNew.Controls.Add(this.richWhatsNew);
            this.grpWhatsNew.Location = new System.Drawing.Point(12, 76);
            this.grpWhatsNew.Name = "grpWhatsNew";
            this.grpWhatsNew.Size = new System.Drawing.Size(346, 154);
            this.grpWhatsNew.TabIndex = 4;
            this.grpWhatsNew.TabStop = false;
            this.grpWhatsNew.Text = "What\'s New";
            // 
            // richWhatsNew
            // 
            this.richWhatsNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richWhatsNew.Location = new System.Drawing.Point(11, 20);
            this.richWhatsNew.Name = "richWhatsNew";
            this.richWhatsNew.ReadOnly = true;
            this.richWhatsNew.Size = new System.Drawing.Size(323, 121);
            this.richWhatsNew.TabIndex = 0;
            this.richWhatsNew.Text = "";
            // 
            // lblCurrentVersionTitle
            // 
            this.lblCurrentVersionTitle.AutoSize = true;
            this.lblCurrentVersionTitle.Location = new System.Drawing.Point(144, 41);
            this.lblCurrentVersionTitle.Name = "lblCurrentVersionTitle";
            this.lblCurrentVersionTitle.Size = new System.Drawing.Size(160, 13);
            this.lblCurrentVersionTitle.TabIndex = 5;
            this.lblCurrentVersionTitle.Text = "The currently installed version is:";
            // 
            // lblInstall
            // 
            this.lblInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstall.AutoSize = true;
            this.lblInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstall.Location = new System.Drawing.Point(20, 239);
            this.lblInstall.Name = "lblInstall";
            this.lblInstall.Size = new System.Drawing.Size(75, 13);
            this.lblInstall.TabIndex = 6;
            this.lblInstall.Text = "Install now?";
            // 
            // lblTakes4Seconds
            // 
            this.lblTakes4Seconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTakes4Seconds.AutoSize = true;
            this.lblTakes4Seconds.Location = new System.Drawing.Point(101, 239);
            this.lblTakes4Seconds.Name = "lblTakes4Seconds";
            this.lblTakes4Seconds.Size = new System.Drawing.Size(223, 13);
            this.lblTakes4Seconds.TabIndex = 7;
            this.lblTakes4Seconds.Text = "(takes exactly 4 seconds, or you money back)";
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(109, 273);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 8;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // btnHellsYea
            // 
            this.btnHellsYea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHellsYea.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnHellsYea.Location = new System.Drawing.Point(190, 273);
            this.btnHellsYea.Name = "btnHellsYea";
            this.btnHellsYea.Size = new System.Drawing.Size(75, 23);
            this.btnHellsYea.TabIndex = 9;
            this.btnHellsYea.Text = "Definitely!";
            this.btnHellsYea.UseVisualStyleBackColor = true;
            this.btnHellsYea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // btnLater
            // 
            this.btnLater.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLater.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnLater.Location = new System.Drawing.Point(271, 273);
            this.btnLater.Name = "btnLater";
            this.btnLater.Size = new System.Drawing.Size(75, 23);
            this.btnLater.TabIndex = 10;
            this.btnLater.Text = "Later...";
            this.btnLater.UseVisualStyleBackColor = true;
            this.btnLater.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // VersionUpdatePrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 308);
            this.Controls.Add(this.btnLater);
            this.Controls.Add(this.btnHellsYea);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblTakes4Seconds);
            this.Controls.Add(this.lblInstall);
            this.Controls.Add(this.lblCurrentVersionTitle);
            this.Controls.Add(this.grpWhatsNew);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.picNew);
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.lblHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(378, 335);
            this.Name = "VersionUpdatePrompt";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Case Tracker - New Version Available!";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picNew)).EndInit();
            this.grpWhatsNew.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.PictureBox picNew;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.GroupBox grpWhatsNew;
        private System.Windows.Forms.Label lblCurrentVersionTitle;
        private System.Windows.Forms.Label lblInstall;
        private System.Windows.Forms.Label lblTakes4Seconds;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnHellsYea;
        private System.Windows.Forms.Button btnLater;
        private System.Windows.Forms.RichTextBox richWhatsNew;
    }
}