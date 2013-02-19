using System;
using System.Collections.Generic;
using System.Text;
using FogBugzNet;

namespace FogBugzNet
{
    public class Search
    {
        public string Query;
        public Case[] Cases;
        public Search ()
        {


        }
        public Search (string query, Case[] cases)
        {
            Cases = cases;
            Query = query;
        }
    }
}
