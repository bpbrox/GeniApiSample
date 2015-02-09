/****************************** Module Header ******************************\
Module Name:  MainForm.cs
Project:      GeniApiSample
Copyright (c) Bjørn P. Brox <bjorn.brox@gmail.com>

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace GeniApiSample
{
    public partial class MainForm : Form
    {
        //
        // Register a your appilcation here as a desktop application: http://www.geni.com/platform/developer/apps
        // and enter your application key and id below.
        // For Desktop Apps the application secret is not needed.
        //
        private const string ApplicationKey = "zVRR6xziYgTeyRVcpBe6eGQOsHOQQH7c6EU2wFht";
        private const int GeniAppId = 93;

        public string AppDescriptionUrl
        {
            get { return string.Format("http://www.geni.com/platform/apps/view/{0}?sec=Info", GeniAppId); }
        }
        
        public MainForm()
        {
            InitializeComponent();
            AccessToken = null;
        }

        private string _accessToken;
        private GeniProfile _userProfile;

        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
            private set
            {
                _accessToken = value;
                if (AccessToken == null)
                    UserProfile = null;
                connectBtn.Visible = AccessToken == null;
                disconnectBtn.Visible = AccessToken != null;
            }
        }

        public GeniProfile UserProfile
        {
            get { return _userProfile; }
            private set
            {
                _userProfile = value;
                UpdateUi(UserProfile);
            }
        }

        private void UpdateUi(GeniProfile profile)
        {
            if (profile == null)
            {
                userLbl.Text = string.Empty;
                bigtreeLbl.Text = string.Empty;
                accounttypeLbl.Text = string.Empty;
                emailLbl.Text = string.Empty; 
                joinedLbl.Text = string.Empty;
                residenceLbl.Text = string.Empty;
                return;
            }
            userLbl.Text = profile.Name;
            bigtreeLbl.Text = profile.BigTree.HasValue ? profile.BigTree.Value.ToString() : "<undefined>";
            accounttypeLbl.Text = profile.AccountType;
            emailLbl.Text = profile.EMail;
            joinedLbl.Text = profile.CreatedAt != null ? profile.CreatedAt.ToString() : "<undefined>";
            residenceLbl.Text = profile.CurrentResidence != null ? profile.CurrentResidence.ToString() : "<undefined>";
        }

        public void LogMsg(string message)
        {
            messagesBox.AppendText(message + Environment.NewLine);
        }

        public void LogMsg(string format, params object[] args)
        {
            LogMsg(string.Format(format, args));
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            ConnectionForm connection = new ConnectionForm(ApplicationKey);
            LogMsg("Connecting to geni.com");
            connection.ShowDialog();
            switch (connection.DialogResult)
            {
                case DialogResult.OK:
                    AccessToken = connection.AccessToken;
                    LogMsg("Connection success!");
                    break;
                case DialogResult.Cancel:
                    LogMsg("Connection cancelled!");
                    if (connection.Status != null && connection.Message != null)
                        LogMsg("    Status={0}, Message={1}",
                               connection.Status, connection.Message);
                    break;
                case DialogResult.No:
                    LogMsg("Connection failed, status={0}, message={1}",
                            connection.Status ?? "<null>", connection.Message ?? "<null>");
                    break;
            }
            if (AccessToken == null)
                return;
            UserProfile = RequestProfile("profile");
            if (UserProfile != null)
            {
                LogMsg("Connected as {0}", UserProfile.Name ?? "<null>");
            }
            else
            {
                LogMsg("Failed to get user profile");
            }
        }

        public GeniProfile RequestProfile(string profileId)
        {
            string profileUrl = "https://www.geni.com/api/" + profileId;
            JsonDictionary userProfileDict = RequestUrl(profileUrl);
            if (userProfileDict == null)
                return null;
            string id = userProfileDict["id"] as string;
            if (id != null && id.StartsWith("profile-"))
            {
                return new GeniProfile(userProfileDict);
            }
            if (id != null)
                LogMsg("RequestProfile url={0} failed, got id={1}", profileUrl, id);
            else
                LogMsg("RequestProfile url={0} failed", profileUrl);
            return null;
        }

        public JsonDictionary RequestUrl(string url, string accessToken)
        {
            try
            {
                return JsonDictionary.RequestUrl(url, accessToken);
            }
            catch (WebException ex)
            {
                LogMsg("Got WebException; {0}", ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                LogMsg("Got Exception {0}; {1}", ex.GetType(), ex.Message);
                return null;
            }
        }

        public JsonDictionary RequestUrl(string url)
        {
            return RequestUrl(url, AccessToken);
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            if (AccessToken == null)
                return;
            LogMsg("Disconnecting");

            string disconnecteUrl = string.Format("https://www.geni.com/platform/oauth/invalidate_token?access_token={0}",
                                            Uri.EscapeDataString(AccessToken));
            JsonDictionary response = RequestUrl(disconnecteUrl, null);
            if (response != null)
            {
                string result = response["result"] as string;

                LogMsg("invalidate_token returned {0}", result ?? "<null>");
                AccessToken = null;
            }
            else
            {
                LogMsg("invalidate_token failed....");
            }
        }

        private void brandingPbx_Click(object sender, EventArgs e)
        {
            // A simple trick to open a web page using the users own browser selection:
            Process.Start(AppDescriptionUrl ?? "http://www.geni.com/corp/");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnectBtn_Click(sender, e);
        }
    }
}
