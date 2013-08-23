using System;
using System.Collections.Generic;
using System.Text;

namespace FogBugzNet
{
    public class MileStone
    {
        public string Name;
		public int ID;
		public DateTime Date;

		public string GetDateIfRelevant() {
			if (Date == new DateTime()) {
				return "";
			}
			if ((Date - DateTime.Now).Days > 3650) {
				return "";
			}

			return Date.ToString("yyyy/M/d");
		}

		public override string ToString() {
			return Name;
		}
    }
}
