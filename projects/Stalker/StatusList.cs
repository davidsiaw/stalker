using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FogBugzNet;

namespace Stalker {
	public partial class StatusList : Form {
		public StatusList(FogBugz fb, Case c) {
			InitializeComponent();
			DialogResult = System.Windows.Forms.DialogResult.Cancel;

			var items = fb.GetStatuses(c.CategoryID);
			foreach (var item in items){
				int val = listBox1.Items.Add(item);
				if (item.Name == c.Status) {
					listBox1.SelectedIndex = val;
				}
			}
		}

		public Status SelectedStatus {
			get {
				return (Status)listBox1.SelectedItem;
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
