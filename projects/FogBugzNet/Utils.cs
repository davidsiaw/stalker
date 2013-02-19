using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace FogBugzNet
{
    public class Utils
    {
        public static void ShowErrorMessage(string error, string title)
        {
            Utils.Log.WarnFormat("User error shown: {0}, {1}", title, error);
            MessageBox.Show(error, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorMessage(string error)
        {
            ShowErrorMessage(error, "Error Encountered");
        }

        private static byte[] entropy = new byte[] { 0x23, 0x10, 0x19, 0x79, 0x18, 0x89, 0x04 };

        // Only ASCII text. Based on example here: http://blogs.msdn.com/shawnfa/archive/2004/05/05/126825.aspx
        public static byte[] EncryptCurrentUser(String text)
        {
            if (text == null)
                throw new Exception("Cannot encrypt null string");

            if (text.Length == 0)
                throw new Exception("Cannot encrypt empty string");

            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(text);

            return ProtectedData.Protect(buffer, entropy, DataProtectionScope.CurrentUser);
        }

        public static string DecryptCurrentUser(byte[] buffer)
        {

            byte[] unprotectedBytes = ProtectedData.Unprotect(buffer, entropy, DataProtectionScope.CurrentUser);

            return ASCIIEncoding.ASCII.GetString(unprotectedBytes);
        }

        public static ILog Log = log4net.LogManager.GetLogger("CaseTracker");

        public static void InitializeLog()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("Initializing Logs");

        }

        public static string ToIsoTimeString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static void OverrideConfiguredLogLevel(log4net.Core.Level l)
        {
            log4net.Repository.Hierarchy.Hierarchy h = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
            log4net.Repository.Hierarchy.Logger rootLogger = h.Root;
            rootLogger.Level = l;
        }


    }
}
