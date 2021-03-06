﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FogBugzNet;
using System.Reflection;
using BlueBlocksLib.AsyncComms;
using System.IO;
using FogBugzCaseTracker;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Diagnostics;
using HgNet;
using System.Text.RegularExpressions;
using KilnApi;
using BlueBlocksLib.SetUtils;

namespace Stalker {
	public partial class Form1 : Form {

		FogBugz fb;
		Agent<Action> commsInterface;
		Agent<Action> sideProcessor;
		HgFogbugzLink m = null;


		public Form1() {
			InitializeComponent();
			txt_search.SetWatermark("Search for case");

		}

		private void OpenMercurialLink() {
			if (File.Exists("hginfo")) {
				try {
					using (StreamReader sr = new StreamReader("hginfo")) {
						m = new HgFogbugzLink(sr.ReadLine(), fb);
					}
				} catch { } finally { }
			}
		}

		private static bool UpdateLogin() {
			LoginForm lf = new LoginForm();
			var res = lf.ShowDialog();
			if (res == DialogResult.Cancel) {
				return false;
			}
			LoginInfo li = new LoginInfo() {
				user = lf.UserName,
				pass = lf.Password,
				url = lf.Server
			};
			BinaryFormatter bf = new BinaryFormatter();
			using (FileStream fs = new FileStream("userlogin", FileMode.Create)) {
				bf.Serialize(fs, li);
			}
			return true;
		}

		bool first = true;
		void UpdateUI(Action<string,int> progressDelegate) {
			try {

				string name = "me";
				string project = "";

				Invoke(new Action(() => {
					try {
						name = combo_people.SelectedItem.ToString();
						project = cmb_project.SelectedItem.ToString();
					} catch { }
				}));

				var p = fb.UpdateAllStuff(string.Format("AssignedTo:\"{0}\" project:\"{1}\"", name, project), progressDelegate, !first);

				if (first) {
					first = false;
				}

				Case currentcase = null;
				FBCase[] cases = new FBCase[p.Length];
				for (int i = 0; i < cases.Length; i++) {
					cases[i] = new FBCase(fb, p[i], commsInterface);

					if (fb.CaseWorkedOnNow != 0) {
						if (cases[i].m_c.ID == fb.CaseWorkedOnNow) {
							currentcase = p[i];
						}
					}
				}

				BeginInvoke(new Action(() => {
					Utilities.UpdateDataGridView(cases, dataGridView1, x => x.ID.ToString());
					UpdateProgressBar(currentcase);
				}));

			} catch (Exception e) {
				Console.WriteLine("Oops! Failed to update because: ", e.Message);
			}
		}

