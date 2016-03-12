using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace TorrentDelivery.GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Basics.Constants.GITHUB_PROJECT_SRC);
        }

        private void onlineDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Basics.Constants.GITHUB_PROJECT_WIKI);
        }

        private void aboutTheProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}
