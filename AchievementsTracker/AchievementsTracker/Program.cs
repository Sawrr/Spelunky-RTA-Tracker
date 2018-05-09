using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AchievementsTracker
{
    static class Program
    {
        private static readonly Object dataLock = new Object();
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initial setup
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create form
            MainForm form = new MainForm();
            
            // Create tracker thread
            Thread trackerThread = new Thread(() => new Tracker(form).Main());
            trackerThread.Start();

            Application.Run(form);
        }
    }
}
