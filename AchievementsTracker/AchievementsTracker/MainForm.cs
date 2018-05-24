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
            todoList.Add(Teamwork);
            todoList.Add(Journal);
            todoList.Add(Characters);
            todoList.Add(Casanova);
            todoList.Add(PublicEnemy);
            todoList.Add(Addicted);

            todoStatusList.Add(SpeedlunkyStatus);
            todoStatusList.Add(BigMoneyStatus);
            todoStatusList.Add(NoGoldStatus);
            todoStatusList.Add(TeamworkStatus);
            todoStatusList.Add(JournalStatus);
            todoStatusList.Add(CharactersStatus);
            todoStatusList.Add(CasanovaStatus);
            todoStatusList.Add(PublicEnemyStatus);
            todoStatusList.Add(AddictedStatus);

            drawList();
            drawStatusList();

            // Refresh statuses
            SetJournalStatus(0);
            SetCharactersStatus(0);
            SetDamselCount(0);
            SetShoppieCount(0);
            SetPlaysCount(0);

            // Reset timer
            timer.Text = FormatTime(0);
        }

        private void drawList()
        {
            int x = 12;
            int y = 70;
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
            int y = 70;
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

        public void StopTimer(long time)
        {
            runTimer.Stop();
            FinalizeTimer(time);
        }

        public void FinalizeTimer(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinalizeTimer(time)));
                return;
            }
            timer.Text = FormatTime(time - startTime);
        }

        public void UpdateTimer(object sender, EventArgs e)
        {
            if (startTime == 0)
            {
                return;
            }

            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime;

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

        private string FormatSplitTime(long time)
        {
            long splitTime = time - startTime;
            return FormatTime(splitTime);
        }

        private string FormatTime(long time)
        {
            if (time < 60 * 1000)
            {
                // time < 1 minute
                return String.Format("{0,9:0}.{1:00}", time / 1000, (time % 1000) / 10);
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

        public void FinishSpeedlunky(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishSpeedlunky(time)));
                return;
            }
            SpeedlunkyStatus.Text = FormatSplitTime(time);
            SpeedlunkyStatus.Font = new Font(SpeedlunkyStatus.Font, FontStyle.Bold);
            Speedlunky.Font = new Font(Speedlunky.Font, FontStyle.Bold);
            todoList.Remove(Speedlunky);
            todoStatusList.Remove(SpeedlunkyStatus);
            doneList.Add(Speedlunky);
            doneStatusList.Add(SpeedlunkyStatus);
            drawList();
            drawStatusList();
        }

        public void FinishBigMoney(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishBigMoney(time)));
                return;
            }
            BigMoneyStatus.Text = FormatSplitTime(time);
            BigMoneyStatus.Font = new Font(BigMoneyStatus.Font, FontStyle.Bold);
            BigMoney.Font = new Font(BigMoney.Font, FontStyle.Bold);
            todoList.Remove(BigMoney);
            todoStatusList.Remove(BigMoneyStatus);
            doneList.Add(BigMoney);
            doneStatusList.Add(BigMoneyStatus);
            drawList();
            drawStatusList();
        }

        public void FinishNoGold(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishNoGold(time)));
                return;
            }
            NoGoldStatus.Text = FormatSplitTime(time);
            NoGoldStatus.Font = new Font(NoGoldStatus.Font, FontStyle.Bold);
            NoGold.Font = new Font(NoGold.Font, FontStyle.Bold);
            todoList.Remove(NoGold);
            todoStatusList.Remove(NoGoldStatus);
            doneList.Add(NoGold);
            doneStatusList.Add(NoGoldStatus);
            drawList();
            drawStatusList();
        }

        public void FinishTeamwork(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishTeamwork(time)));
                return;
            }
            TeamworkStatus.Text = FormatSplitTime(time);
            TeamworkStatus.Font = new Font(TeamworkStatus.Font, FontStyle.Bold);
            Teamwork.Font = new Font(Teamwork.Font, FontStyle.Bold);
            todoList.Remove(Teamwork);
            todoStatusList.Remove(TeamworkStatus);
            doneList.Add(Teamwork);
            doneStatusList.Add(TeamworkStatus);
            drawList();
            drawStatusList();
        }

        public void FinishAddicted(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishAddicted(time)));
                return;
            }
            AddictedStatus.Text = FormatSplitTime(time);
            AddictedStatus.Font = new Font(AddictedStatus.Font, FontStyle.Bold);
            Addicted.Font = new Font(Addicted.Font, FontStyle.Bold);
            todoList.Remove(Addicted);
            todoStatusList.Remove(AddictedStatus);
            doneList.Add(Addicted);
            doneStatusList.Add(AddictedStatus);
            drawList();
            drawStatusList();
        }

        public void FinishNineteen(long time, int plays)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishNineteen(time, plays)));
                return;
            }

            int numDeaths = 1000 - plays;
            int numSeconds = numDeaths * 6;

            long predictedTime = time - startTime + numSeconds * 1000;

            NineteenStatus.Text = FormatTime(predictedTime);
        }

        public void FinishJournal(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishJournal(time)));
                return;
            }
            JournalStatus.Text = FormatSplitTime(time);
            JournalStatus.Font = new Font(JournalStatus.Font, FontStyle.Bold);
            Journal.Font = new Font(Journal.Font, FontStyle.Bold);
            todoList.Remove(Journal);
            todoStatusList.Remove(JournalStatus);
            doneList.Add(Journal);
            doneStatusList.Add(JournalStatus);
            drawList();
            drawStatusList();
        }

        public void FinishCharacters(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishCharacters(time)));
                return;
            }
            CharactersStatus.Text = FormatSplitTime(time);
            CharactersStatus.Font = new Font(CharactersStatus.Font, FontStyle.Bold);
            Characters.Font = new Font(Characters.Font, FontStyle.Bold);
            todoList.Remove(Characters);
            todoStatusList.Remove(CharactersStatus);
            doneList.Add(Characters);
            doneStatusList.Add(CharactersStatus);
            drawList();
            drawStatusList();
        }

        public void FinishCasanova(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishCasanova(time)));
                return;
            }
            CasanovaStatus.Text = FormatSplitTime(time);
            CasanovaStatus.Font = new Font(CasanovaStatus.Font, FontStyle.Bold);
            Casanova.Font = new Font(Casanova.Font, FontStyle.Bold);
            todoList.Remove(Casanova);
            todoStatusList.Remove(CasanovaStatus);
            doneList.Add(Casanova);
            doneStatusList.Add(CasanovaStatus);
            drawList();
            drawStatusList();
        }

        public void FinishPublicEnemy(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishPublicEnemy(time)));
                return;
            }
            PublicEnemyStatus.Text = FormatSplitTime(time);
            PublicEnemyStatus.Font = new Font(PublicEnemyStatus.Font, FontStyle.Bold);
            PublicEnemy.Font = new Font(PublicEnemy.Font, FontStyle.Bold);
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
