using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AchievementsTracker.Program;

namespace AchievementsTracker
{

    public partial class MainForm : Form
    {

        public const int FORM_WIDTH = 320;
        public const int FORM_TIMER_DY = 40;
        public const int FORM_LABEL_X = 12;
        public const int FORM_LABEL_Y = 70;
        public const int FORM_LABEL_DX = 168;
        public const int FORM_LABEL_DY = 35;
        public const int FORM_TIMER_HEIGHT = 105;

        const int RESET_HOTKEY_ID = 0;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Timer runTimer;
        private long startTime;

        private List<Label> todoList;
        private List<Label> doneList;

        private List<Label> todoStatusList;
        private List<Label> doneStatusList;

        private TrayApplicationContext context;

        // Default to AA
        private Category category = Category.AA;

        private long achievementsFinishTime;
        private long journalFinishTime;
        private long charactersFinishTime;
        private long asoFinishTime;
        private long tutorialFinishTime;

        private int journalNum;

        public MainForm(TrayApplicationContext ctx)
        {
            context = ctx;

            InitializeComponent();

            Reset();
        }

        public void SetResetHotKey(int modifiers, Keys key)
        {
            Log.WriteLine("Set reset hotkey. Mods: " + modifiers + " Key: " + key);
            UnregisterHotKey(Handle, RESET_HOTKEY_ID);
            RegisterHotKey(Handle, RESET_HOTKEY_ID, modifiers, (int)key);
        }
        
