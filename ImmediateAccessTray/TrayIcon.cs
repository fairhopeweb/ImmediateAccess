﻿using ImmediateAccessTray.Properties;
using System.Linq;
using System.Windows.Forms;

namespace ImmediateAccessTray
{
    class TrayIcon : ApplicationContext
    {
        NotifyIcon Icon;
        public TrayIcon()
        {
            ThreadExit += TrayIcon_ThreadExit;
            Icon = new NotifyIcon();
            Icon.Icon = Resources.Icon;
            Icon.Text = "Immediate Access\nAlways On VPN Service";
            Icon.MouseClick += Icon_Click;
            Icon.Visible = true;
            if (!Program.Arguments.Contains("Minimized")) Icon_Click(null, null);
            if (Program.Arguments.Contains("ElevatedStartStopService")) Program.Arguments = new string[0];
        }
        /// <summary>
        /// This event fires whenever the tray icon is pressed, triggering the main form to open.
        /// </summary>
        private void Icon_Click(object sender, MouseEventArgs e)
        {
            if (Program.TrayWindow == null || Program.TrayWindow.IsDisposed)
            {
                Program.TrayWindow = new TrayWindow();
                Program.TrayWindow.Show();
            }
            else
            {
                Program.TrayWindow.Activate();
            }
        }
        /// <summary>
        /// This event fires whenever the App Context thread exits, and hides the icon before the process ends.
        /// </summary>
        private void TrayIcon_ThreadExit(object sender, System.EventArgs e)
        {
            Icon.Visible = false;
        }
    }
}