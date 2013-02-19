using System;
using System.IO;
using System.Text;
using FogBugzNet;
using NUnit.Framework;
using System.Globalization;

namespace FogBugzCaseTracker
{
    [TestFixture]
    public class Crypto
    {
        public static void VerifyDownloadedFileHash(string filename, string expectedHash, System.Security.Cryptography.HashAlgorithm alg)
        {
            Utils.Log.InfoFormat("Verifying downloaded setup file {0}, {1}", filename, expectedHash);
            string actualHashStr = ComputeFileHash(filename, alg);
            if (actualHashStr.ToUpper() != expectedHash.ToUpper())
            {
                Utils.Log.Warn("Actual hash was: " + actualHashStr);
                File.Delete(filename);
                throw new Exception(String.Format(CultureInfo.InvariantCulture, "Bad hash of downloaded version.\nExpected: {0}\n  Actual: {1}", expectedHash, actualHashStr));
            }
        }

        public static string ComputeFileHash(string filename, System.Security.Cryptography.HashAlgorithm alg)
        {
            StringBuilder sb = new StringBuilder();
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (fs)
            {
                byte[] actualHash = alg.ComputeHash(fs);
                foreach (Byte b in actualHash)
                    sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        [Test]
        public void TestSHA1()
        {
            const string data = "test";
            const string expectedSha1 = "A94A8FE5CCB19BA61C4C0873D391E987982FBBD3";

            string tmpFile = Path.GetTempFileName();
            File.WriteAllText(tmpFile, data);
            Assert.DoesNotThrow(delegate { VerifyDownloadedFileHash(tmpFile, expectedSha1, new System.Security.Cryptography.SHA1Managed()); });
        }

    }
}
