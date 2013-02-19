using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HgNet;
using System.Drawing;

namespace Stalker {



	class BranchEntry {
		Branch m_b;
		public BranchEntry(Branch b) {
			m_b = b;
		}

		[AutoFillColumn]
		public string Name {
			get {
				return m_b.name;
			}
		}


	}
}
