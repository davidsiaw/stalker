using System;
using System.Collections.Generic;
using System.Text;
using FogBugzNet;

namespace FogBugzCaseTracker
{
    using NUnit.Framework;

    [TestFixture]
    public class FogBugzTest
    {

        private void BadLogin()
        {
            FogBugz fb = new FogBugz("bad url");
            fb.Logon("bad", "bad");
        }

        [Test]
        public void TestLogin()
        {
            Assert.Throws(typeof (EURLError), new TestDelegate(BadLogin));

        }


    }
}
