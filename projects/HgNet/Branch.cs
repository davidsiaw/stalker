using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HgNet {
	public struct Branch {
		public string name;
		public string changeset;

		public override string ToString() {
			return name;
		}
	}
}
