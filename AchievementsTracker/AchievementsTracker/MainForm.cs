using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AchievementsTracker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void SetSpelunkyRunning(bool value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetSpelunkyRunning), new object[] { value });
                return;
            }
            this.runningLabel.Text = value ? "Spelunky is running!" : "Waiting for Spelunky process";
        }
    }
}
