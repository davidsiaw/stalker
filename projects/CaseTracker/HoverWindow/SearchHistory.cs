using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using NUnit.Framework;

namespace FogBugzCaseTracker
{
    public class SearchHistory
    {
        private int _maxSize;

        public List<string> QueryStrings { get; set; }

        RegistryKey _key;

        public SearchHistory(int howLong)
        {
            _maxSize = howLong;
            QueryStrings = new List<string>(howLong);
        }

        public void Load()
        {
            QueryStrings.Clear();
            _key = Registry.CurrentUser.OpenSubKey("Software\\VisionMap\\CaseTracker\\SearchHistory");
            try
            {

                if (_key == null)
                    return;

                for (int i = 0; i < _maxSize; ++i)
                {
                    string filter = (string)_key.GetValue(i.ToString(), "") ?? "";
                    if (filter != "")
                        QueryStrings.Add(filter);
                }
            }
            finally
            {
                if (_key != null)
                    _key.Close();

            }

        }

        private void DeleteKeyIfExists(string key)
        {
            RegistryKey k = Registry.CurrentUser.OpenSubKey(key);
            if (k != null)
            {
                k.Close();
                Registry.CurrentUser.DeleteSubKeyTree("Software\\VisionMap\\CaseTracker\\SearchHistory");
            }
        }

        public void Save()
        {
            string historyKey = @"Software\VisionMap\CaseTracker\SearchHistory";
            DeleteKeyIfExists(historyKey);

            _key = Registry.CurrentUser.CreateSubKey(historyKey);
            
            try
            {
                for (int i = 0; i < QueryStrings.Count; ++i)
                    _key.SetValue(i.ToString(), QueryStrings[i]);
            }
            finally
            {
                if (_key != null)
                    _key.Close();
            }
        }

        public void PushSearch(string filter)
        {
            if (filter == "")
                return;

            QueryStrings.RemoveAll(delegate(string val) { return val == filter; });

            QueryStrings.Insert(0, filter);
            if (QueryStrings.Count > _maxSize)
                QueryStrings.RemoveRange(_maxSize, QueryStrings.Count - _maxSize);


        }

    }

    [TestFixture]
    class TestHistory
    {
        [Test]
        public void Test()
        {
            SearchHistory sh = new SearchHistory(3);
            sh.QueryStrings.Add("");
            sh.QueryStrings.Add("");
            sh.QueryStrings.Add("");
            Assert.AreEqual("", sh.QueryStrings[0]);
            Assert.AreEqual("", sh.QueryStrings[1]);
            Assert.AreEqual("", sh.QueryStrings[2]);
            sh.Save();
            sh.Load();
            Assert.AreEqual(0, sh.QueryStrings.Count);
            sh.PushSearch("assaf");
            Assert.AreEqual("assaf", sh.QueryStrings[0]);
            Assert.AreEqual(1, sh.QueryStrings.Count);
            sh.PushSearch("lavie");
            Assert.AreEqual("lavie", sh.QueryStrings[0]);
            Assert.AreEqual("assaf", sh.QueryStrings[1]);
            Assert.AreEqual(2, sh.QueryStrings.Count);
            sh.PushSearch("again");
            Assert.AreEqual("again", sh.QueryStrings[0]);
            Assert.AreEqual("lavie", sh.QueryStrings[1]);
            Assert.AreEqual("assaf", sh.QueryStrings[2]);
            Assert.AreEqual(3, sh.QueryStrings.Count);
            sh.PushSearch("again");
            Assert.AreEqual("again", sh.QueryStrings[0]);
            Assert.AreEqual("lavie", sh.QueryStrings[1]);
            Assert.AreEqual("assaf", sh.QueryStrings[2]);
            sh.PushSearch("assaf");
            Assert.AreEqual("assaf", sh.QueryStrings[0]);
            Assert.AreEqual("again", sh.QueryStrings[1]);
            Assert.AreEqual("lavie", sh.QueryStrings[2]);
            sh.PushSearch("again");
            Assert.AreEqual("again", sh.QueryStrings[0]);
            Assert.AreEqual("assaf", sh.QueryStrings[1]);
            Assert.AreEqual("lavie", sh.QueryStrings[2]);
            sh.Save();
            SearchHistory sh2 = new SearchHistory(4);
            sh2.Load();
            Assert.AreEqual("again", sh.QueryStrings[0]);
            Assert.AreEqual("assaf", sh.QueryStrings[1]);
            Assert.AreEqual("lavie", sh.QueryStrings[2]);
            Assert.AreEqual(3, sh.QueryStrings.Count);
            sh2.Save();
            SearchHistory sh3 = new SearchHistory(2);
            sh3.Load();
            Assert.AreEqual("again", sh.QueryStrings[0]);
            Assert.AreEqual("assaf", sh.QueryStrings[1]);


        }


    }

}
