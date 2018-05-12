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
            // Logging
            string logFolder = "AchievementsTrackerLogs";
            Directory.CreateDirectory(logFolder);
            string logName = logFolder + "\\log_" + (DateTime.Now.ToString("MMddyyyy_HHmmss")) + ".txt";
            StreamWriter logFile = File.CreateText(logName);
            logFile.AutoFlush = true;
            Console.SetOut(logFile);
            Console.SetError(logFile);
            
            // Initial setup
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create form
            MainForm form = new MainForm();
            
            // Create tracker thread
            Thread trackerThread = new Thread(() => new Tracker(form).Main());
            trackerThread.IsBackground = true;
            trackerThread.Start();

            Application.Run(form);
        }
    }
}
