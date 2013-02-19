using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KilnApi;

namespace Stalker {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {

			//Application.Run(new SmallWave());

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
