using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;


/*
 * 
 * The plan for analyzing the XML
 * ==============================
 * Parse XML into tree of project/milestone/case
 * 
 * From the root element only Projects can exist. Fail otherwise.
 * From each project only Milestones can exist, fail otherwise.
 * Under each milestones only cases can exist.
 * 
 * Project nodes begin with Project: 
 * Milestone nodes begin with MileStone:
 * Cases begin with either Bug/Feature/..etc.
 * 
 * Anything not identified as a milestone or project is a case (Task)
 * 
 * Treat every project that has a link as an existing project. If no such project found, fail (we're not into creating new projects)
 * 
 * Treat every milestone that has a link as an existing milestone. If no link, create a new milestone for the project.
 * If link exists and points to a milestone that has a different name, rename the milestone.
 * 
 * Any case that has a number (i.e. "Feature 234: ...") is an existing case.
 * If the title is different thatn the existing title, rename it.
 * If the case has no number, create a new case.
 * If the case has a parent that's different than the existing parent, update the parent.
 * Traverse children, recur.
 * 
 * Ignore cases when parsing prefixes (project, milestone, bug, features, task, etc.)
 * 
 * ==== First Stage - Done ====
 * [V] Implement only parsing and moving cases between parents.
 * [V] The root node should contain the link to the original search that yielded the mind map. Use it as comparison for changes.
 * [V] The result of analysis should be a list of action objects (for now, just "Move to Parent" actions).
 * [V] These actions need to be shown to the user for approval and then executed as batch. Basically, it's a diff before committing.
 * 
 * 
 */

using System.Xml;
namespace FogBugzNet
{

    public class ImportAnalysis
    {
        public bool NothingToDo
        {
            get
            {
                return CaseToNewParent.Count == 0;
            }
        }

        public Dictionary<Case, Case> CaseToNewParent = new Dictionary<Case, Case>();
        public string[] Describe()
        {
            List<String> ret = new List<String>();

            foreach (Case c in CaseToNewParent.Keys)
            {
                Case parent = CaseToNewParent[c];

                ret.Add(String.Format("Case {0} will be a sub-case of {1}. Here's how it's going to look:", c.ID, parent.ID ));
                ret.Add(String.Format("  Parent Case {0}: {1}", parent.ID, parent.Name));
                ret.Add(String.Format("    Sub Case {0}: {1}", c.ID, c.Name));
            }
            return ret.ToArray();
        }
    }

    public class Importer
    {
        private FogBugz _fb;
        private XmlDocument _doc;
        private Search _origSearch;
        private Dictionary<int, Case> _origCases = new Dictionary<int,Case>();

        public Importer(XmlDocument mindMap, FogBugz fb)
        {
            _fb = fb;
            _doc = mindMap;
        }

        public static bool IsCaseNode(XmlNode node)
        {
            return Regex.Match(node.Attributes["TEXT"].Value, @"(\w+) (\d+): (.*)").Success;
        }

        private Case ParseCaseNode(XmlNode node)
        {
            // Parse case link / title
            string text = node.Attributes["TEXT"].Value;
            Match m = Regex.Match(text, @"(\w+) (\d+): (.*)");
            if (!m.Success)
                return null;

            Case c = new Case();
            c.Category = m.Groups[1].Value;
            c.ID = int.Parse(m.Groups[2].Value);
            c.Name = m.Groups[3].Value;
            return c;
        }

        private MileStone ParseMileStoneNode(XmlNode node)
        {
            MileStone ret = new MileStone();
            Match m = Regex.Match(node.Attributes["TEXT"].Value, "MileStone: (.*)");

            if (!m.Success)
                return null;

            ret.Name = m.Groups[1].Value;
            m = Regex.Match(node.Attributes["LINK"].Value, @"ixFixFor=(\d+)");
            if (!m.Success)
                return null;

            ret.ID = int.Parse(m.Groups[1].Value);

            return ret;
        }

        public ImportAnalysis Analyze()
        {
            RedoOriginalSearch();

            IndexOriginalCases();

            ImportAnalysis a = new ImportAnalysis();

            XmlNodeList nodes = _doc.SelectNodes("//node");
            FindCasesWithNewParents(nodes, a);
            return a;
        }

        private void FindCasesWithNewParents(XmlNodeList nodes, ImportAnalysis a)
        {
            foreach (XmlNode node in nodes)
            {
                Case c = ParseCaseNode(node);
                if (c == null) // If not a case
                    continue;


                Case parent = ParseCaseNode(node.ParentNode);
                if (parent == null)
                {
                    continue; // TODO: Figure out a way to detect when a case was moved to the root of the milestone
                }
                else
                    c.ParentCaseID = parent.ID;

                // Now we have a case with a parent case check to see if it differs from the original case's parent
                if (_origCases.ContainsKey(c.ID) && _origCases[c.ID].ParentCaseID != c.ParentCaseID)
                {
                    a.CaseToNewParent.Add(c, parent);
                }
            }
        }

        private string OriginalSearchLink
        {
            get {
                return _doc.SelectSingleNode("/map/node").Attributes["LINK"].Value;             
            }
        }

        private void IndexOriginalCases()
        {
            foreach (Case c in _origSearch.Cases)
                _origCases.Add(c.ID, c);
        }
        private void RedoOriginalSearch()
        {
            _origSearch = new Search();
            _origSearch.Query = Regex.Match(OriginalSearchLink, "searchFor=(.*)").Groups[1].Value;
			_origSearch.Cases = _fb.UpdateAllStuff(_origSearch.Query, (x, y) => { });
        }
        
    }
}
