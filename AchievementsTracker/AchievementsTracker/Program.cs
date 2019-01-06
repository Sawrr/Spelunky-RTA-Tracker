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
            string logFolder = "AchievementsTrackerLogs";
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
                settings = new SettingsForm(this, form);

                // Create tracker thread
                tracker = new Tracker(form, imgForm);
                Thread trackerThread = new Thread(() => tracker.Main());
                trackerThread.IsBackground = true;
                trackerThread.Start();

                // Create tray icon
                MenuItem CategoryMenu = new MenuItem("Category");
                CategoryMenu.MenuItems.Add(new MenuItem("All Achievements", SelectAA));
                CategoryMenu.MenuItems.Add(new MenuItem("All Journal Entries", SelectAJE));
                CategoryMenu.MenuItems.Add(new MenuItem("All Characters", SelectAC));
                CategoryMenu.MenuItems.Add(new MenuItem("All Shortcuts + Olmec", SelectASO));
                CategoryMenu.MenuItems.Add(new MenuItem("Tutorial%", SelectTutorial));
                trayIcon = new NotifyIcon()
                {
                    Icon = Resources.icon,
                    ContextMenu = new ContextMenu(new MenuItem[] {
                        new MenuItem("Reset", Reset),
                        CategoryMenu,
                        new MenuItem("Settings", OpenSettings),
                        new MenuItem("Exit", Exit)
                    }),
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

                // Set background color
                Color bgColor = ColorTranslator.FromHtml(trackerSettings.backColor);
                SetBackgroundColor(bgColor);
                settings.SetBackgroundColor(bgColor);

                // Set text color
                Color textColor = ColorTranslator.FromHtml(trackerSettings.textColor);
                SetTextColor(textColor);
                settings.SetTextColor(textColor);

                // Get started!
                Reset(null, null);
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

                form.drawList();
                form.drawStatusList();

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

                form.drawList();
                form.drawStatusList();

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

                form.drawList();
                form.drawStatusList();

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

                form.drawList();
                form.drawStatusList();

                imgForm.Show();

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

                form.drawList();
                form.drawStatusList();

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

            public void Reset(object sender, EventArgs e)
            {
                form.Reset();
                imgForm.Reset();
                tracker.Reset();
            }

            public void SaveSettings(Color backColor, Color formColor, Keys resetHotkey, int resetHotkeyMods)
            {
                trackerSettings.backColor = ColorTranslator.ToHtml(backColor);
                trackerSettings.textColor = ColorTranslator.ToHtml(formColor);
                trackerSettings.resetHotkey = (int)resetHotkey;
                trackerSettings.resetHotkeyMods = resetHotkeyMods;
                trackerSettings.Save();
            }
        }

        public class TrackerSettings : AppSettings<TrackerSettings>
        {
            public String backColor = "#0F0F0F";
            public String textColor = "#E2E2E2";
            public int resetHotkey = 0;
            public int resetHotkeyMods = 0;
        }
    }

    public class AppSettings<T> where T : new()
    {
        private const string FILENAME = "AchievementsTrackerSettings.txt";

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
