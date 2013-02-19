using System;
using System.Windows.Forms;
using System.Threading;
using FogBugzNet;

namespace FogBugzCaseTracker
{
    static class Program
    {
        static Mutex mutexSingleton;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitLogger(argv);

            FixLocale();

            HandleThreadExceptions();

            if (IsFirstInstance())
                Application.Run(new HoverWindow());

            Utils.Log.Debug("Shutting down");
        }

        private static void HandleThreadExceptions()
        {
            Application.ThreadException += delegate(object sender, ThreadExceptionEventArgs args)
            {
                Utils.Log.Error(args.Exception.ToString());
                Utils.ShowErrorMessage(args.Exception.Message);
            };
        }

        private static void FixLocale()
        {
            Utils.Log.DebugFormat("Current Local is: {0}", System.Threading.Thread.CurrentThread.CurrentCulture.DisplayName);

            Utils.Log.Debug("Setting locale of this application to en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
        }

        private static void InitLogger(string[] argv)
        {

            Utils.InitializeLog();

            if ((argv.Length > 0) && (argv[0] == "DEBUG"))
                Utils.OverrideConfiguredLogLevel(log4net.Core.Level.Debug);
        }

        private static bool IsFirstInstance()
        {
            bool firstInstance;
            mutexSingleton = new Mutex(false, "Local\\VisionMapCaseTrackerSingletonMutex", out firstInstance);
            // If firstInstance is now true, we're the first instance of the application;
            // otherwise another instance is running.
            return firstInstance;
        }
    }
}