using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TorrentDelivery.GUI
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void suggestFtLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Basics.Constants.GITHUB_PROJECT_ISSUES);
        }
    }
}
