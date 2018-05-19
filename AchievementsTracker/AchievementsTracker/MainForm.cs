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
            if (startTime == 0)
            {
                startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            }
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
                timerText = String.Format("{0,5}:{1:00}.{2:00}", time / 60000, (time % 60000) / 1000, (time % 1000) / 10);
            }
            else
            {
                // other
                timerText = String.Format("{0,1}:{1:00}:{2:00}.{3:00}", time / 3600000, (time % 3600000) / 60000, (time % 60000) / 1000, (time % 1000) / 10);
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

        public void FinishSpeedlunky()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishSpeedlunky), new object[] { });
                return;
            }
            SpeedlunkyStatus.Text = "Done";
        }

        public void FinishBigMoney()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishBigMoney), new object[] { });
                return;
            }
            BigMoneyStatus.Text = "Done";
        }

        public void FinishNoGold()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishNoGold), new object[] { });
                return;
            }
            NoGoldStatus.Text = "Done";
        }

        public void FinishTeamwork()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishTeamwork), new object[] { });
                return;
            }
            TeamworkStatus.Text = "Done";
        }

        public void SetJournalStatus(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetJournalStatus), new object[] { num });
                return;
            }
            if (num == 114)
            {
                JournalStatus.Text = "Done";
            }
            else
            {
                JournalStatus.Text = num + "/114";
            }
        }

        public void SetCharactersStatus(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetCharactersStatus), new object[] { num });
                return;
            }
            if (num == 16)
            {
                CharactersStatus.Text = "Done";
            }
            else
            {
                CharactersStatus.Text = num + "/16";
            }
        }

        public void SetDamselCount(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetDamselCount), new object[] { num });
                return;
            }
            if (num >= 10)
            {
                CasanovaStatus.Text = "Done";
            } else
            {
                CasanovaStatus.Text = num + "/10";
            }
        }

        public void SetShoppieCount(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetShoppieCount), new object[] { num });
                return;
            }
            if (num >= 12)
            {
                PublicEnemyStatus.Text = "Done";
            } else
            {
                PublicEnemyStatus.Text = num + "/12";
            }

        }

    }
}
