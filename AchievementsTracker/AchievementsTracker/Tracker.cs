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
            runManager.StartRun();
        }

        public void RunCompleted()
        {
            ui.StopTimer();
        }

        public void NineteenAchieved()
        {
            ui.FinishNineteen();
        }

        public void SpeedlunkyAchieved()
        {
            if (!runManager.IsAchievementDone(Achievement.Speedlunky) && runManager.IsRunInProgress())
            {
                ui.FinishSpeedlunky();
                runManager.FinishAchievement(Achievement.Speedlunky);
            }
        }

        public void BigMoneyAchieved()
        {
            if (!runManager.IsAchievementDone(Achievement.BigMoney) && runManager.IsRunInProgress())
            {
                ui.FinishBigMoney();
                runManager.FinishAchievement(Achievement.BigMoney);
            }
        }

        public void NoGoldAchieved()
        {
            if (!runManager.IsAchievementDone(Achievement.NoGold) && runManager.IsRunInProgress())
            {
                ui.FinishNoGold();
                runManager.FinishAchievement(Achievement.NoGold);
            }
        }

        public void TeamworkAchieved()
        {
            if (!runManager.IsAchievementDone(Achievement.Teamwork) && runManager.IsRunInProgress())
            {
                ui.FinishTeamwork();
                runManager.FinishAchievement(Achievement.Teamwork);
            }
        }

        public void JournalEvent(int num)
        {
            if (!runManager.IsAchievementDone(Achievement.Journal) && runManager.IsRunInProgress())
            {
                ui.SetJournalStatus(num);
            }
            if (num == 114)
            {
                ui.FinishJournal();
                runManager.FinishAchievement(Achievement.Journal);
            }
        }

        public void CharactersEvent(int num)
        {
            if (!runManager.IsAchievementDone(Achievement.Characters) && runManager.IsRunInProgress())
            {
                ui.SetCharactersStatus(num);
            }
            if (num == 16)
            {
                ui.FinishCharacters();
                runManager.FinishAchievement(Achievement.Characters);
            }
        }

        public void DamselEvent(int num)
        {
            if (!runManager.IsAchievementDone(Achievement.Casanova) && runManager.IsRunInProgress()) {
                ui.SetDamselCount(num);
            }
            if (num >= 10)
            {
                ui.FinishCasanova();
                runManager.FinishAchievement(Achievement.Casanova);
            }
        }

        public void ShoppieEvent(int num)
        {
            if (!runManager.IsAchievementDone(Achievement.PublicEnemy) && runManager.IsRunInProgress())
            {
                ui.SetShoppieCount(num);
            }
            if (num >= 12)
            {
                ui.FinishPublicEnemy();
                runManager.FinishAchievement(Achievement.PublicEnemy);
            }
        }

        public void PlaysEvent(int num)
        {
            if (!runManager.IsAchievementDone(Achievement.Addicted) && runManager.IsRunInProgress())
            {
                ui.SetPlaysCount(num);
            }
            if (num >= 1000)
            {
                ui.FinishAddicted();
                runManager.FinishAchievement(Achievement.Addicted);
            }
        }

        public void Main()
        {
            ui.SetSpelunkyRunning(false);
            running = false;

            // Listen for Spelunky Process
            int processHandle = 0;
            int baseAddress = 0;
            try
            {
                spelunky = SpelunkyProcessListener.listenForSpelunkyProcess();
                Log.WriteLine("Spelunky process detected");
                ui.SetSpelunkyRunning(true);
                processHandle = (int)OpenProcess(PROCESS_WM_READ, false, spelunky.Id);
                baseAddress = spelunky.MainModule.BaseAddress.ToInt32();
            } catch (Exception e)
            {
                Log.WriteLine("Error encountered listening for or opening the process");
                Log.WriteLine(e.ToString());
                Main();
            }

            // Listen for process terminating
            spelunky.EnableRaisingEvents = true;
            spelunky.Exited += new EventHandler((s, e) =>
            {
                Log.WriteLine("Spelunky process exited");

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
                if (sleepTime > 0)
                {
                    Thread.Sleep((int)sleepTime);
                } else
                {
                    Log.WriteLine("This tick took longer than 16 ms");
                }
            }
        }
    }
}