        // Hotkey
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == RESET_HOTKEY_ID)
            {
                context.Reset(null, null);
                Log.WriteLine("Reset hotkey triggered");
            }
            base.WndProc(ref m);
        }

        public void Reset()
        {
            room.Text = "";

            startTime = 0;
            
            // Init timer
            if (runTimer != null)
            {
                runTimer.Stop();
            }
            runTimer = new Timer();
            runTimer.Tick += new EventHandler(UpdateTimer);
            runTimer.Interval = 50;
            runTimer.Start();

            // Reset completion times
            achievementsFinishTime = 0;
            journalFinishTime = 0;
            charactersFinishTime = 0;
            asoFinishTime = 0;
            tutorialFinishTime = 0;

            // Label lists

            todoList = new List<Label>();
            doneList = new List<Label>();

            todoStatusList = new List<Label>();
            doneStatusList = new List<Label>();

            todoList.Add(Tutorial);
            todoList.Add(Speedlunky);
            todoList.Add(BigMoney);
            todoList.Add(NoGold);
            todoList.Add(Teamwork);
            todoList.Add(Journal);
            todoList.Add(Characters);
            todoList.Add(Casanova);
            todoList.Add(PublicEnemy);
            todoList.Add(Addicted);

            todoList.Add(aso1Bomb);
            todoList.Add(aso1Rope);
            todoList.Add(aso10k);
            todoList.Add(aso2Bombs);
            todoList.Add(aso2Ropes);
            todoList.Add(asoShotgun);
            todoList.Add(aso3Bombs);
            todoList.Add(aso3Ropes);
            todoList.Add(asoKey);
            todoList.Add(asoOlmec);

            todoStatusList.Add(TutorialStatus);
            todoStatusList.Add(SpeedlunkyStatus);
            todoStatusList.Add(BigMoneyStatus);
            todoStatusList.Add(NoGoldStatus);
            todoStatusList.Add(TeamworkStatus);
            todoStatusList.Add(JournalStatus);
            todoStatusList.Add(CharactersStatus);
            todoStatusList.Add(CasanovaStatus);
            todoStatusList.Add(PublicEnemyStatus);
            todoStatusList.Add(AddictedStatus);

            todoStatusList.Add(aso1BombStatus);
            todoStatusList.Add(aso1RopeStatus);
            todoStatusList.Add(aso10kStatus);
            todoStatusList.Add(aso2BombsStatus);
            todoStatusList.Add(aso2RopesStatus);
            todoStatusList.Add(asoShotgunStatus);
            todoStatusList.Add(aso3BombsStatus);
            todoStatusList.Add(aso3RopesStatus);
            todoStatusList.Add(asoKeyStatus);
            todoStatusList.Add(asoOlmecStatus);

            drawList();
            drawStatusList();

            // Refresh labels
            Tutorial.Font = new Font(Tutorial.Font, FontStyle.Regular);
            Journal.Font = new Font(Journal.Font, FontStyle.Regular);
            Characters.Font = new Font(Characters.Font, FontStyle.Regular);
            Speedlunky.Font = new Font(Speedlunky.Font, FontStyle.Regular);
            BigMoney.Font = new Font(BigMoney.Font, FontStyle.Regular);
            NoGold.Font = new Font(NoGold.Font, FontStyle.Regular);
            Casanova.Font = new Font(Casanova.Font, FontStyle.Regular);
            PublicEnemy.Font = new Font(PublicEnemy.Font, FontStyle.Regular);
            Teamwork.Font = new Font(Teamwork.Font, FontStyle.Regular);
            Addicted.Font = new Font(Addicted.Font, FontStyle.Regular);
            ASO.Font = new Font(ASO.Font, FontStyle.Regular);
            aso1Bomb.Font = new Font(aso1Bomb.Font, FontStyle.Regular);
            aso1Rope.Font = new Font(aso1Rope.Font, FontStyle.Regular);
            aso10k.Font = new Font(aso10k.Font, FontStyle.Regular);
            aso2Bombs.Font = new Font(aso2Bombs.Font, FontStyle.Regular);
            aso2Ropes.Font = new Font(aso2Ropes.Font, FontStyle.Regular);
            asoShotgun.Font = new Font(asoShotgun.Font, FontStyle.Regular);
            aso3Bombs.Font = new Font(aso3Bombs.Font, FontStyle.Regular);
            aso3Ropes.Font = new Font(aso3Ropes.Font, FontStyle.Regular);
            asoKey.Font = new Font(asoKey.Font, FontStyle.Regular);
            asoOlmec.Font = new Font(asoOlmec.Font, FontStyle.Regular);

            // Refresh statuses
            SetJournalStatus(0);
            SetCharactersStatus(0);
            SetDamselCount(0);
            SetShoppieCount(0);
            SetPlaysCount(0);
            TutorialStatus.Font = new Font(TutorialStatus.Font, FontStyle.Regular);
            JournalStatus.Font = new Font(JournalStatus.Font, FontStyle.Regular);
            CharactersStatus.Font = new Font(CharactersStatus.Font, FontStyle.Regular);
            CasanovaStatus.Font = new Font(CasanovaStatus.Font, FontStyle.Regular);
            PublicEnemyStatus.Font = new Font(PublicEnemyStatus.Font, FontStyle.Regular);
            AddictedStatus.Font = new Font(AddictedStatus.Font, FontStyle.Regular);
            ASOStatus.Font = new Font(ASOStatus.Font, FontStyle.Regular);
            SpeedlunkyStatus.Text = "";
            BigMoneyStatus.Text = "";
            NoGoldStatus.Text = "";
            TeamworkStatus.Text = "";
            ExtrapolatedTimeStatus.Text = "";
            TutorialStatus.Text = "";
            ASOStatus.Text = "1 Bomb";
            aso1BombStatus.Font = new Font(aso1BombStatus.Font, FontStyle.Regular);
            aso1RopeStatus.Font = new Font(aso1RopeStatus.Font, FontStyle.Regular);
            aso10kStatus.Font = new Font(aso10kStatus.Font, FontStyle.Regular);
            aso2BombsStatus.Font = new Font(aso2BombsStatus.Font, FontStyle.Regular);
            aso2RopesStatus.Font = new Font(aso2RopesStatus.Font, FontStyle.Regular);
            asoShotgunStatus.Font = new Font(asoShotgunStatus.Font, FontStyle.Regular);
            aso3BombsStatus.Font = new Font(aso3BombsStatus.Font, FontStyle.Regular);
            aso3RopesStatus.Font = new Font(aso3RopesStatus.Font, FontStyle.Regular);
            asoKeyStatus.Font = new Font(asoKeyStatus.Font, FontStyle.Regular);
            asoOlmecStatus.Font = new Font(asoOlmecStatus.Font, FontStyle.Regular);
            aso1BombStatus.Text = "";
            aso1RopeStatus.Text = "";
            aso10kStatus.Text = "";
            aso2BombsStatus.Text = "";
            aso2RopesStatus.Text = "";
            asoShotgunStatus.Text = "";
            aso3BombsStatus.Text = "";
            aso3RopesStatus.Text = "";
            asoKeyStatus.Text = "";
            asoOlmecStatus.Text = "";

            // Reset timer
            timer.Text = FormatTime(0);
        }

        public void setRoomCode(string code)
        {
            if (code != null)
            {
                room.Text = "Room: " + code;
            } else
            {
                room.Text = "";
            }
        }

        public void changeCategory(Category cat)
        {
            this.category = cat;
        }

        public void drawList()
        {
            int x = FORM_LABEL_X;
            int y = FORM_LABEL_Y;
            int dy = FORM_LABEL_DY;

            foreach (Label l in doneList)
            {
                l.Location = new Point(x, y);

                if (l.Visible) y += dy;
            }

            foreach (Label l in todoList)
            {
                l.Location = new Point(x, y);

                if (l.Visible) y += dy;
            }

            timer.Location = new Point(-5, y + FORM_TIMER_DY);
            this.Size = new Size(FORM_WIDTH, y + FORM_TIMER_DY + FORM_TIMER_HEIGHT);
        }

        public void drawStatusList()
        {
            int x = FORM_LABEL_X + FORM_LABEL_DX;
            int y = FORM_LABEL_Y;
            int dy = FORM_LABEL_DY;

            foreach (Label l in doneStatusList)
            {
                l.Location = new Point(x, y);

                if (l.Visible) y += dy;
            }

            foreach (Label l in todoStatusList)
            {
                l.Location = new Point(x, y);

                if (l.Visible) y += dy;
            }
        }

        public void StartTimer(long time, bool coop)
        {
            if (startTime == 0 || coop)
            {
                startTime = time;
            }
        }

        public void FinishAA(long time)
        {
            SetTimer(time);

            achievementsFinishTime = time;
        }

        public void SetTimer(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetTimer(time)));
                return;
            }
            timer.Text = FormatTime(time - startTime);
        }

        public void UpdateTimer(object sender, EventArgs e)
        {
            // Make sure run has started
            if (startTime == 0)
            {
                return;
            }

            // Get current time
            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // If selected category is complete, display the completion time instead
            switch (this.category)
            {
                case Category.AA:
                    if (achievementsFinishTime != 0) time = achievementsFinishTime;
                    break;
                case Category.AJE:
                    if (journalFinishTime != 0) time = journalFinishTime;
                    break;
                case Category.AC:
                    if (charactersFinishTime != 0) time = charactersFinishTime;
                    break;
                case Category.ASO:
                    if (asoFinishTime != 0) time = asoFinishTime;
                    break;
                case Category.Tutorial:
                    if (tutorialFinishTime != 0) time = tutorialFinishTime;
                    break;
            }

            // Subtract off start time of run
            time -= startTime;

            timer.Text = FormatTime(time);
        }

        public void SetSpelunkyRunning(bool value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetSpelunkyRunning), new object[] { value });
                return;
            }
            runningLabel.Text = value ? "Running" : "Waiting";
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

        public void FinishTutorial(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishTutorial(time)));
                return;
            }
            TutorialStatus.Text = FormatSplitTime(time);
            TutorialStatus.Font = new Font(TutorialStatus.Font, FontStyle.Bold);
            Tutorial.Font = new Font(Tutorial.Font, FontStyle.Bold);
            todoList.Remove(Tutorial);
            todoStatusList.Remove(TutorialStatus);
            doneList.Add(Tutorial);
            doneStatusList.Add(TutorialStatus);
            drawList();
            drawStatusList();

            tutorialFinishTime = time;
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

            ExtrapolatedTimeStatus.Text = FormatTime(predictedTime);
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

            journalFinishTime = time;
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

            charactersFinishTime = time;
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

        public void FinishASO1Bomb(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASO1Bomb(time)));
                return;
            }
            aso1BombStatus.Text = FormatSplitTime(time);
            aso1BombStatus.Font = new Font(aso1BombStatus.Font, FontStyle.Bold);
            aso1Bomb.Font = new Font(aso1Bomb.Font, FontStyle.Bold);
            todoList.Remove(aso1Bomb);
            todoStatusList.Remove(aso1BombStatus);
            doneList.Add(aso1Bomb);
            doneStatusList.Add(aso1BombStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASO1Rope(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASO1Rope(time)));
                return;
            }
            aso1RopeStatus.Text = FormatSplitTime(time);
            aso1RopeStatus.Font = new Font(aso1RopeStatus.Font, FontStyle.Bold);
            aso1Rope.Font = new Font(aso1Rope.Font, FontStyle.Bold);
            todoList.Remove(aso1Rope);
            todoStatusList.Remove(aso1RopeStatus);
            doneList.Add(aso1Rope);
            doneStatusList.Add(aso1RopeStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASO10k(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASO10k(time)));
                return;
            }
            aso10kStatus.Text = FormatSplitTime(time);
            aso10kStatus.Font = new Font(aso10kStatus.Font, FontStyle.Bold);
            aso10k.Font = new Font(aso10k.Font, FontStyle.Bold);
            todoList.Remove(aso10k);
            todoStatusList.Remove(aso10kStatus);
            doneList.Add(aso10k);
            doneStatusList.Add(aso10kStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASO2Bombs(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASO2Bombs(time)));
                return;
            }
            aso2BombsStatus.Text = FormatSplitTime(time);
            aso2BombsStatus.Font = new Font(aso2BombsStatus.Font, FontStyle.Bold);
            aso2Bombs.Font = new Font(aso2Bombs.Font, FontStyle.Bold);
            todoList.Remove(aso2Bombs);
            todoStatusList.Remove(aso2BombsStatus);
            doneList.Add(aso2Bombs);
            doneStatusList.Add(aso2BombsStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASO2Ropes(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASO2Ropes(time)));
                return;
            }
            aso2RopesStatus.Text = FormatSplitTime(time);
            aso2RopesStatus.Font = new Font(aso2RopesStatus.Font, FontStyle.Bold);
            aso2Ropes.Font = new Font(aso2Ropes.Font, FontStyle.Bold);
            todoList.Remove(aso2Ropes);
            todoStatusList.Remove(aso2RopesStatus);
            doneList.Add(aso2Ropes);
            doneStatusList.Add(aso2RopesStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASOShotgun(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASOShotgun(time)));
                return;
            }
            asoShotgunStatus.Text = FormatSplitTime(time);
            asoShotgunStatus.Font = new Font(asoShotgunStatus.Font, FontStyle.Bold);
            asoShotgun.Font = new Font(asoShotgun.Font, FontStyle.Bold);
            todoList.Remove(asoShotgun);
            todoStatusList.Remove(asoShotgunStatus);
            doneList.Add(asoShotgun);
            doneStatusList.Add(asoShotgunStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASO3Bombs(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASO3Bombs(time)));
                return;
            }
            aso3BombsStatus.Text = FormatSplitTime(time);
            aso3BombsStatus.Font = new Font(aso3BombsStatus.Font, FontStyle.Bold);
            aso3Bombs.Font = new Font(aso3Bombs.Font, FontStyle.Bold);
            todoList.Remove(aso3Bombs);
            todoStatusList.Remove(aso3BombsStatus);
            doneList.Add(aso3Bombs);
            doneStatusList.Add(aso3BombsStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASO3Ropes(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASO3Ropes(time)));
                return;
            }
            aso3RopesStatus.Text = FormatSplitTime(time);
            aso3RopesStatus.Font = new Font(aso3RopesStatus.Font, FontStyle.Bold);
            aso3Ropes.Font = new Font(aso3Ropes.Font, FontStyle.Bold);
            todoList.Remove(aso3Ropes);
            todoStatusList.Remove(aso3RopesStatus);
            doneList.Add(aso3Ropes);
            doneStatusList.Add(aso3RopesStatus);
            drawList();
            drawStatusList();
        }

        public void FinishASOKey(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => FinishASOKey(time)));
                return;
            }
            asoKeyStatus.Text = FormatSplitTime(time);
            asoKeyStatus.Font = new Font(asoKeyStatus.Font, FontStyle.Bold);
            asoKey.Font = new Font(asoKey.Font, FontStyle.Bold);
            todoList.Remove(asoKey);
            todoStatusList.Remove(asoKeyStatus);
            doneList.Add(asoKey);
            doneStatusList.Add(asoKeyStatus);
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
            if (num > journalNum || num == 0)
            {
                journalNum = num;
                JournalStatus.Text = String.Format("{0,5} {1}", num, "/ 114");
            }
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

        public void SetASOStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetASOStatus), new object[] { status });
                return;
            }
            ASOStatus.Text = status;
        }

        public void ASODone(long time)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<long>(ASODone), new object[] { time });
                return;
            }
            ASOStatus.Text = FormatSplitTime(time);
            ASOStatus.Font = new Font(ASOStatus.Font, FontStyle.Bold);
            ASO.Font = new Font(ASO.Font, FontStyle.Bold);

            asoOlmecStatus.Text = FormatSplitTime(time);
            asoOlmecStatus.Font = new Font(asoOlmecStatus.Font, FontStyle.Bold);
            asoOlmec.Font = new Font(asoOlmec.Font, FontStyle.Bold);
            todoList.Remove(asoOlmec);
            todoStatusList.Remove(asoOlmecStatus);
            doneList.Add(asoOlmec);
            doneStatusList.Add(asoOlmecStatus);
            drawList();
            drawStatusList();

            asoFinishTime = time;
        }
    }
}
