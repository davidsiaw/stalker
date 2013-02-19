using System;
using NUnit.Framework;

namespace FogBugzCaseTracker
{
    class VersionInfo
    {
        private string _str;
        public int A;
        public int B;
        public int C;
        public int D;

        // Expects "number.number.number.number"
        public static VersionInfo FromString(string str)
        {
            VersionInfo ret = new VersionInfo();
            ret._str = str;

            String[] parts = str.Split(new char[] { '.' });
            if (parts.Length != 4)
                throw new Exception("Expecting 4 dot-separated numbers in version string: " + str);

            try
            {

                ret.A = int.Parse(parts[0]);
                ret.B = int.Parse(parts[1]);
                ret.C = int.Parse(parts[2]);
                ret.D = int.Parse(parts[3]);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ret;
        }

        public bool IsNewerThan(VersionInfo other)
        {
            if (A > other.A)
                return true;
            if (A < other.A)
                return false;
            if (B > other.B)
                return true;
            if (B < other.B)
                return false;
            if (C > other.C)
                return true;
            if (C < other.C)
                return false;
            if (D > other.D)
                return true;
            return false;
        }



    }

    [TestFixture]
    class VersionStringTest
    {

        [Test]
        public void TestParse()
        {
            string[] invalidStrings = { 
                                          "23.23.32.", 
                                          "",
                                          ".23.323.2",
                                          "a.234.asdf.233",
                                          "234.234.234.234.23.423"
                                          
                                      };
            foreach (string invalidString in invalidStrings)
                Assert.Throws<Exception>(delegate { VersionInfo.FromString(invalidString); });


            string[] validStrings = {
                                         "1.2.3.4",
                                         "11111111.22222222.3333333.444444"
                                     };
            foreach (string validString in validStrings)
                Assert.DoesNotThrow(delegate { VersionInfo.FromString(validString); });


            VersionInfo vi = VersionInfo.FromString("1.2.3.4");
            Assert.AreEqual(1, vi.A);
            Assert.AreEqual(2, vi.B);
            Assert.AreEqual(3, vi.C);
            Assert.AreEqual(4, vi.D);
        }

        [Test]
        public void TestIsNewer()
        {
            Assert.True(VersionInfo.FromString("1.2.3.4").IsNewerThan(VersionInfo.FromString("1.2.3.3")));
            Assert.False(VersionInfo.FromString("1.2.3.4").IsNewerThan(VersionInfo.FromString("1.2.3.4")));
            Assert.True(VersionInfo.FromString("1.2.3.4").IsNewerThan(VersionInfo.FromString("1.2.2.4")));
            Assert.True(VersionInfo.FromString("1.2.3.4").IsNewerThan(VersionInfo.FromString("1.1.3.4")));
            Assert.True(VersionInfo.FromString("1.2.3.4").IsNewerThan(VersionInfo.FromString("0.2.3.3")));
        }
    }

}
