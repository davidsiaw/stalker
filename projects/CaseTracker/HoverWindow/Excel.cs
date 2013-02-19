using System;
using System.Collections.Generic;
using System.Text;
using FogBugzNet;
using System.Windows.Forms;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private void ExportToExcel()
        {
            try
            {
                Utils.Log.InfoFormat("Exporting case list to Excel ({0} cases)", dropCaseList.Items.Count);

                String tempTabSep = System.IO.Path.GetTempPath() + "cases_" + (Guid.NewGuid()).ToString() + ".txt";
                // create a writer and open the file
                System.IO.TextWriter tw = new System.IO.StreamWriter(tempTabSep);

                for (int i = 1; i < dropCaseList.Items.Count; ++i)
                {
                    Case c = (Case)dropCaseList.Items[i];
                    tw.WriteLine("({0:D}) {1}\t{2}h\t{3}", c.ID, c.Name, c.Estimate.TotalHours, c.AssignedTo);
                }

                tw.Close();
                System.Diagnostics.Process.Start("excel.exe", "\"" + tempTabSep + "\"");
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Sorry, couldn't launch Excel");
                Utils.Log.Error(x.ToString());
            }
        }

    }
}
