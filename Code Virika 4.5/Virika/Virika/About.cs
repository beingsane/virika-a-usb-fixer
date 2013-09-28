using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// For the draggable code
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Virika
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText("kamranahmed.se@gmail.com");

            MessageBox.Show("Email address copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Draggable Code

        // Make the form draggable

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void About_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText("http://www.stork.site50.net");
            MessageBox.Show("Website copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText("http://www.facebook.com/kamranahmed.se");
            MessageBox.Show("My Facebook Profile link copied to clipboard", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText("pk.linkedin.com/in/kaamranahmed/");
            MessageBox.Show("My LinkedIn profile link copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText("kamranahmed.se@gmail.com");

            MessageBox.Show("Email address copied to clipboard!", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://www.stork.site50.net");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://www.facebook.com/kamranahmed.se");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://www.pk.linkedin.com/in/kaamranahmed/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://www.virika.netii.net");
        }
    }
}
