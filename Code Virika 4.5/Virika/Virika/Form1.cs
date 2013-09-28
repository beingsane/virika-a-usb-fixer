using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Virika
{
    public partial class virika : Form
    {
        public Thread fixerThread;
        public string path;
        About ab;

        public virika()
        {
            InitializeComponent();
            fixerThread = new Thread(fixDrive);
        }

        private void virika_Load(object sender, EventArgs e)
        {
            populateCombo();
            if (drivesCmbo.SelectedIndex != -1)
            {
                path = drivesCmbo.SelectedItem.ToString();
            }
            notifyIcon1.ShowBalloonTip(5000);
        }

        public void populateCombo()
        {
            drivesCmbo.DataSource = null;

            drivesCmbo.DataSource = DriveInfo.GetDrives()
                    .Where(drive => drive.DriveType == DriveType.Removable && drive.IsReady && drive.TotalSize > 0).ToList();
        }

        private void refreshBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            populateCombo();

            statusLbl.Text = "";

            if (drivesCmbo.SelectedIndex != -1)
            {
                path = drivesCmbo.SelectedItem.ToString();
            }
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void fixDrive()
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Action a = new Action(() => statusLbl.Text = "Fixing your USB drive. Please Wait...");
                    this.Invoke(a);
                    System.Windows.Forms.Application.DoEvents();

                    scanDrive();

                    Action b = new Action(() => statusLbl.Text = "USB Fixed...!!");
                    this.Invoke(b);
                }
                else
                {
                    MessageBox.Show("The selected device is not ready. Please make sure that the selected device is attatched to the computer!", "USB device not detected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    Action b = new Action(() => populateCombo());
                    this.Invoke(b);

                    System.Windows.Forms.Application.DoEvents();
                }


                Action c = new Action(() => fixBtn.Enabled = true);
                this.Invoke(c);
            }

            catch
            {
                MessageBox.Show("An error occured while performing the specified task. Please restart the application. If the problem persists, please send the details at kamranahmed.se@gmail.com", "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        String[] malFiles;

        public void scanDrive()
        {
            try
            {

                malFiles = Directory
                                     .GetFiles(path, "*.*", SearchOption.AllDirectories)
                                     .Where(file => file.ToLower().EndsWith("eml") || file.ToLower().EndsWith("lnk"))
                                     .ToArray();

                applyAttribCommand();
            }
            catch
            {
                MessageBox.Show("An error occured while performing the specified task. Please restart the application. If the problem persists, please send the details at kamranahmed.se@gmail.com", "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void applyAttribCommand()
        {
            string command = "/C attrib -h -r -s /s /d " + path + "*.*";

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = command;
            process.StartInfo = startInfo;

            process.Start();

            deleteMalFiles();
        }

        public void deleteMalFiles()
        {
            foreach (string file in malFiles)
            {
                File.Delete(file);
            }
        }

        private void fixBtn_Click(object sender, EventArgs e)
        {
            statusLbl.Text = "";

            if (drivesCmbo.SelectedIndex != -1)
            {
                fixBtn.Enabled = false;

                fixerThread = new Thread(fixDrive);
                fixerThread.Start();
            }
            else
            {
                MessageBox.Show("Pleases select your USB drive first.\n\nIf you can't find your USB device listed, please make sure that the USB device is attatched to the PC and click the refresh link.", "Select a USB drive", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void drivesCmbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drivesCmbo.SelectedIndex != -1)
            {
                path = drivesCmbo.SelectedItem.ToString();
            }
        }

        private void reachMeLbl_Click(object sender, EventArgs e)
        {
            ab = new About();
            ab.Show();
        }


        #region Draggable Code

        // Make the form draggable

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

        private void drivesCmbo_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
