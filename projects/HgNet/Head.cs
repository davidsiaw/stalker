using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stalker;

namespace HgNet {
	public struct Head {
		public string changeset;
		public string branch;
		public string parent;
		public string user;
		public string date;
		public string summary;
		public string tag;

		public override string ToString() {
			return (branch ?? "default") + ":" + changeset;
		}

		[AutoFillColumn]
		public string BranchName { get { return branch ?? "default"; } }

		[AutoSizeColumn]
		public string Date { get { return date; } }

		[AutoSizeColumn]
		public string ChangeSet { get { return changeset; } }
	}
}
