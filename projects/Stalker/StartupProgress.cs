using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stalker {
	public partial class StartupProgress : Form {
		public StartupProgress() {
			InitializeComponent();
		}

		string Status {
			get {
				return label1.Text;
			}
			set {
				label1.Text = value;
			}
		}

		void U(string status, int progress) {
			BeginInvoke(new Action<int>((p) => {
				Status = status;
				progressBar1.Value = p;
			}), progress);
		}

		public void SettingUpTable() { U("Setting up table", 10); }
		public void LoggingIn() { U("Logging in to your account", 15); }
		public void TestingSystem() { U("Testing the connection", 20); }
		public void Oops() { U("Oops something funny happened", 20); }
		public void PuttingTicketsUp() { U("Putting Tickets Up", 25); }
		public void PreparingToWork() { U("Preparing to work", 30); }
		public void IndicateProgress(string msg, int progress) { 
			U(msg, (int)(progress / 100.0 * 70) + 30); 
		}

		private void StartupProgress_Load(object sender, EventArgs e) {

		}
	}
}
