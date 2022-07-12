using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using AchievementsTracker.Properties;
using System.Drawing;
using System.Web.Script.Serialization;

namespace AchievementsTracker
{
    static public class Program
    {     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Global exception handler
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Log.WriteLine(e.ExceptionObject.ToString());
            };

            // Logging
            string logFolder = "SpelunkyRTATrackerLogs";
            Directory.CreateDirectory(logFolder);
            string logName = logFolder + "\\log_" + (DateTime.Now.ToString("MMddyyyy_HHmmss")) + ".txt";
            StreamWriter logFile = File.CreateText(logName);
            logFile.AutoFlush = true;
            Console.SetOut(logFile);

            // Settings file
            TrackerSettings settings = TrackerSettings.Load();
            settings.Save();

            // Initial setup
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TrayApplicationContext(settings));
        }

        public class TrayApplicationContext : ApplicationContext
        {
            private NotifyIcon trayIcon;

            private MainForm form;
            private ImgForm imgForm;
            private SettingsForm settings;
            private Tracker tracker;

            TrackerSettings trackerSettings;

            public TrayApplicationContext(TrackerSettings trackerSettings)
            {
                this.trackerSettings = trackerSettings;
                
                // Create forms
                form = new MainForm(this);
                imgForm = new ImgForm();
                settings = new SettingsForm(this, form, imgForm);

                // Create tracker thread
                tracker = new Tracker(form, imgForm);
                Thread trackerThread = new Thread(() => tracker.Main());
                trackerThread.IsBackground = true;
                trackerThread.Start();

                // Create tracker multiplayer thread
                Thread trackerMultiplayerThread = new Thread(() => tracker.MultiplayerMain());
                trackerMultiplayerThread.IsBackground = true;
                trackerMultiplayerThread.Start();

                // Create Category menu
                MenuItem CategoryMenu = new MenuItem("Category");
                CategoryMenu.MenuItems.Add(new MenuItem("All Achievements", SelectAA));
                CategoryMenu.MenuItems.Add(new MenuItem("All Journal Entries", SelectAJE));
                CategoryMenu.MenuItems.Add(new MenuItem("All Characters", SelectAC));
                CategoryMenu.MenuItems.Add(new MenuItem("All Shortcuts + Olmec", SelectASO));
                CategoryMenu.MenuItems.Add(new MenuItem("Tutorial%", SelectTutorial));

                // Create Coop menu
                MenuItem CoopMenu = new MenuItem("Co-op");
                CoopMenu.MenuItems.Add(new MenuItem("Create room", CreateRoom));
                CoopMenu.MenuItems.Add(new MenuItem("Join room", JoinRoom));

                // Create context menu
                ContextMenu contextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Reset", Reset),
                    CategoryMenu,
                    CoopMenu,
                    new MenuItem("Settings", OpenSettings),
                    new MenuItem("Exit", Exit)
                });

                form.ContextMenu = contextMenu;
                imgForm.ContextMenu = contextMenu;

                // Create tray icon
                trayIcon = new NotifyIcon()
                {
                    Icon = Resources.icon,
                    ContextMenu = contextMenu,
                    Visible = true
                };

                // Set main form to terminate application on close
                form.FormClosing += (s, e) =>
                {
                    Exit(s, e);
                };

                // Display both forms
                form.Show();
                imgForm.Show();

                // Set hotkey
                Keys key = (Keys)trackerSettings.resetHotkey;
                int mods = trackerSettings.resetHotkeyMods;
                form.SetResetHotKey(mods, key);
                settings.SetHotkey(mods, key);

                // Set image size, rows, inverted, groupByArea
                settings.SetImageSize(trackerSettings.imageSize);
                settings.SetRows(trackerSettings.rows);
                settings.SetInverted(trackerSettings.inverted);
                settings.SetGroupByArea(trackerSettings.groupByArea);

                // Set background color
                Color bgColor = ColorTranslator.FromHtml(trackerSettings.backColor);
                SetBackgroundColor(bgColor);
                settings.SetBackgroundColor(bgColor);

                // Set text color
                Color textColor = ColorTranslator.FromHtml(trackerSettings.textColor);
                SetTextColor(textColor);
                settings.SetTextColor(textColor);

                // Set save files
                settings.SetFreshSave(trackerSettings.freshSave);
                settings.SetGameSave(trackerSettings.gameSave);

                // Initialize HTTP client
                Http.setURL(trackerSettings.baseURL);

                // Get started!
                ResetFormsAndTracker();
            }

            bool SynchronizeTime()
            {
                try
                {
                    Http.GetAndSetTimeOffset();
                } catch (Exception e)
                {
                    Log.WriteLine(e.ToString());
                    MessageBox.Show("Failed to synchronize system clock. Please check your internet connection and make sure your system clock is in sync.", "Error");
                    return false;
                }
                // Success
                return true;
            }

            async void CreateRoom(object sender, EventArgs e)
            {
                if (!SynchronizeTime()) return;

                Reset(sender, e);
                string code = null;
                try
                {
                    code = await Http.createRoom();
                } catch (Exception ex)
                {
                    MessageBox.Show("Failed to create a room.", "Error");
                    Log.WriteLine(ex.ToString());
                    return;
                }
                tracker.SetMultiplayerRoom(code, true);
                form.setRoomCode(code);
                form.SetRoomStatusReady(false);
            }

            async void JoinRoom(object sender, EventArgs e)
            {
                if (!SynchronizeTime()) return;

                string code = Microsoft.VisualBasic.Interaction.InputBox("Enter room code", "Room code", "", -1, -1);
                if (code.Length == 4 && code.All(char.IsLetterOrDigit))
                {
                    // try to join room
                    try
                    {
                        System.Net.Http.HttpResponseMessage res = await Http.joinRoom(code);
                        if (!res.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Room not found", "Error");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to join the room.", "Error");
                        Log.WriteLine(ex.ToString());
                        return;
                    }
                    // valid code chosen
                    tracker.SetMultiplayerRoom(code, false);
                    form.setRoomCode(code);
                    form.SetRoomStatusReady(true);
                } else
                {
                    MessageBox.Show("Invalid room code", "Error");
                }
            }

            void SelectAA(object sender, EventArgs e)
            {
                form.ASO.Show();
                form.ASOStatus.Show();
                form.Tutorial.Show();
                form.TutorialStatus.Show();
                form.Journal.Show();
                form.JournalStatus.Show();
                form.Characters.Show();
                form.CharactersStatus.Show();
                form.Speedlunky.Show();
                form.SpeedlunkyStatus.Show();
                form.BigMoney.Show();
                form.BigMoneyStatus.Show();
                form.NoGold.Show();
                form.NoGoldStatus.Show();
                form.Casanova.Show();
                form.CasanovaStatus.Show();
                form.PublicEnemy.Show();
                form.PublicEnemyStatus.Show();
                form.Teamwork.Show();
                form.TeamworkStatus.Show();
                form.Addicted.Show();
                form.AddictedStatus.Show();
                form.ExtrapolatedTime.Show();
                form.ExtrapolatedTimeStatus.Show();

                form.aso1Bomb.Hide();
                form.aso1BombStatus.Hide();
                form.aso1Rope.Hide();
                form.aso1RopeStatus.Hide();
                form.aso10k.Hide();
                form.aso10kStatus.Hide();
                form.aso2Bombs.Hide();
                form.aso2BombsStatus.Hide();
                form.aso2Ropes.Hide();
                form.aso2RopesStatus.Hide();
                form.asoShotgun.Hide();
                form.asoShotgunStatus.Hide();
                form.aso3Bombs.Hide();
                form.aso3BombsStatus.Hide();
                form.aso3Ropes.Hide();
                form.aso3RopesStatus.Hide();
                form.asoKey.Hide();
                form.asoKeyStatus.Hide();
                form.asoOlmec.Hide();
                form.asoOlmecStatus.Hide();

                form.drawList();
                form.drawStatusList();

                imgForm.changeCategory(Category.AA);
                imgForm.ArrangeUnlockables();
                imgForm.Show();

                form.changeCategory(Category.AA);
            }

            void SelectAJE(object sender, EventArgs e)
            {
                form.ASO.Show();
                form.ASOStatus.Show();
                form.Tutorial.Show();
                form.TutorialStatus.Show();
                form.Journal.Show();
                form.JournalStatus.Show();
                form.Characters.Hide();
                form.CharactersStatus.Hide();
                form.Speedlunky.Hide();
                form.SpeedlunkyStatus.Hide();
                form.BigMoney.Hide();
                form.BigMoneyStatus.Hide();
                form.NoGold.Hide();
                form.NoGoldStatus.Hide();
                form.Casanova.Hide();
                form.CasanovaStatus.Hide();
                form.PublicEnemy.Hide();
                form.PublicEnemyStatus.Hide();
                form.Teamwork.Hide();
                form.TeamworkStatus.Hide();
                form.Addicted.Hide();
                form.AddictedStatus.Hide();
                form.ExtrapolatedTime.Hide();
                form.ExtrapolatedTimeStatus.Hide();

                form.aso1Bomb.Hide();
                form.aso1BombStatus.Hide();
                form.aso1Rope.Hide();
                form.aso1RopeStatus.Hide();
                form.aso10k.Hide();
                form.aso10kStatus.Hide();
                form.aso2Bombs.Hide();
                form.aso2BombsStatus.Hide();
                form.aso2Ropes.Hide();
                form.aso2RopesStatus.Hide();
                form.asoShotgun.Hide();
                form.asoShotgunStatus.Hide();
                form.aso3Bombs.Hide();
                form.aso3BombsStatus.Hide();
                form.aso3Ropes.Hide();
                form.aso3RopesStatus.Hide();
                form.asoKey.Hide();
                form.asoKeyStatus.Hide();
                form.asoOlmec.Hide();
                form.asoOlmecStatus.Hide();

                form.drawList();
                form.drawStatusList();

                imgForm.changeCategory(Category.AJE);
                imgForm.ArrangeUnlockables();
                imgForm.Show();

                form.changeCategory(Category.AJE);
            }

            void SelectAC(object sender, EventArgs e)
            {
                form.ASO.Show();
                form.ASOStatus.Show();
                form.Tutorial.Show();
                form.TutorialStatus.Show();
                form.Journal.Hide();
                form.JournalStatus.Hide();
                form.Characters.Show();
                form.CharactersStatus.Show();
                form.Speedlunky.Hide();
                form.SpeedlunkyStatus.Hide();
                form.BigMoney.Hide();
                form.BigMoneyStatus.Hide();
                form.NoGold.Hide();
                form.NoGoldStatus.Hide();
                form.Casanova.Hide();
                form.CasanovaStatus.Hide();
                form.PublicEnemy.Hide();
                form.PublicEnemyStatus.Hide();
                form.Teamwork.Hide();
                form.TeamworkStatus.Hide();
                form.Addicted.Hide();
                form.AddictedStatus.Hide();
                form.ExtrapolatedTime.Hide();
                form.ExtrapolatedTimeStatus.Hide();

                form.aso1Bomb.Hide();
                form.aso1BombStatus.Hide();
                form.aso1Rope.Hide();
                form.aso1RopeStatus.Hide();
                form.aso10k.Hide();
                form.aso10kStatus.Hide();
                form.aso2Bombs.Hide();
                form.aso2BombsStatus.Hide();
                form.aso2Ropes.Hide();
                form.aso2RopesStatus.Hide();
                form.asoShotgun.Hide();
                form.asoShotgunStatus.Hide();
                form.aso3Bombs.Hide();
                form.aso3BombsStatus.Hide();
                form.aso3Ropes.Hide();
                form.aso3RopesStatus.Hide();
                form.asoKey.Hide();
                form.asoKeyStatus.Hide();
                form.asoOlmec.Hide();
                form.asoOlmecStatus.Hide();

                form.drawList();
                form.drawStatusList();

                imgForm.changeCategory(Category.AC);
                imgForm.ArrangeUnlockables();
                imgForm.Show();

                form.changeCategory(Category.AC);
            }

            void SelectASO(object sender, EventArgs e)
            {
                form.ASO.Show();
                form.ASOStatus.Show();
                form.Tutorial.Show();
                form.TutorialStatus.Show();
                form.Journal.Hide();
                form.JournalStatus.Hide();
                form.Characters.Hide();
                form.CharactersStatus.Hide();
                form.Speedlunky.Hide();
                form.SpeedlunkyStatus.Hide();
                form.BigMoney.Hide();
                form.BigMoneyStatus.Hide();
                form.NoGold.Hide();
                form.NoGoldStatus.Hide();
                form.Casanova.Hide();
                form.CasanovaStatus.Hide();
                form.PublicEnemy.Hide();
                form.PublicEnemyStatus.Hide();
                form.Teamwork.Hide();
                form.TeamworkStatus.Hide();
                form.Addicted.Hide();
                form.AddictedStatus.Hide();
                form.ExtrapolatedTime.Hide();
                form.ExtrapolatedTimeStatus.Hide();

                form.aso1Bomb.Show();
                form.aso1BombStatus.Show();
                form.aso1Rope.Show();
                form.aso1RopeStatus.Show();
                form.aso10k.Show();
                form.aso10kStatus.Show();
                form.aso2Bombs.Show();
                form.aso2BombsStatus.Show();
                form.aso2Ropes.Show();
                form.aso2RopesStatus.Show();
                form.asoShotgun.Show();
                form.asoShotgunStatus.Show();
                form.aso3Bombs.Show();
                form.aso3BombsStatus.Show();
                form.aso3Ropes.Show();
                form.aso3RopesStatus.Show();
                form.asoKey.Show();
                form.asoKeyStatus.Show();
                form.asoOlmec.Show();
                form.asoOlmecStatus.Show();

                form.drawList();
                form.drawStatusList();

                imgForm.changeCategory(Category.ASO);
                imgForm.Hide();

                form.changeCategory(Category.ASO);
            }

            void SelectTutorial(object sender, EventArgs e)
            {
                form.ASO.Hide();
                form.ASOStatus.Hide();
                form.Tutorial.Show();
                form.TutorialStatus.Show();
                form.Journal.Hide();
                form.JournalStatus.Hide();
                form.Characters.Hide();
                form.CharactersStatus.Hide();
                form.Speedlunky.Hide();
                form.SpeedlunkyStatus.Hide();
                form.BigMoney.Hide();
                form.BigMoneyStatus.Hide();
                form.NoGold.Hide();
                form.NoGoldStatus.Hide();
                form.Casanova.Hide();
                form.CasanovaStatus.Hide();
                form.PublicEnemy.Hide();
                form.PublicEnemyStatus.Hide();
                form.Teamwork.Hide();
                form.TeamworkStatus.Hide();
                form.Addicted.Hide();
                form.AddictedStatus.Hide();
                form.ExtrapolatedTime.Hide();
                form.ExtrapolatedTimeStatus.Hide();

                form.aso1Bomb.Hide();
                form.aso1BombStatus.Hide();
                form.aso1Rope.Hide();
                form.aso1RopeStatus.Hide();
                form.aso10k.Hide();
                form.aso10kStatus.Hide();
                form.aso2Bombs.Hide();
                form.aso2BombsStatus.Hide();
                form.aso2Ropes.Hide();
                form.aso2RopesStatus.Hide();
                form.asoShotgun.Hide();
                form.asoShotgunStatus.Hide();
                form.aso3Bombs.Hide();
                form.aso3BombsStatus.Hide();
                form.aso3Ropes.Hide();
                form.aso3RopesStatus.Hide();
                form.asoKey.Hide();
                form.asoKeyStatus.Hide();
                form.asoOlmec.Hide();
                form.asoOlmecStatus.Hide();

                form.drawList();
                form.drawStatusList();

                imgForm.changeCategory(Category.Tutorial);
                imgForm.Hide();

                form.changeCategory(Category.Tutorial);
            }

            void Exit(object sender, EventArgs e)
            {
                trayIcon.Visible = false;

                Application.Exit();
            }

            void OpenSettings(object sender, EventArgs e)
            {
                settings.Show();
            }

            public void SetBackgroundColor(Color color)
            {
                form.BackColor = color;
                imgForm.BackColor = color;
                settings.BackColor = color;
            }

            public void SetTextColor(Color color)
            {
                form.ForeColor = color;
                imgForm.ForeColor = color;
                settings.ForeColor = color;
            }

            private void ResetFormsAndTracker()
            {
                form.Reset();
                imgForm.Reset();
                tracker.Reset();
            }

            public void Reset(object sender, EventArgs e)
            {
                ResetFormsAndTracker();

                settings.ResetSaveFile();
            }

            public void SaveSettings(Color backColor, Color formColor, Keys resetHotkey, int resetHotkeyMods, String freshSave, String gameSave, int imageSize, int rows, bool inverted, bool groupByArea)
            {
                trackerSettings.backColor = ColorTranslator.ToHtml(backColor);
                trackerSettings.textColor = ColorTranslator.ToHtml(formColor);
                trackerSettings.resetHotkey = (int)resetHotkey;
                trackerSettings.resetHotkeyMods = resetHotkeyMods;
                trackerSettings.freshSave = freshSave;
                trackerSettings.gameSave = gameSave;
                trackerSettings.imageSize = imageSize;
                trackerSettings.rows = rows;
                trackerSettings.inverted = inverted;
                trackerSettings.groupByArea = groupByArea;
                trackerSettings.Save();
            }
        }

        public class TrackerSettings : AppSettings<TrackerSettings>
        {
            public String backColor = "#0F0F0F";
            public String textColor = "#E2E2E2";
            public int resetHotkey = 0;
            public int resetHotkeyMods = 0;
            public String freshSave;
            public String gameSave;
            public String baseURL = "http://spelunky.sawr.org";
            public int imageSize = 40;
            public int rows = 8;
            public bool inverted = false;
            public bool groupByArea = false;
        }
    }

    public class AppSettings<T> where T : new()
    {
        private const string FILENAME = "SpelunkyRTATrackerSettings.txt";

        public void Save()
        {
            File.WriteAllText(FILENAME, (new JavaScriptSerializer()).Serialize(this));
        }

        public static T Load()
        {
            T t = new T();
            if (File.Exists(FILENAME))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(FILENAME));
            return t;
        }
    }
}
