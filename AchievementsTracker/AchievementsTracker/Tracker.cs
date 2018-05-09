using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AchievementsTracker
{
    class Tracker
    {
        private const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        private MainForm form;

        private Process spelunky;
        private bool running;

        public Tracker(MainForm form)
        {
            this.form = form;
        }

        public void Main()
        {
            Console.WriteLine("Started!");
            form.SetSpelunkyRunning(false);
            running = false;

            // Listen for Spelunky Process
            spelunky = SpelunkyProcessListener.listenForSpelunkyProcess();
            Console.WriteLine(" Spelunky detected");
            form.SetSpelunkyRunning(true);
            int processHandle = (int)OpenProcess(PROCESS_WM_READ, false, spelunky.Id);
            int baseAddress = spelunky.MainModule.BaseAddress.ToInt32();

            // Listen for process terminating
            spelunky.EnableRaisingEvents = true;
            spelunky.Exited += new EventHandler((s, e) =>
            {
                Console.WriteLine("Spelunky process exited.");
                form.SetSpelunkyRunning(false);

                // Now start over
                Main();
            });

            // main game loop
            //gameLoop();
        }
    }
}
