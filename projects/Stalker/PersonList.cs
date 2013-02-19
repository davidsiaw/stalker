using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KilnApi;

namespace Stalker {
	public partial class PersonList : Form {
		public PersonList(Person[] ppl) {
			InitializeComponent();

			Array.Sort(ppl, (x, y) => x.Name.CompareTo(y.Name));
			listBox1.Items.AddRange(ppl);
			
		}

		public Person Selected;

		private void listBox1_DoubleClick(object sender, EventArgs e) {
			if (listBox1.SelectedItem != null) {
				Selected = listBox1.SelectedItem as Person;
				this.Close();
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			if (listBox1.SelectedItem != null) {
				Selected = listBox1.SelectedItem as Person;
				this.Close();
			}
		}
	}
}
