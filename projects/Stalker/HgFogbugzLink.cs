// -----------------------------------------------------------------------
// <copyright file="HgFogbugzLink.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Stalker {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;
	using HgNet;
	using FogBugzNet;
	using System.Text.RegularExpressions;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>

	class HgFogbugzLink {

		Mercurial m_m;
		FogBugz m_fb;

		public string WorkingCopyDirectory { get; private set; }

		public HgFogbugzLink(string workingCopy, FogBugz fb) {
			WorkingCopyDirectory = workingCopy;
			m_m = new Mercurial(workingCopy);
			m_fb = fb;
		}

		Regex notalphanum = new Regex("[^0-9a-z]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public void StartWorkingOn(Case c) {
		}

		public void StopWorkingOn() {
		}

		public bool IsSynchronizedWithRemote() {
			DoUpdates();
			return m_m.IsSynchronizedWithRemote();
		}

		public bool HasOutgoing() {
			DoUpdates();
			return m_m.HasOutgoing();
		}

		public bool HasIncoming() {
			DoUpdates();
			return m_m.HasIncoming();
		}

		private void DoUpdates() {
			if (m_fb.CaseWorkedOnNow != 0 &&
							!GetCurrentBranch().StartsWith(m_fb.CaseWorkedOnNow.ToString())) {
				StartWorkingOn(m_fb.GetCases().First(x => x.ID == m_fb.CaseWorkedOnNow));
			} else if (m_fb.CaseWorkedOnNow == 0) {
				StopWorkingOn();
			}
		}

		public string GetCurrentBranch() {
			return m_m.GetCurrentBranch();
		}


		public string[] GetModifiedFiles() {
			return m_m.GetModifiedFiles();
		}

		public void Commit(string message) {
			if (m_fb.CaseWorkedOnNow == 0) {
				MessageBox.Show("You are not working on anything!");
			}

			m_m.Commit("BugId " + m_fb.CaseWorkedOnNow + ": " + message);
		}

		public Head[] GetBranches() {
			Head[] branches = m_m.GetHeadsOfUser(this.m_fb.Email);
			Case[] cases = m_fb.GetCases();
			var relevant = branches;
			return relevant;
		}
	}
}
