namespace Stalker {
	partial class EventDisplayer {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.txt_name = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.RichTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.flow_pictures = new System.Windows.Forms.FlowLayoutPanel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txt_name
			// 
			this.txt_name.Dock = System.Windows.Forms.DockStyle.Top;
			this.txt_name.Location = new System.Drawing.Point(0, 0);
			this.txt_name.Name = "txt_name";
			this.txt_name.ReadOnly = true;
			this.txt_name.Size = new System.Drawing.Size(631, 20);
			this.txt_name.TabIndex = 0;
			this.txt_name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_name_MouseClick);
			this.txt_name.TextChanged += new System.EventHandler(this.txt_name_TextChanged);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.Window;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(631, 316);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "";
			this.textBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 20);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(631, 316);
			this.panel1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 15);
			this.label1.Size = new System.Drawing.Size(45, 33);
			this.label1.TabIndex = 3;
			this.label1.Text = "label1";
			// 
			// flow_pictures
			// 
			this.flow_pictures.AutoScroll = true;
			this.flow_pictures.AutoSize = true;
			this.flow_pictures.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flow_pictures.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flow_pictures.Location = new System.Drawing.Point(0, 336);
			this.flow_pictures.Name = "flow_pictures";
			this.flow_pictures.Size = new System.Drawing.Size(631, 0);
			this.flow_pictures.TabIndex = 4;
			this.flow_pictures.WrapContents = false;
			// 
			// EventDisplayer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.txt_name);
			this.Controls.Add(this.flow_pictures);
			this.Name = "EventDisplayer";
			this.Size = new System.Drawing.Size(631, 336);
			this.Load += new System.EventHandler(this.EventDisplayer_Load);
			this.SizeChanged += new System.EventHandler(this.EventDisplayer_SizeChanged);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt_name;
		private System.Windows.Forms.RichTextBox textBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel flow_pictures;
	}
}