		private void UpdateProgressBar(Case currentcase) {
			if (currentcase == null) {
				progressBar1.Value = progressBar1.Maximum;
				Utilities.ChangeProgressBarColor(progressBar1, ProgressBarColor.PBST_PAUSED);
			} else {
				int ratio = (int)(
					currentcase.Elapsed.TotalSeconds /
					currentcase.Estimate.TotalSeconds * progressBar1.Maximum);

				if (ratio > progressBar1.Maximum) {

					progressBar1.Value = progressBar1.Maximum;
					Utilities.ChangeProgressBarColor(progressBar1, ProgressBarColor.PBST_ERROR);

				} else {

					if (ratio < 0) {
						Utilities.ChangeProgressBarColor(progressBar1, ProgressBarColor.PBST_PAUSED);
					} else {
						progressBar1.Value = ratio;
						Utilities.ChangeProgressBarColor(progressBar1, ProgressBarColor.PBST_NORMAL);
					}
				}

			}
		}


		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void Form1_Load(object sender, EventArgs evt) {

			commsInterface = new Agent<Action>(
				() => { },
				() => { },
				action => {
					try {
						action();
						UpdateUIWithCurrentData();
					} catch (Exception e) {
						Console.WriteLine("Oops! Failed to perform action because: ", e.Message);
					}
					return NextAction.WaitForNextMessage;
				}
				);

			sideProcessor = new Agent<Action>(
				() => { },
				() => { },
				action => {
					try {
						action();
					} catch (Exception e) {
						Console.WriteLine("Oops! Failed to perform side operation because: ", e.Message);
					}
					return NextAction.WaitForNextMessage;
				}
				);

			commsInterface.Start();
			sideProcessor.Start();

			StartupProgress sp = new StartupProgress();

			sideProcessor.SendMessage(() => {

				dataGridView1.CellEndEdit += new DataGridViewCellEventHandler((o, e) => {
					UpdateUIWithCurrentData();

					sideProcessor.SendMessage(() => {
						UpdateUI((x, y) => { });
					});
				});

				if (!File.Exists("userlogin")) {
					if (!UpdateLogin()) {
						Close();
						return;
					}
				}

				BinaryFormatter bf = new BinaryFormatter();
				while (true) {
					try {
						using (FileStream fs = new FileStream("userlogin", FileMode.Open)) {
							LoginInfo li = (LoginInfo)bf.Deserialize(fs);
							fb = new FogBugz(li.url);
							sp.LoggingIn();
							fb.LogOn(li.user, li.pass);
							sp.TestingSystem();
							fb.GetCasesMatchingSearch("1337", (x, y) => { });	// try do something
							BeginInvoke(new Action(() => {
								this.Text = "St4lker:: " + li.user;
							}));

							OpenMercurialLink();
							break;
						}
					} catch {
						File.Delete("userlogin");
						sp.Oops();
						MessageBox.Show("Cannot log in! Please fix your username and pass");
						Console.WriteLine("Oops! An error happened during login.");
						if (!UpdateLogin()) {
							Close();
							return;
						}
					}
				}

				FBCase[] c = new FBCase[0];
				sp.PuttingTicketsUp();
				Invoke(new Action(() => {
					Utilities.InterfaceToDataGridView(c, dataGridView1);
				}));

				var persons = fb.Kiln.GetPersons().OrderBy(x => x.ToString());
				var projects = fb.Kiln.GetProjects();
				Invoke(new Action(() => {
					combo_people.Items.Clear();
					cmb_project.Items.Clear();
					foreach (var person in persons) {
						combo_people.Items.Add(person);
						if (person.Email == fb.Email) {
							combo_people.SelectedItem = person;
						}
					}

					cmb_project.Items.Add("");
					foreach (var project in projects) {
						cmb_project.Items.Add(project);
					}
					cmb_project.SelectedItem = "";
				}));

				Head[] be = new Head[0];
				Invoke(new Action(() => {
					Utilities.InterfaceToDataGridView(be, data_branches);
				}));

				sp.PreparingToWork();
				UpdateUI(sp.IndicateProgress);

				BeginInvoke(new Action(() => {
					sp.Close();
				}));
			});

			sp.ShowDialog();

			// This thing just keeps the table up to date
			Timer updatetimer = new Timer();
			updatetimer.Interval = 1000 * 20;
			updatetimer.Tick += new EventHandler((o, e) => {
				sideProcessor.SendMessage(() => {
					PeriodicUpdate();
				});
			});
			updatetimer.Start();

			Timer updateHgTimer = new Timer();
			updateHgTimer.Interval = 1000 * 5;
			updateHgTimer.Tick += new EventHandler((o, e) => {
				sideProcessor.SendMessage(() => {
					UpdateHGInfo();
				});
			});
			updateHgTimer.Start();

		}

		private void PeriodicUpdate() {

			UpdateUI((x, y) => {
				BeginInvoke(new Action<string, int>((msg, prog) => {
					statuslabel.Text = msg;
				}), x, y);
			});

			UpdateHGInfo();

			BeginInvoke(new Action(() => {
				statuslabel.Text = "Idle";
			}));
		}

		private void UpdateHGInfo() {
			if (m != null) {
				if (m.HasIncoming()) {
					BeginInvoke(new Action(() => {
						lbl_incoming.Text = "Incoming Changes Available";
						lbl_incoming.BackColor = Color.Yellow;
					}));
				} else {
					BeginInvoke(new Action(() => {
						lbl_incoming.Text = "No Incoming Changes";
						lbl_incoming.BackColor = Color.Green;
					}));
				}

				if (m.GetModifiedFiles().Length == 0) {
					BeginInvoke(new Action(() => {
						lbl_wcuptodate.Text = "Working Copy Up To Date";
						lbl_wcuptodate.BackColor = Color.Green;
					}));
				} else {
					BeginInvoke(new Action(() => {
						lbl_wcuptodate.Text = "Working Copy Modified";
						lbl_wcuptodate.BackColor = Color.Yellow;
					}));
				}
				BeginInvoke(new Action(() => {
					lbl_branchname.Text = "Current Branch: " + m.GetCurrentBranch();
					txt_workingCopy.Text = m.WorkingCopyDirectory;
				}));

				var relevant = m.GetBranches();
				IEnumerable<Head> singleHeadedBranches;
				IEnumerable<Head> multiHeadedBranches;
				GetBranchInformation(relevant, out singleHeadedBranches, out multiHeadedBranches);

				BeginInvoke(new Action(() => {

					Utilities.UpdateDataGridView(singleHeadedBranches.Concat(multiHeadedBranches), data_branches, x => x.BranchName, (row) => {
						if (((Head)row.Tag).BranchName.Contains("(merge required)")) {
							return Color.Red;
						}
						return Color.White;
					});
				}));

				if (multiHeadedBranches.Count() != 0) {
					BeginInvoke(new Action(() => {
						lbl_outgoing.Text = "Outgoing Multiple Heads: " + string.Join(", ", multiHeadedBranches.Select(x => x.branch));
						lbl_outgoing.BackColor = Color.Red;
					}));
				}
				else if (m.HasOutgoing()) {
					BeginInvoke(new Action(() => {
						lbl_outgoing.Text = "Outgoing Changes Queued";
						lbl_outgoing.BackColor = Color.Yellow;
					}));
				} else {
					BeginInvoke(new Action(() => {
						lbl_outgoing.Text = "No Outgoing Changes";
						lbl_outgoing.BackColor = Color.Green;
					}));
				}
			}
		}

