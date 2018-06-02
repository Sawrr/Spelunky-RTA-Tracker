using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace AchievementsTracker
{
    static class Program
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

            // Create forms
            MainForm form = new MainForm();
            ImgForm imageForm = new ImgForm();

            // Create tracker thread
            Thread trackerThread = new Thread(() => new Tracker(form).Main());
            trackerThread.IsBackground = true;
            trackerThread.Start();

            // Create image thread
            Thread imageThread = new Thread(() => Application.Run(imageForm));
            imageThread.Start();

            Application.Run(form);
        }
    }
}
