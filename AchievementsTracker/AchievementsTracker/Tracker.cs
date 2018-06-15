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

        // Lock for resetting run manager
        private readonly object _runManagerLock = new Object();

        private MainForm ui;
        private ImgForm unlockables;
        private Process spelunky;
        private bool running;
        private RunManager runManager;

        public Tracker(MainForm form, ImgForm imgForm)
        {
            ui = form;
            unlockables = imgForm;

            // create run manager
            runManager = new RunManager(this);
        }

        public void Reset()
        {
            lock(_runManagerLock)
            {
                runManager = new RunManager(this);
            }
        }

        public void RunStarted()
        {
            ui.StartTimer();
            runManager.StartRun();
        }

        public void RunCompleted(long time)
        {
            ui.StopTimer(time);
        }

        public void NineteenAchieved(long time, int plays)
        {
                ui.FinishNineteen(time, plays);
        }

        public void SpeedlunkyAchieved(long time, int plays)
        {
            if (!runManager.IsAchievementDone(Achievement.Speedlunky) && runManager.IsRunInProgress())
            {
                ui.FinishSpeedlunky(time);
                runManager.FinishAchievement(Achievement.Speedlunky, time, plays);
            }
        }

        public void BigMoneyAchieved(long time, int plays)
        {
            if (!runManager.IsAchievementDone(Achievement.BigMoney) && runManager.IsRunInProgress())
            {
                ui.FinishBigMoney(time);
                runManager.FinishAchievement(Achievement.BigMoney, time, plays);
            }
        }

        public void NoGoldAchieved(long time, int plays)
        {
            if (!runManager.IsAchievementDone(Achievement.NoGold) && runManager.IsRunInProgress())
            {
                ui.FinishNoGold(time);
                runManager.FinishAchievement(Achievement.NoGold, time, plays);
            }
        }

        public void TeamworkAchieved(long time, int plays)
        {
            if (!runManager.IsAchievementDone(Achievement.Teamwork) && runManager.IsRunInProgress())
            {
                ui.FinishTeamwork(time);
                runManager.FinishAchievement(Achievement.Teamwork, time, plays);
            }
        }

        public void JournalEvent(int num, long time, int plays, byte[] mons, byte[] items, byte[] traps)
        {
            if (!runManager.IsAchievementDone(Achievement.Journal) && runManager.IsRunInProgress())
            {
                ui.SetJournalStatus(num);
                unlockables.UpdateMonsters(mons);
                unlockables.UpdateItems(items);
                unlockables.UpdateTraps(traps);
                if (num == 114)
                {
                    ui.FinishJournal(time);
                    runManager.FinishAchievement(Achievement.Journal, time, plays);
                }
            }
        }

        public void CharactersEvent(int num, long time, int plays, byte[] chars)
        {
            if (!runManager.IsAchievementDone(Achievement.Characters) && runManager.IsRunInProgress())
            {
                ui.SetCharactersStatus(num);
                unlockables.UpdateCharacters(chars);
                if (num == 16)
                {
                    ui.FinishCharacters(time);
                    runManager.FinishAchievement(Achievement.Characters, time, plays);
                }
            }
        }

        public void DamselEvent(int num, long time, int plays)
        {
            if (!runManager.IsAchievementDone(Achievement.Casanova) && runManager.IsRunInProgress()) {
                ui.SetDamselCount(num);
                if (num >= 10)
                {
                    ui.FinishCasanova(time);
                    runManager.FinishAchievement(Achievement.Casanova, time, plays);
                }
            }
        }

        public void ShoppieEvent(int num, long time, int plays)
        {
            if (!runManager.IsAchievementDone(Achievement.PublicEnemy) && runManager.IsRunInProgress())
            {
                ui.SetShoppieCount(num);
                if (num >= 12)
                {
                    ui.FinishPublicEnemy(time);
                    runManager.FinishAchievement(Achievement.PublicEnemy, time, plays);
                }
            }
        }

        public void PlaysEvent(int num, long time)
        {
            if (!runManager.IsAchievementDone(Achievement.Addicted) && runManager.IsRunInProgress())
            {
                ui.SetPlaysCount(num);
                if (num >= 1000)
                {
                    ui.FinishAddicted(time);
                    runManager.FinishAchievement(Achievement.Addicted, time, num);
                }
            }
        }

        public void TunnelManEvent(string status)
        {
            ui.SetTunnelManStatus(status);
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
            }
            catch (Exception e)
            {
                Log.WriteLine("Error encountered listening for or opening the process");
                Log.WriteLine(e.ToString());
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

                // Acquire lock to prevent reset during updates
                lock(_runManagerLock)
                {
                    gameManager.update();
                }

                long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long sleepTime = wakeUpTime - currentTime;
                if (sleepTime > 0)
                {
                    Thread.Sleep((int)sleepTime);
                }
                else
                {
                    Log.WriteLine("This tick took longer than 16 ms");
                }
            }
        }
    }
}
