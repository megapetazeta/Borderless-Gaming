using System;
using System.Reflection;
using System.Windows.Forms;
using BorderlessGaming.Logic.System;

namespace BorderlessGaming.Forms
{
    public partial class AboutForm : Form
    {
        private int _foxClicks;

        public AboutForm()
        {
            InitializeComponent();
            // psst... alguien vive aqui
            versionLabel.Click += (o, a) =>
            {
                if (++_foxClicks < 5) return;
                _foxClicks = 0;
                MessageBox.Show(
                    "    /\\_/\\\r\n   ( o.o )   \U0001F98A\r\n    > ^ <\r\n\r\nHola! Soy Foxito, muy peludito y muy bonito.\r\nCuido tus ventanas sin bordes. \U0001F43E",
                    "\U0001F98A ???", MessageBoxButtons.OK, MessageBoxIcon.None);
            };
        }

        private void AboutFormLoad(object sender, EventArgs e)
        {
            // removed .Version.ToString(2) in favor of just .ToString() here so we can see the build number now
            versionLabel.Text = "Borderless Gaming " + Assembly.GetExecutingAssembly().GetName().Version;
            _copyrightLabel.Text = "Copyright © 2014-" + DateTime.Now.Year + " Andrew Sampson";
        }

        #region Project and Maintainer Links

        private void OpenBlog(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tools.GotoSite("https://andrew.im/");
        }

        private void OpenSteamGroup(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tools.GotoSite("http://steamcommunity.com/app/388080/");
        }

        private void OpenOwnerGithub(object sender, EventArgs e)
        {
            Tools.GotoSite("https://github.com/Codeusa/");
        }

        private void OpenOwnerSteam(object sender, EventArgs e)
        {
            Tools.GotoSite("http://steamcommunity.com/id/Andrewmd5/");
        }

        private void OpenGithubRepo(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Tools.GotoSite("https://github.com/Codeusa/Borderless-Gaming");
        }

        #endregion

        #region Contributers

        private void OpenDmxtGithub(object sender, EventArgs e)
        {
            Tools.GotoSite("https://github.com/dmxt/");
        }

        private void OpenImpulserGithub(object sender, EventArgs e)
        {
            Tools.GotoSite("https://github.com/Impulser/");
        }

        private void OpenStackOfPancakesGithub(object sender, EventArgs e)
        {
            Tools.GotoSite("https://github.com/Stack-of-Pancakes/");
        }

        private void OpenMadpewGithub(object sender, EventArgs e)
        {
            Tools.GotoSite("https://github.com/madpew/");
        }

        private void OpenPsouza4Github(object sender, EventArgs e)
        {
            Tools.GotoSite("https://github.com/psouza4/");
        }

        private void OpenPsouza4Steam(object sender, EventArgs e)
        {
            Tools.GotoSite("http://steamcommunity.com/id/psouza4/");
        }

        private void OpenSecretOnlineGithub(object sender, EventArgs e)
        {
            Tools.GotoSite("https://github.com/SecretOnline/");
        }

        #endregion
    }
}