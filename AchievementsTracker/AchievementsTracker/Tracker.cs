using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AchievementsTracker
{
    class Tracker
    {
        private const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        private MainForm ui;
        private Process spelunky;
        private bool running;
        private RunManager runManager;

        public Tracker(MainForm form)
        {
            ui = form;

            // create run manager
            runManager = new RunManager(this);
        }

        public void RunStarted()
        {
            ui.StartTimer();
        }

        public void RunCompleted()
        {
            ui.StopTimer();
        }

        public void DamselEvent(int num)
        {
            Console.WriteLine("damsels: " + num);
            ui.SetDamselCount(num);
            if (num == 3)
            {
                //ui.FinishAchievement(Achievements.Damsels);
                runManager.FinishAchievement(Achievement.Damsels);
            }
        }

        public void ShoppieEvent(int num)
        {
            ui.SetShoppieCount(num);
            if (num == 3)
            {
                //ui.FinishAchievement(Achievements.Shoppies);
                runManager.FinishAchievement(Achievement.Shoppies);
            }
        }

        public void Main()
        {
            ui.SetSpelunkyRunning(false);
            running = false;

            // Listen for Spelunky Process
            spelunky = SpelunkyProcessListener.listenForSpelunkyProcess();
            Console.WriteLine(" Spelunky detected");
            ui.SetSpelunkyRunning(true);
            int processHandle = (int)OpenProcess(PROCESS_WM_READ, false, spelunky.Id);
            int baseAddress = spelunky.MainModule.BaseAddress.ToInt32();

            // Listen for process terminating
            spelunky.EnableRaisingEvents = true;
            spelunky.Exited += new EventHandler((s, e) =>
            {
                Console.WriteLine("Spelunky process exited.");

                // Now start over
                Main();
            });

            // Create game manager
            GameManager gameManager = new GameManager(this, new MemoryReader(processHandle, baseAddress));

            // main game loop
            running = true;
            long time;
            while (running)
            {
                time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long wakeUpTime = time + 16;

                gameManager.update();

                long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long sleepTime = wakeUpTime - currentTime;
                Thread.Sleep((int)sleepTime);
            }
        }
    }
}
