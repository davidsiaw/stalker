using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace HgNet {

	public class Mercurial {

		string workingCopy;
		public Mercurial(string workingCopy) {
			this.workingCopy = workingCopy;
			var a = runCommand("status");

			if (workingCopy == null) {
				throw new ArgumentNullException();
			}

			if (a.Count > 0) {
				if (a[0].StartsWith("abort")) {
					throw new Exception(a[0]);
				}
			} 
		}

		string FullPath(string file) {
			return Path.Combine(workingCopy, file);
		}

		public Head[] GetHeads() {
			var lines = runCommand("heads");

			List<Head> heads = new List<Head>();
			for (int i = 0; i < lines.Count; i++) {
				Head head = new Head();
				for (; i < lines.Count; i++) {
					if (lines[i].Trim().Length == 0) {
						break;
					}
					string[] toks = lines[i]
						.Split(":".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries);
					string key = toks[0];
					string value = toks[1].Trim();
					switch (key) {
						case "changeset":
							head.changeset = GetGlobalOnly(value);
							break;
						case "branch":
							head.branch = value;
							break;
						case "parent":
							head.parent = value;
							break;
						case "user":
							head.user = value;
							break;
						case "date":
							head.date = value;
							break;
						case "summary":
							head.summary = value;
							break;
						case "tag":
							head.tag = value;
							break;
					}

				}
				heads.Add(head);
			}
			return heads.ToArray();
		}

		private static string GetGlobalOnly(string value) {
			return value.Split(':')[1];
		}

		public void Commit(string message) {
			runCommand("commit -m " + @"""" + message + @"""");
		}


		public string GetCurrentBranch() {
			return runCommand("branch")[0];
		}

		public void CloseBranch() {
			runCommand(@"commit --close-branch -m ""Close Branch""");
			runCommand(@"update default");
		}

		public bool IsSynchronizedWithRemote() {
			return !(HasOutgoing() && HasIncoming());
		}

		public bool HasOutgoing() {
			return !runCommand("outgoing").Contains("no changes found");
		}

		public bool HasIncoming() {
			return !runCommand("incoming").Contains("no changes found");
		}

		public string[] Log(int limit = 0, string style = "") {
			return runCommand(@"log "
				+ (limit > 0 ? "" : ("--limit " + limit + " "))
				+ (string.IsNullOrEmpty(style) ? "" : ("--style " + style + " "))

				).ToArray();
		}

		public string[] Incoming(string style = "") {
			return runCommand(@"incoming " + (string.IsNullOrEmpty(style) ? "" : ("--style " + style))).ToArray();
		}

		public void Update() {
			var a = runCommand("pull");
		}

		Regex changeSummary = new Regex("(?<updated>[0-9]+) files? updated, (?<merged>[0-9]+) files? merged, (?<removed>[0-9]+) files? removed, (?<unresolved>[0-9]+) files? unresolved", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public bool CanMergeFromDefault() {

			if (GetModifiedFiles().Length != 0) {
				return false;
			}

			var lines = runCommand("merge default");
			string summary = lines.Find(x => changeSummary.Match(x).Success);

			var m = changeSummary.Match(summary);
			if (m.Groups["unresolved"].Value == "0") {
				return true;
			}

			runCommand("update -C");

			return false;
		}

		public void CleanBranch() {
			runCommand("update -C");
			runCommand("purge");
		}

		public void SwitchBranch(string branchname) {
			var result = runCommand("update " + branchname);

		}

		public void CreateBranch(string branchname) {
			runCommand("branch " + branchname);
			//runCommand("commit -m " + @"""Start work on " + branchname + @"""");
		}

		public Branch[] GetBranches() {
			return ProcessBranchInfo(runCommand("branches"));
		}

		private Branch[] ProcessBranchInfo(List<string> returnValues) {
			return returnValues.Select(x => new Branch() {
							 name = x.Split(" ".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries)[0],
							 changeset = GetGlobalOnly(x.Split(" ".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries)[1])
						 }).ToArray();
		}

		public Head[] GetHeadsOfUser(string user) {
			return GetHeads().Where(x => x.user.ToLower().Contains(user.ToLower())).ToArray();
		}

		public string[] GetModifiedFiles() {
			return runCommand("status")
				.Where(x => x.StartsWith("M "))
				.Select(x => FullPath(x.Substring(2)))
				.ToArray();
		}

		List<string> runCommand(string command) {
			Process p = new Process();
			ProcessStartInfo psi = new ProcessStartInfo("hg.exe", command);
			psi.WorkingDirectory = workingCopy;
			psi.UseShellExecute = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardOutput = true;
			p.StartInfo = psi;
			p.Start();

			List<string> lines = new List<string>();
			while (!p.StandardOutput.EndOfStream) {
				lines.Add(p.StandardOutput.ReadLine());
			}
			return lines;
		}

	}
}
