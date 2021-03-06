﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueBlocksLib.AsyncComms;
using FogBugzNet;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using KilnApi;

namespace Stalker {

	class FBCase {
		public readonly Case m_c;
		FogBugz m_fb;
		Agent<Action> m_ci;
		Kiln m_kiln;
		public FBCase(FogBugz fb, Case c, Agent<Action> ci) {
			m_c = c;
			m_fb = fb;
			m_ci = ci;
			m_kiln = m_fb.Kiln;
		}
		
		[InitialWidth(30)]
		public bool WrkOn {
			get {
				return m_fb.CaseWorkedOnNow == m_c.ID;
			}
			set {
				if (m_c.kilnReview != null) {
					return;
				}

				if (value == false) {
					m_ci.SendMessage(() => {
						m_fb.StopWorking();
					});
				} else {
					m_ci.SendMessage(() => {
						m_fb.StartWorking(m_c.ID);
					});
				}
			}
		}

		[InitialWidth(50)]
		public string ID {
			get {
				if (m_c.kilnReview != null) {
					return m_c.ID.ToString() + " -> " + m_c.kilnReview.ID.ToString();
				}
				return m_c.ID.ToString();
			}
		}

		public string Project {
			get {
				return m_c.ParentProject.Name;
			}
		}

		public string Area {
			get {
				return m_c.Area;
			}
		}

		[AutoFillColumn]
		[MinimumWidth(300)]
		public string Name {
			get {
				return m_c.Name;
			}
		}

		[InitialWidth(30)]
		public int Prty {
			get {
				return m_c.Priority;
			}
		}

		public string Milestone {
			get {
				if (m_c.kilnReview != null) {
					return m_c.kilnReview.Status.ToString();
				}
				return string.Join(" ", m_c.ParentMileStone.GetDateIfRelevant(), m_c.ParentMileStone.Name).Trim();
			}
		}

		public string Elapsed {
			get {
				if (m_c.kilnReview != null) {
					return m_c.kilnReview.Reviewers[0].Name;
				}
				return m_c.Elapsed.ToReadableString();
			}
		}

		[InitialWidth(70)]
		public string Estimate(bool clicked) {
			if (clicked) {

				if (m_c.kilnReview != null) {

					m_ci.SendMessage(() => {
						if (m_c.assignedReviewToSelf) {
							Process.Start(m_fb.ReviewURL(ID));

						} else if (m_c.kilnReview.Status == KilnApi.ReviewStatus.Approved) {
							m_ci.SendMessage(() => {
								m_fb.CloseCase(m_c);
								//m_kiln.UpdateReview(m_c.kilnReview.ID, close: true);
							});

						} else {
							Process.Start(m_fb.ReviewURL(ID));
						}
					});

				} else {
					EstimateDialog ed = new EstimateDialog();
					var res = ed.ShowDialog();
					if (res == DialogResult.OK) {
						m_ci.SendMessage(() => {

							m_fb.SetEstimate(m_c.ID, ed.UserEstimate);
						});
					}
				}
			}

			if (m_c.kilnReview != null) {
				if (m_c.reviewAssignedToMe) {
					if (m_c.assignedReviewToSelf) {
						return "YOU ASSIGNED A REVIEW TO YOURSELF";
					}

					return "Review Code";
				}

				if (m_c.kilnReview.Status == KilnApi.ReviewStatus.Approved) {
					return "Close";
				}

				return "See Review";
			}
			return m_c.Estimate.ToReadableString();
		}

		[InitialWidth(70)]
		public string Status(bool clicked) {
			if (clicked) {
				StatusList ed = new StatusList(m_fb, m_c);
				var res = ed.ShowDialog();
				if (res == DialogResult.OK) {
					m_ci.SendMessage(() => {
						m_fb.SetStatus(m_c.ID, ed.SelectedStatus.ID);
					});
				}
			}
			return m_c.Status;
		}

		[InitialWidth(50)]
		public string Details(bool clicked) {
			if (clicked) {
				if (m_c.caseEvents != null) {
					CaseDetails cd = new CaseDetails(m_c, m_fb, m_ci);
					cd.Show();
				}
			}

			return "Details";
		}

		[InitialWidth(50)]
		public string GoTo(bool clicked) {
			if (clicked) {

				m_ci.SendMessage(() => {
					if (m_c.kilnReview != null) {
						Process.Start(m_fb.ReviewURL(m_c.kilnReview.ID));
					} else {
						Process.Start(m_fb.CaseEditURL(m_c.ID));
					}
				});
			}

			if (m_c.kilnReview != null) {
				return "Review";
			}
			return "Go to";
		}

		[InitialWidth(50)]
		public string OTL(bool clicked) {
			if (clicked) {

				PersonList pl = new PersonList(m_kiln.GetPersons());
				pl.ShowDialog();

				if (m_c.kilnReview == null && pl.Selected != null) {
					if (m_c.changeSets.Length != 0) {
						m_kiln.CreateReview(
							m_c.changeSets[0].repo.ID,
							pl.Selected.ID,
							m_c.changeSets.Select(x => x.changeSet.Revision).ToArray(),
							m_c.Name,
							m_c.Name);
					}
				} else {
					
				}
			}

			if (m_c.kilnReview == null) {
				return "Review";
			}
			return "No Op";
		}

		[RowColor]
		public Color color {
			get {
				if (m_c.assignedReviewToSelf) {
					return Color.Red;
				}

				if (m_c.kilnReview != null) {
					if (m_c.reviewAssignedToMe) {

						switch (m_c.kilnReview.Status) {
							case KilnApi.ReviewStatus.Active:
								return Color.Red;
							case KilnApi.ReviewStatus.Approved:
								return Color.LightGreen;
							case KilnApi.ReviewStatus.Rejected:
								return Color.Pink;
							case KilnApi.ReviewStatus.Unknown:
								return Color.LightGray;
							case KilnApi.ReviewStatus.WillNotReview:
								return Color.LightGray;
							default:
								return Color.LightGray;
						}

					} else {
						switch (m_c.kilnReview.Status) {
							case KilnApi.ReviewStatus.Active:
								return Color.LightYellow;
							case KilnApi.ReviewStatus.Approved:
								return Color.LightGreen;
							case KilnApi.ReviewStatus.Rejected:
								return Color.Pink;
							case KilnApi.ReviewStatus.Unknown:
								return Color.LightGray;
							case KilnApi.ReviewStatus.WillNotReview:
								return Color.LightGray;
							default:
								return Color.LightGray;
						}
					}
				} else {
					switch (m_c.Priority) {
						case 1:
							return Color.DodgerBlue;
						case 2:
							return Color.DeepSkyBlue;
						case 3:
							return Color.SkyBlue;
						case 4:
							return Color.LightBlue;
						case 5:
							return Color.LightCyan;
						default:
							return Color.Blue;
					}
				}
			}
		}

	}
}
