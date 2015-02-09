/****************************** Module Header ******************************\
Module Name:  ConnectionForm.cs
Project:      GeniApiSample
Copyright (c) Bjørn P. Brox <bjorn.brox@gmail.com>

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GeniApiSample
{
    public partial class ConnectionForm : Form
    {
        private Cursor _origCursor;

        public ConnectionForm(string applicationKey)
        {
            ApplicationKey = applicationKey;
            InitializeComponent();
        }

        public string ApplicationKey { get; set; }
        public string AccessToken { get; private set; }
        public string Status { get; private set; }
        public string Message { get; private set; }
        public string ExpiresIn { get; private set; }


        public static Dictionary<string, string> UrlDecode2Dict(string urlParams)
        {
            if (String.IsNullOrEmpty(urlParams))
                return new Dictionary<string, string>();
            if (urlParams.StartsWith("#") || urlParams.StartsWith("?"))
                urlParams = urlParams.Substring(1);
            string[] responseArgs = urlParams.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, string> dict = responseArgs.Select(item => item.Split(
                        new[] { '=' })).Where(p => p.Length == 2).ToDictionary(p => p[0], p => Uri.UnescapeDataString(p[1]));
            return dict;
        }

        private void connectionBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_origCursor != null)
            {
                // We got contact...
                Cursor.Current = _origCursor;
                _origCursor = null;
            }
            // Ignore if not what we are looking for.
            if (!e.Url.Fragment.StartsWith("#") && !e.Url.Fragment.StartsWith("?"))
                return;

            if (e.Url.AbsolutePath != "/platform/oauth/auth_success"
                    && e.Url.AbsolutePath != "/platform/oauth/auth_failed")
                return;

            // Got what we needed, - either failed or success, so we drop furter
            // 
            connectionBrowser.DocumentCompleted -= connectionBrowser_DocumentCompleted;

            Dictionary<string, string> arguments = UrlDecode2Dict(Uri.UnescapeDataString(e.Url.Fragment));

            string argValue;
            if (arguments.TryGetValue("status", out argValue))
                Status = argValue;
            if (arguments.TryGetValue("message", out argValue))
                Message = argValue;

            // Did we get an access token like this?
            // www.geni.com/platform/oauth/auth_success#access_token%3Dxxxx%26expires_in%3D86400%26scope%3D%26state%3D

            if (arguments.TryGetValue("access_token", out argValue))
            {
                AccessToken = argValue;

                if (arguments.TryGetValue("expires_in", out argValue))
                    ExpiresIn = argValue;
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.None;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            connectionBrowser.DocumentCompleted -= connectionBrowser_DocumentCompleted;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConnectionForm_Shown(object sender, EventArgs e)
        {
            // Ensure visible
            Application.DoEvents();
            string authorizeUrl =
                        String.Format("http://www.geni.com/platform/oauth/authorize?client_id={0}&response_type=token&display=desktop",
                                     Uri.EscapeDataString(ApplicationKey));
            // Here we go....
            _origCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            connectionBrowser.Navigate(authorizeUrl);
        }
    }
}
