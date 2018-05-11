using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AchievementsTracker
{
    public partial class MainForm : Form
    {
        private Timer runTimer;
        private long startTime;

        public MainForm()
        {
            InitializeComponent();
            runTimer = new Timer();
            runTimer.Tick += new EventHandler(UpdateTimer);
            runTimer.Interval = 50;
            runTimer.Start();
        }

        public void StartTimer()
        {
            startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public void StopTimer()
        {
            runTimer.Stop();
        }

        public void UpdateTimer(object sender, EventArgs e)
        {
            if (startTime == 0)
            {
                return;
            }

            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds() - this.startTime;

            string timerText;
            if (time < 60 * 1000)
            {
                // time < 1 minute
                timerText = String.Format("{0,10:0}.{1:00}", time / 1000, (time % 1000) / 10);
            }
            else if (time < 60 * 60 * 1000)
            {
                // time < 1 hour
                timerText = String.Format("{0,6}:{1:00}.{2:00}", time / 60000, (time % 60000) / 1000, (time % 1000) / 10);
            }
            else
            {
                // other
                timerText = String.Format("{0,2}:{1:00}:{2:00}.{3:00}", time / 3600000, (time % 3600000) / 60000, (time % 60000) / 1000, (time % 1000) / 10);
            }

            timer.Text = timerText;
        }

        public void SetSpelunkyRunning(bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetSpelunkyRunning), new object[] { value });
                return;
            }
            runningLabel.Text = value ? "Spelunky is running!" : "Waiting for Spelunky process";
        }

        public void SetDamselCount(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetDamselCount), new object[] { num });
                return;
            }
            damselCount.Text = "" + num;
        }

        public void SetShoppieCount(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetShoppieCount), new object[] { num });
                return;
            }
            shoppieCount.Text = "" + num;
        }

    }
}
