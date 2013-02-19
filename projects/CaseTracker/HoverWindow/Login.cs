using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using FogBugzNet;
using System.ComponentModel;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private string _username;
        private string _server;
        private string _password;

        public struct LoginResultInfo
        {
            public String User;
            public string Password;
            public string Server;
            public DialogResult UserChoice;
        };

        LoginResultInfo DoLoginScreen(string initialUser, string initialPassword, string initialServer)
        {
            Utils.Log.Debug("Login screen showing...");
            LoginResultInfo ret = new LoginResultInfo();

            LoginForm f = new LoginForm();
            f.Password = initialPassword;
            f.UserName = initialUser;
            f.Server = initialServer;


            if (f.ShowDialog() == DialogResult.Cancel)
                ret.UserChoice = DialogResult.Cancel;
            else
            {
                ret.UserChoice = DialogResult.OK;
                ret.User = f.UserName;
                ret.Password = f.Password;
                ret.Server = f.Server;

            }
            Utils.Log.Debug("Logon screen done.");
            return ret;
        }

        private void loginWithPrompt()
        {
            loginWithPrompt(false);
        }

        private void loginWithPrompt(bool forceNewCreds)
        {
            Utils.Log.DebugFormat("Logging in to FogBugz (with prompt: {0}", forceNewCreds);
            try
            {
                SetState(new StateLoggingIn(this));
                if (forceNewCreds || _password.Length == 0 || _username.Length == 0 || _server.Length == 0 || _server == (string)ConfigurationManager.AppSettings["ExampleServerURL"])
                {
                    if (_server.Length == 0)
                    {
                        string url = ConfigurationManager.AppSettings["FogBugzBaseURL"] ?? "";
                        _server = (url.Length > 0) ? url : (string)ConfigurationManager.AppSettings["ExampleServerURL"];
                    }

                    LoginResultInfo info = DoLoginScreen(_username, _password, _server);
                    if (info.UserChoice != DialogResult.Cancel)
                    {
                        _username = info.User;
                        _password = info.Password;
                        _server = info.Server;
                    }
                }

                Utils.Log.DebugFormat("Server is at {0}", _server);

                _fb = new FogBugz(_server);

                LoginAsync(_username, _password, delegate(bool succeeded)
                {
                    if (succeeded)
                    {
                        saveSettings();
                        updateCases();
                    }
                    else
                    {
                        _password = "";
                        SetState(new StateLoggedOff(this));
                        MessageBox.Show("Login failed", "FogBugz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }
            catch (Exception x)
            {
                SetState(new StateLoggedOff(this));
                throw x;
            }
        }
        public delegate void OnLogin(bool succeeded);

        public void LoginAsync(string username, string password, OnLogin OnDone)
        {
            Utils.Log.DebugFormat("Logging in as {0}", username);
            BackgroundWorker bw = new CultureAwareBackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(delegate(object sender, DoWorkEventArgs args)
            {
                args.Result = _fb.LogOn(username, password);
            });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate(object sender, RunWorkerCompletedEventArgs args)
            {
                if (args.Error != null)
                {
                    Utils.Log.ErrorFormat("Error during login: {0}", args.Error.ToString());
                    OnDone(false);
                }
                else
                    OnDone((bool)args.Result);
            });
            bw.RunWorkerAsync();
        }
        private void RetryLogin()
        {
            Utils.Log.Debug("Retrying login...");
            LoginAsync(_username, _password, delegate(bool success)
            {
                if (success)
                    updateCases(true);
                else
                    SetState(new StateRetryLogin(this));
            });
        }



    }
}
