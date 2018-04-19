using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MouseJiggler
{
    class MouseJigglerApplicationContext : ApplicationContext
    {
        protected bool Reverse = true;

        private NotifyIcon _NotifyIcon;
        private IContainer _IconComponents;
        private ContextMenu cmMain;

        private ToolStripMenuItem tsmiAbout;
        private ToolStripMenuItem tsmiExit;
        private ToolStripMenuItem tsmiEnable;
        private ToolStripMenuItem tsmiGhost;
        private ToolStripMenuItem tsmiHelp;


        private System.Timers.Timer _JiggleTimer = new System.Timers.Timer((double)(1000));

        public MouseJigglerApplicationContext()
        {
            InitializeContext();
            try
            {
                if ((int)Registry.CurrentUser.CreateSubKey("Software\\TGMousing\\MouseJiggle", RegistryKeyPermissionCheck.ReadWriteSubTree).GetValue("GhostEnabled", 0) != 0)
                    tsmiGhost.Checked = true;
                else
                    tsmiGhost.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error writing registry value!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Program.GhostJiggle)
                this.tsmiGhost.Checked = true;
            if (Program.JiggleEnabled)
                this.tsmiEnable.Checked = true;
        }
        /// <summary>
        /// This method sets up the Tray Icon and event handlers
        /// </summary>
        private void InitializeContext()
        {
            cmMain = new ContextMenu();
            tsmiAbout = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            tsmiEnable = new ToolStripMenuItem();
            tsmiGhost = new ToolStripMenuItem();
            tsmiHelp = new ToolStripMenuItem();
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.Image = Properties.Resources.Help.ToBitmap();
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(142, 22);
            this.tsmiHelp.Text = "Help";
            this.tsmiHelp.ToolTipText = "Displays the help screen.";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Image = Properties.Resources.Info.ToBitmap();
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(142, 22);
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.ToolTipText = "Shows version information.";
            // 
            // tsmiExit
            // 
            this.tsmiExit.Image = Properties.Resources.Exit.ToBitmap();
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(142, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.ToolTipText = "Closes the mouse jiggler application.";
            // 
            // tsmiEnable
            // 
            this.tsmiEnable.CheckOnClick = true;
            this.tsmiEnable.Name = "tsmiEnable";
            this.tsmiEnable.Size = new System.Drawing.Size(142, 22);
            this.tsmiEnable.Text = "Enable Jiggle";
            this.tsmiEnable.ToolTipText = "Enables or disables the jiggler.";
            // 
            // tsmiGhost
            // 
            this.tsmiGhost.CheckOnClick = true;
            this.tsmiGhost.Name = "tsmiGhost";
            this.tsmiGhost.Size = new System.Drawing.Size(142, 22);
            this.tsmiGhost.Text = "Ghost Jiggle";
            this.tsmiGhost.ToolTipText = "Sets the ghost jiggle on or off.";
           

            _JiggleTimer.Elapsed += new System.Timers.ElapsedEventHandler(_JiggleTimer_Elapsed);
            _JiggleTimer.Enabled = false;
            _JiggleTimer.Interval = 1000;

            _IconComponents = new Container();
            _NotifyIcon = new NotifyIcon(_IconComponents)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true,
                Text = "Mouse Jiggler",
                Icon = Properties.Resources.mouse
            };

            tsmiAbout.Click += TsmiAbout_Click;
            tsmiEnable.Click += TsmiEnable_Click;
            tsmiGhost.Click += TsmiGhost_Click;
            tsmiExit.Click += TsmiExit_Click;
            tsmiHelp.Click += TsmiHelp_Click;
            _NotifyIcon.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
            _NotifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
        }

        private void TsmiHelp_Click(object sender, EventArgs e)
        {
            Help H = new Help();
            H.Show();
        }

        private void TsmiExit_Click(object sender, EventArgs e)
        {
            _NotifyIcon.Visible = false;
            Application.Exit();
        }

        private void TsmiGhost_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\TGMousing\\MouseJiggle", RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (!tsmiGhost.Checked)
                    registryKey.SetValue("GhostEnabled", 0);
                else
                    registryKey.SetValue("GhostEnabled", 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error writing registry value!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsmiEnable_Click(object sender, EventArgs e)
        {
            _JiggleTimer.Enabled = tsmiEnable.Checked;
        }

        private void TsmiAbout_Click(object sender, EventArgs e)
        {
            About A = new About();
            A.ShowDialog();
        }
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            tsmiEnable.Checked = !tsmiEnable.Checked;
            TsmiEnable_Click(sender, e);
            if (tsmiEnable.Checked)
                _NotifyIcon.BalloonTipText = "Jiggling Enabled!";
            else
                _NotifyIcon.BalloonTipText = "Jiggling Disabled!";
            _NotifyIcon.ShowBalloonTip(2);
        }
        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            _NotifyIcon.ContextMenuStrip.Items.Clear();
            _NotifyIcon.ContextMenuStrip.Items.Add(tsmiEnable);
            _NotifyIcon.ContextMenuStrip.Items.Add(tsmiGhost);
            _NotifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _NotifyIcon.ContextMenuStrip.Items.Add(tsmiAbout);
            _NotifyIcon.ContextMenuStrip.Items.Add(tsmiHelp);
            _NotifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            _NotifyIcon.ContextMenuStrip.Items.Add(tsmiExit);
        }
        private void _JiggleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.tsmiGhost.Checked)
                Jiggler.Jiggle(0, 0);
            else if (!this.Reverse)
                Jiggler.Jiggle(-4, -4);
            else
                Jiggler.Jiggle(4, 4);
            this.Reverse = !this.Reverse;
        }
    }
}
