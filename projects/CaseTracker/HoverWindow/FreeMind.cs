using System;
using System.Collections.Generic;
using System.Text;
using FogBugzNet;
using System.Xml;
using System.Windows.Forms;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private void ExportToFreeMind()
        {
            try
            {

                String tempTabSep = System.IO.Path.GetTempPath() + "cases_" + (Guid.NewGuid()).ToString() + ".mm";
                // create a writer and open the file

                Exporter ex = new Exporter(_fb, new Search(_filter.FormatSearchQuery(), _cases));
                ex.CasesToMindMap().Save(tempTabSep);

                System.Diagnostics.Process.Start("\"" + tempTabSep + "\"");
            }
            catch (System.Exception x)
            {
                MessageBox.Show("Sorry, couldn't launch Excel");
                Utils.Log.Error(x.ToString());
            }
        }
        private void DoImport()
        {
            XmlDocument doc = GetMindMapFromUser();
            if (doc == null)
                return;

            Importer imp = new Importer(doc, _fb);
            ImportAnalysis results = imp.Analyze();
            if (results.NothingToDo)
            {
                MessageBox.Show("No changes were detected. Nothing to import.", "Import Mind Map", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ImportConfirmationDlg dlg = new ImportConfirmationDlg(results);

            if (dlg.ShowDialog() == DialogResult.Yes)
            {
                foreach (Case c in results.CaseToNewParent.Keys)
                {
                    try
                    {
                        _fb.SetParent(c, results.CaseToNewParent[c].ID);
                    }
                    catch (Exception x)
                    {
                        Utils.Log.Error(x.ToString());
                    }
                }
            }
        }

        XmlDocument GetMindMapFromUser()
        {

            OpenFileDialog d = new OpenFileDialog();

            d.CheckFileExists = true;
            d.Multiselect = false;
            d.Filter = "FreeMind files (*.mm)|*.mm|All files (*.*)|*.*";

            if (d.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(d.FileName);
                    return doc;
                }
                catch (Exception x)
                {
                    Utils.Log.Error(x.ToString());
                }
            }

            return null;
        }


    }
}
