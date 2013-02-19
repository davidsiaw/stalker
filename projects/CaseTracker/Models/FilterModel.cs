using System;
using System.Collections.Generic;
using System.Text;
using FogBugzNet;
using System.Configuration;

namespace FogBugzCaseTracker
{
    public class FilterModel
    {
        public String BaseSearch = ConfigurationManager.AppSettings["BaseSearch"];
        public bool IncludeNoEstimate = true;
        public String UserSearch;
        public Case[] Cases;
        public bool IgnoreBaseSearch;
        public SearchHistory History;

        public string FormatSearchQuery()
        {
            String ret = UserSearch;

            if (!IgnoreBaseSearch)
                ret = BaseSearch + " " + ret;

            if (!IncludeNoEstimate)
                ret = ret + " -CurrentEstimate:\"0\"";
            return ret;
        }
    }
}