		private static void GetBranchInformation(Head[] relevant, out IEnumerable<Head> singleHeadedBranches, out IEnumerable<Head> multiHeadedBranches) {
			OneToManyMap<string, Head> record = new OneToManyMap<string, Head>();

			foreach (var rel in relevant) {
				record.Add(rel.BranchName, rel);
			}

			singleHeadedBranches = record.Where(x => x.Value.Count() == 1).Select(x => x.Value.ToArray()[0]);
			multiHeadedBranches = record.Where(x => x.Value.Count() > 1).Select(x => new Head() {
				branch = x.Value.ToArray()[0].BranchName + " (merge required)",
				changeset = string.Join(",", x.Value.Select(h => h.changeset)),
				parent = string.Join(",", x.Value.Select(h => h.parent)),
				user = string.Join(",", x.Value.Select(h => h.user)),
				date = string.Join(",", x.Value.Select(h => h.date)),
				summary = string.Join(",", x.Value.Select(h => h.summary)),
				tag = string.Join(",", x.Value.Select(h => h.tag))
			});
		}


		private void UpdateUIWithCurrentData() {

			BeginInvoke(new Action(() => {
				try {
					Case currcase = null;
					List<FBCase> fbcases = new List<FBCase>();
					foreach (DataGridViewRow row in dataGridView1.Rows) {
						FBCase c = row.Tag as FBCase;
						if (!c.m_c.closed) {
							fbcases.Add(c);
						}
						if (c.m_c.ID == fb.CaseWorkedOnNow) {
							currcase = c.m_c;
						}
					}
					Utilities.UpdateDataGridView(fbcases.ToArray(), dataGridView1, x => x.ID.ToString());
					UpdateProgressBar(currcase);
				} catch (Exception e) {
					Console.WriteLine("Oops! Failed to update UI because: ", e.Message);
				}
			}));
		}

		private void btn_someoneelse_Click(object sender, EventArgs e) {
			// SOMEONE WAN WAN
			if (!UpdateLogin()) {
				Close();
				return;
			}
		}

		private void txt_search_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) {
				Utilities.TryParse(txt_search.Text, 
					x => Process.Start(fb.CaseEditURL(x)));
			}
		}

		private void txt_search_TextChanged(object sender, EventArgs e) { }

		private void btn_commit_Click(object sender, EventArgs e) {
			m.Commit(txt_commitMessage.Text);
		}

		private void txt_workingCopy_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.FileName = "select folder";
			ofd.CheckFileExists = false;
			ofd.CheckPathExists = false;
			ofd.ReadOnlyChecked = true;
			ofd.ValidateNames = false;
			ofd.Multiselect = false;
			ofd.ShowDialog();

			if (!string.IsNullOrEmpty(ofd.FileName)) {
				try {

					string path = Path.GetDirectoryName(ofd.FileName);
					using (StreamWriter sw = new StreamWriter("hginfo", false)) {
						m = new HgFogbugzLink(path, fb);
						sw.WriteLine(path);
					}
					txt_workingCopy.Text = path;
					sideProcessor.SendMessage(() => {
						UpdateHGInfo();
					});
				} catch { } finally { }
			}
		}

		private void combo_people_SelectedIndexChanged(object sender, EventArgs e) {
			if (combo_people.SelectedItem != null) {
				sideProcessor.SendMessage(() => {
					PeriodicUpdate();
				});
			}
		}

		private void cmb_project_SelectedIndexChanged(object sender, EventArgs e) {
			if (combo_people.SelectedItem != null) {
				sideProcessor.SendMessage(() => {
					PeriodicUpdate();
				});
			}
		}

		
	}

}
