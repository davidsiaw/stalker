using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text;
using FogBugzNet;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace FogBugzCaseTracker
{
    public partial class HoverWindow
    {
        private SettingsModel _settings;

        private RegistryKey _settingsRegKey;
        private void saveSettings()
        {
            Utils.Log.Debug("Saving settings to registry");

            _settingsRegKey = Registry.CurrentUser.CreateSubKey("Software\\VisionMap\\CaseTracker");
            _settingsRegKey.SetValue("username", _username);
            _settingsRegKey.SetValue("password", Convert.ToBase64String(Utils.EncryptCurrentUser(_password)));
            _settingsRegKey.SetValue("server", _server);
            _settingsRegKey.SetValue("LastX", Location.X);
            _settingsRegKey.SetValue("LastY", Location.Y);
            _settingsRegKey.SetValue("IgnoreBaseSearch", _filter.IgnoreBaseSearch ? 1 : 0);
            _settingsRegKey.SetValue("IncludeNoEstimate", _filter.IncludeNoEstimate ? 1 : 0);
            _settingsRegKey.SetValue("LastWidth", Width);
            _settings.SaveToRegistry(_settingsRegKey);
            _settingsRegKey.Close();
            _filter.History.Save();
        }

        private void loadSettings()
        {
            _settings = ExtractModelFromUI();
            Utils.Log.Debug("Loading settings from registry");

            _filter.History = new SearchHistory(int.Parse(ConfigurationManager.AppSettings["SearchFilterHistorySize"]));
            _filter.History.Load();
            _filter.UserSearch = (_filter.History.QueryStrings.Count > 0) ? _filter.History.QueryStrings[0] : ConfigurationManager.AppSettings["DefaultNarrowSearch"];

            _settingsRegKey = Registry.CurrentUser.OpenSubKey("Software\\VisionMap\\CaseTracker");
            if (_settingsRegKey == null)
                ResetAuthenticationData();
            else
            {
                RestoreAuthenticationData();
                ReadSettingsFromRegKey();
                _settingsRegKey.Close();
                ApplySettings();
            }
        }

        private void ApplySettings()
        {
            Opacity = _settings.Opacity;
            dropCaseList.Font = _settings.UserFont;
            timerUpdateCases.Interval = _settings.CaseListRefreshInterval_Secs * 1000;
        }

        private SettingsModel ExtractModelFromUI()
        {
            SettingsModel ret = new SettingsModel();
            ret.Opacity = Opacity;
            ret.UserFont = (Font)dropCaseList.Font.Clone();
            return ret;
        }

        private void ReadSettingsFromRegKey()
        {

            SettingsModel defaultValues = ExtractModelFromUI();
            _settings.LoadFromRegistry(_settingsRegKey, defaultValues);
            Point newLoc = new Point();
            timerRetryLogin.Interval = int.Parse(ConfigurationManager.AppSettings["RetryLoginInterval_ms"]);

            newLoc.X = (int)_settingsRegKey.GetValue("LastX", Location.X);
            newLoc.Y = (int)_settingsRegKey.GetValue("LastY", Location.Y);


            Location = newLoc;

            Width = (int)_settingsRegKey.GetValue("LastWidth", Width);
            _filter.IgnoreBaseSearch = (int)_settingsRegKey.GetValue("IgnoreBaseSearch", bool.Parse(ConfigurationManager.AppSettings["IgnoreBaseSearch"]) ? 1 : 0) != 0;
            _filter.IncludeNoEstimate = (int)_settingsRegKey.GetValue("IncludeNoEstimate", bool.Parse(ConfigurationManager.AppSettings["IncludeNoEstimates"]) ? 1 : 0) != 0;
        }

        private void RestoreAuthenticationData()
        {
            _username = (String)_settingsRegKey.GetValue("username", "");
            _server = (String)_settingsRegKey.GetValue("server", "");

            if (_server == "")
                _server = (string)ConfigurationManager.AppSettings["FogBugzBaseURL"];

            RecoverPassword(_settingsRegKey);
        }

        private void ResetAuthenticationData()
        {
            _username = "";
            _password = "";
            _server = "";
        }

        private void RecoverPassword(RegistryKey key)
        {
            try
            {
                string regVal = (String)key.GetValue("password", "");
                if (regVal == "")
                    _password = "";
                else
                    _password = Utils.DecryptCurrentUser(Convert.FromBase64String(regVal));
            }
            catch (System.FormatException x)
            {
                _password = ""; // Don't bother the user about the malformed pwd in the registry, but do log this
                Utils.Log.Error("Base 64 of pwd is bad: " + x.ToString());
            }
            catch (System.Security.Cryptography.CryptographicException x)
            {
                _password = ""; // Don't bother the user about the malformed pwd in the registry, but do log this
                Utils.Log.Error("Unable to decode password: " + x.ToString());
            }

        }

        private void ShowSettingsDialog()
        {
            SettingsDlg dlg = new SettingsDlg();

            dlg.Owner = this;
            dlg.LoadModel(_settings);
            PositionDialogBelowOrAboveWindow(dlg);

            SettingsModel oldSettings = (SettingsModel)_settings.Clone();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _settings = dlg.SaveModel();
                saveSettings();
            }
            else
                _settings = oldSettings;
            ApplySettings();
        }

    } // class HoverWindow
} // ns
