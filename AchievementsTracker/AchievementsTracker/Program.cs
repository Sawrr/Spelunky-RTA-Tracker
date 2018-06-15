using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using AchievementsTracker.Properties;
using System.Drawing;

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
            
            // Initial setup
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TrayApplicationContext());
        }

        public class TrayApplicationContext : ApplicationContext
        {
            private NotifyIcon trayIcon;

            private MainForm form;
            private ImgForm imgForm;
            private SettingsForm settings;
            private Tracker tracker;

            public TrayApplicationContext()
            {                
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
                trayIcon = new NotifyIcon()
                {
                    Icon = Resources.icon,
                    ContextMenu = new ContextMenu(new MenuItem[] {
                        new MenuItem("Reset", Reset),
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

                // Get started!
                Reset(null, null);
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
        }
    }
}
