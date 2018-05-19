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

        private List<Label> todoList;
        private List<Label> doneList;

        private List<Label> todoStatusList;
        private List<Label> doneStatusList;

        public MainForm()
        {
            InitializeComponent();

            // Init timer
            runTimer = new Timer();
            runTimer.Tick += new EventHandler(UpdateTimer);
            runTimer.Interval = 50;
            runTimer.Start();

            // Label lists

            todoList = new List<Label>();
            doneList = new List<Label>();

            todoStatusList = new List<Label>();
            doneStatusList = new List<Label>();

            todoList.Add(Speedlunky);
            todoList.Add(BigMoney);
            todoList.Add(NoGold);
            todoList.Add(Journal);
            todoList.Add(Characters);
            todoList.Add(Casanova);
            todoList.Add(PublicEnemy);
            todoList.Add(Teamwork);
            todoList.Add(Nineteen);
            todoList.Add(Addicted);

            todoStatusList.Add(SpeedlunkyStatus);
            todoStatusList.Add(BigMoneyStatus);
            todoStatusList.Add(NoGoldStatus);
            todoStatusList.Add(JournalStatus);
            todoStatusList.Add(CharactersStatus);
            todoStatusList.Add(CasanovaStatus);
            todoStatusList.Add(PublicEnemyStatus);
            todoStatusList.Add(TeamworkStatus);
            todoStatusList.Add(NineteenStatus);
            todoStatusList.Add(AddictedStatus);

            drawList();
            drawStatusList();

            // Refresh statuses
            SetJournalStatus(0);
            SetCharactersStatus(0);
            SetDamselCount(0);
            SetShoppieCount(0);
            SetPlaysCount(0);
        }

        private void drawList()
        {
            int x = 12;
            int y = 55;
            int dy = 35;

            foreach (Label l in doneList)
            {
                l.Location = new Point(x, y);

                y += dy;
            }

            foreach (Label l in todoList)
            {
                l.Location = new Point(x, y);

                y += dy;
            }
        }

        private void drawStatusList()
        {
            int x = 180;
            int y = 55;
            int dy = 35;

            foreach (Label l in doneStatusList)
            {
                l.Location = new Point(x, y);

                y += dy;
            }

            foreach (Label l in todoStatusList)
            {
                l.Location = new Point(x, y);

                y += dy;
            }
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

            timer.Text = FormatTime(time);
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

        private string FormatSplitTime()
        {
            long finishTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long splitTime = finishTime - startTime;
            return FormatTime(splitTime);
        }

        private string FormatTime(long time)
        {
            if (time < 60 * 1000)
            {
                // time < 1 minute
                return String.Format("{0,10:0}.{1:00}", time / 1000, (time % 1000) / 10);
            }
            else if (time < 60 * 60 * 1000)
            {
                // time < 1 hour
                return String.Format("{0,5}:{1:00}.{2:00}", time / 60000, (time % 60000) / 1000, (time % 1000) / 10);
            }
            else
            {
                // other
                return String.Format("{0,1}:{1:00}:{2:00}.{3:00}", time / 3600000, (time % 3600000) / 60000, (time % 60000) / 1000, (time % 1000) / 10);
            }
        }

        public void FinishSpeedlunky()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishSpeedlunky), new object[] { });
                return;
            }
            SpeedlunkyStatus.Text = FormatSplitTime();
            SpeedlunkyStatus.Font = new Font(SpeedlunkyStatus.Font, FontStyle.Bold);
            todoList.Remove(Speedlunky);
            todoStatusList.Remove(SpeedlunkyStatus);
            doneList.Add(Speedlunky);
            doneStatusList.Add(SpeedlunkyStatus);
            drawList();
            drawStatusList();
        }

        public void FinishBigMoney()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishBigMoney), new object[] { });
                return;
            }
            BigMoneyStatus.Text = FormatSplitTime();
            BigMoneyStatus.Font = new Font(BigMoneyStatus.Font, FontStyle.Bold);
            todoList.Remove(BigMoney);
            todoStatusList.Remove(BigMoneyStatus);
            doneList.Add(BigMoney);
            doneStatusList.Add(BigMoneyStatus);
            drawList();
            drawStatusList();
        }

        public void FinishNoGold()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishNoGold), new object[] { });
                return;
            }
            NoGoldStatus.Text = FormatSplitTime();
            NoGoldStatus.Font = new Font(NoGoldStatus.Font, FontStyle.Bold);
            todoList.Remove(NoGold);
            todoStatusList.Remove(NoGoldStatus);
            doneList.Add(NoGold);
            doneStatusList.Add(NoGoldStatus);
            drawList();
            drawStatusList();
        }

        public void FinishTeamwork()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishTeamwork), new object[] { });
                return;
            }
            TeamworkStatus.Text = FormatSplitTime();
            TeamworkStatus.Font = new Font(TeamworkStatus.Font, FontStyle.Bold);
            todoList.Remove(Teamwork);
            todoStatusList.Remove(TeamworkStatus);
            doneList.Add(Teamwork);
            doneStatusList.Add(TeamworkStatus);
            drawList();
            drawStatusList();
        }

        public void FinishAddicted()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishAddicted), new object[] { });
                return;
            }
            AddictedStatus.Text = FormatSplitTime();
            AddictedStatus.Font = new Font(AddictedStatus.Font, FontStyle.Bold);
            todoList.Remove(Addicted);
            todoStatusList.Remove(AddictedStatus);
            doneList.Add(Addicted);
            doneStatusList.Add(AddictedStatus);
            drawList();
            drawStatusList();
        }

        public void FinishNineteen()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishNineteen), new object[] { });
                return;
            }
            NineteenStatus.Text = FormatSplitTime();
            NineteenStatus.Font = new Font(NineteenStatus.Font, FontStyle.Bold);
            todoList.Remove(Nineteen);
            todoStatusList.Remove(NineteenStatus);
            doneList.Add(Nineteen);
            doneStatusList.Add(NineteenStatus);
            drawList();
            drawStatusList();
        }

        public void FinishJournal()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishJournal), new object[] { });
                return;
            }
            JournalStatus.Text = FormatSplitTime();
            JournalStatus.Font = new Font(JournalStatus.Font, FontStyle.Bold);
            todoList.Remove(Journal);
            todoStatusList.Remove(JournalStatus);
            doneList.Add(Journal);
            doneStatusList.Add(JournalStatus);
            drawList();
            drawStatusList();
        }

        public void FinishCharacters()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishCharacters), new object[] { });
                return;
            }
            CharactersStatus.Text = FormatSplitTime();
            CharactersStatus.Font = new Font(CharactersStatus.Font, FontStyle.Bold);
            todoList.Remove(Characters);
            todoStatusList.Remove(CharactersStatus);
            doneList.Add(Characters);
            doneStatusList.Add(CharactersStatus);
            drawList();
            drawStatusList();
        }

        public void FinishCasanova()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishCasanova), new object[] { });
                return;
            }
            CasanovaStatus.Text = FormatSplitTime();
            CasanovaStatus.Font = new Font(CasanovaStatus.Font, FontStyle.Bold);
            todoList.Remove(Casanova);
            todoStatusList.Remove(CasanovaStatus);
            doneList.Add(Casanova);
            doneStatusList.Add(CasanovaStatus);
            drawList();
            drawStatusList();
        }

        public void FinishPublicEnemy()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(FinishPublicEnemy), new object[] { });
                return;
            }
            PublicEnemyStatus.Text = FormatSplitTime();
            PublicEnemyStatus.Font = new Font(PublicEnemyStatus.Font, FontStyle.Bold);
            todoList.Remove(PublicEnemy);
            todoStatusList.Remove(PublicEnemyStatus);
            doneList.Add(PublicEnemy);
            doneStatusList.Add(PublicEnemyStatus);
            drawList();
            drawStatusList();
        }

        // Progress status updates

        public void SetJournalStatus(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetJournalStatus), new object[] { num });
                return;
            }
            JournalStatus.Text = String.Format("{0,5} {1}", num, "/ 114");
        }

        public void SetCharactersStatus(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetCharactersStatus), new object[] { num });
                return;
            }
            CharactersStatus.Text = String.Format("{0,5} {1}", num, "/ 16");
        }

        public void SetDamselCount(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetDamselCount), new object[] { num });
                return;
            }
            CasanovaStatus.Text = String.Format("{0,5} {1}", num, "/ 10");
        }

        public void SetShoppieCount(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetShoppieCount), new object[] { num });
                return;
            }
            PublicEnemyStatus.Text = String.Format("{0,5} {1}", num, "/ 12");
        }

        public void SetPlaysCount(int num)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetPlaysCount), new object[] { num });
                return;
            }
            AddictedStatus.Text = String.Format("{0,5} {1}", num, "/ 1000");
        }
    }
}
