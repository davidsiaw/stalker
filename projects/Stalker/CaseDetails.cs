using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FogBugzNet;
using BlueBlocksLib.AsyncComms;

namespace Stalker {
	public partial class CaseDetails : Form {

		FogBugz m_fb;
		Case m_c;
		Agent<Action> m_ci;

		public CaseDetails(Case c, FogBugz fb, Agent<Action> ci) {
			InitializeComponent();

			Case.Events events = c.caseEvents;

			foreach (Case.Event ev in events.@event) {
				EventDisplayer ed = new EventDisplayer(ev, fb, ci);
				if (!string.IsNullOrEmpty(ed.Text)) {
					flowLayoutPanel1.Controls.Add(ed);
				}
			}
			m_fb = fb;
			m_c = c;
			m_ci = ci;

			fb.CasesRefreshed += new EventHandler<BlueBlocksLib.BaseClasses.GenericEventArgs<Case[]>>(fb_CasesRefreshed);

			FormClosing += new FormClosingEventHandler(CaseDetails_FormClosing);

			this.Text = c.ID + ": " + c.Name;
		}

		void CaseDetails_FormClosing(object sender, FormClosingEventArgs e) {
			m_fb.CasesRefreshed -= new EventHandler<BlueBlocksLib.BaseClasses.GenericEventArgs<Case[]>>(fb_CasesRefreshed);
		}

		void fb_CasesRefreshed(object sender, BlueBlocksLib.BaseClasses.GenericEventArgs<Case[]> e) {
			Case c = e.Args.First(x => x.ID == m_c.ID);
			
			Dictionary<string, EventDisplayer> existing = new Dictionary<string, EventDisplayer>();

			BeginInvoke(new Action(() => {
				foreach (EventDisplayer disp in flowLayoutPanel1.Controls) {
					existing[disp.Text] = disp;
				}

				Case.Events events = c.caseEvents;
				foreach (Case.Event ev in events.@event) {
					EventDisplayer ed = new EventDisplayer(ev, m_fb, m_ci);
					if (!existing.ContainsKey(ed.Text) && !string.IsNullOrEmpty(ed.Text)) {
						flowLayoutPanel1.Controls.Add(ed);
					}
				}
			}));
		}

		private void CaseDetails_Load(object sender, EventArgs e) {
			flowLayoutPanel1_SizeChanged(sender, e);
		}

		private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e) {
			foreach (EventDisplayer ed in flowLayoutPanel1.Controls) {
				Size s = new Size(flowLayoutPanel1.Width - 24, 0);
				ed.MinimumSize = s;
				ed.MaximumSize = s;
			}
		}

		private void btn_submit_Click(object sender, EventArgs e) {
			Case.Event ev = new Case.Event();
			ev.s = txt_comment.Text;
			ev.sPerson = m_c.AssignedTo;
			ev.rgAttachments = new Case.Attachments() { attachment = new Case.Attachment[0] };

			EventDisplayer ed = new EventDisplayer(ev, m_fb, m_ci);
			if (!string.IsNullOrEmpty(ed.Text)) {
				flowLayoutPanel1.Controls.Add(ed);
				
				m_fb.CommentOnCase(m_c.ID, txt_comment.Text);
			}

			txt_comment.Text = "";
		}

		private void txt_comment_TextChanged(object sender, EventArgs e) {

		}


	}
}
