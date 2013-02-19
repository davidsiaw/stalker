using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FogBugzNet;
using System.Net;
using System.IO;
using BlueBlocksLib.AsyncComms;

namespace Stalker {
	public partial class EventDisplayer : UserControl {
		Case.Event m_ev;
		FogBugz m_fb; 
		Agent<Action> m_ci;

		public EventDisplayer(Case.Event ev, FogBugz fb, Agent<Action> ci) {
			InitializeComponent();

			txt_name.Text = ev.sPerson;
			textBox1.Text = ev.s;
			if (string.IsNullOrEmpty(textBox1.Text)) {
				textBox1.Text = ev.sHtml;
			}
			if (string.IsNullOrEmpty(textBox1.Text)) {
				textBox1.Text = ev.sBodyText;
			}
			if (string.IsNullOrEmpty(textBox1.Text)) {
				textBox1.Text = ev.sBodyHTML;
			}

			//textBox1.Text = textBox1.Text.Replace("\n", @"\line");
			label1.Text = textBox1.Text;
			Text = textBox1.Text;

			m_ev = ev;
			m_fb = fb;
			m_ci = ci;
		}

		private void StartLoadingAttachments(Case.Event ev, FogBugz fb, Agent<Action> ci) {

			ci.SendMessage(() => {

				foreach (Case.Attachment att in ev.rgAttachments.attachment) {

					Case.Attachment attachment = att;
					WebRequest wr = WebRequest.Create("https://telogis.fogbugz.com/" + att.sURL.Replace("&amp;", "&") + "&token=" + fb.AuthToken);
					var wresp = wr.GetResponse();
					if (wresp.ContentType.Contains("image")) {
						Bitmap b = new Bitmap(wresp.GetResponseStream());
						BeginInvoke(new Action(() => {
							PictureBox pb = new PictureBox();
							pb.Size = b.Size;
							pb.Image = b;
							flow_pictures.Controls.Add(pb);
						}));

					} else if (attachment.sFileName.EndsWith(".csv")) {

						var data = new List<string[]>(ExtractCSVData(wresp));

						ci.SendMessage(() => {
							BeginInvoke(new Action(() => {
								DataGridView dgv = new DataGridView();

								flow_pictures.Resize += new EventHandler((o, e) => {
									dgv.Width = flow_pictures.Width - 10;
								});
								dgv.Width = flow_pictures.Width - 10;
								flow_pictures.Controls.Add(dgv);

								ci.SendMessage(() => {
									foreach (var datum in data) {
										BeginInvoke(new Action<string[]>(x => {
											if (dgv.ColumnCount == 0) {
												foreach (string tok in x) {
													int col = dgv.Columns.Add(tok, tok);
													dgv.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
												}
											}

											dgv.Rows.Add(x);
										}), new object[] { datum });
									}
								});
							}));
						});

					} else {
						using (StreamReader sr = new StreamReader(wresp.GetResponseStream())) {
							string str = sr.ReadToEnd();
							BeginInvoke(new Action(() => {
								Label lbl = new Label();
								lbl.Text = str;
								flow_pictures.Controls.Add(lbl);
							}));
						}
					}
					BeginInvoke(new Action(() => {
						Label l = new Label();
						l.Text = attachment.sFileName;
						flow_pictures.Controls.Add(l);
					}));
				}
			});
		}

		private static IEnumerable<string[]> ExtractCSVData(WebResponse wresp) {
			string[] columns;
			List<string[]> data = new List<string[]>();
			using (StreamReader sr = new StreamReader(wresp.GetResponseStream())) {

				string line = sr.ReadLine();
				string[] toks = line.Split(',');
				columns = toks;

				toks = line.Split(",".ToCharArray(), columns.Length);
				data.Add(toks);

				while (!sr.EndOfStream) {
					line = sr.ReadLine();
					toks = line.Split(",".ToCharArray(), columns.Length);
					data.Add(toks);
				}
			}

			foreach (string[] datum in data) {
				yield return datum;
			}
		}


		public new string Text {
			get;
			private set;
		}

		private void EventDisplayer_Load(object sender, EventArgs e) {
			StartLoadingAttachments(m_ev, m_fb, m_ci);
		}

		private void EventDisplayer_SizeChanged(object sender, EventArgs e) {
			label1.MaximumSize = new Size(Width - 6, 0);
		}

		private void txt_name_TextChanged(object sender, EventArgs e) {
		}

		private void txt_name_MouseClick(object sender, MouseEventArgs e) {
			Utilities.HideCaret(txt_name);
		}

		private void textBox1_MouseClick(object sender, MouseEventArgs e) {
			Utilities.HideCaret(textBox1);

		}

	}
}
