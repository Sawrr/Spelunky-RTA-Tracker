using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private bool host;
        private string roomCode;

        public Tracker(MainForm form, ImgForm imgForm)
        {
            ui = form;
            unlockables = imgForm;

            // create run manager
            runManager = new RunManager(this);
        }

        public void SetMultiplayerRoom(string roomCode, bool host)
        {
            this.roomCode = roomCode;
            this.host = host;
        }

        public void Reset()
        {
            roomCode = null;

            lock (_runManagerLock)
            {
                runManager = new RunManager(this);
            }
        }

        public void RunStarted(long time)
        {
            ui.StartTimer(time);
            runManager.StartRun();
        }

        public void RunCompleted(long time)
        {
            ui.FinishAA(time);
        }

        public void NineteenAchieved(long time, int plays)
        {
            ui.FinishNineteen(time, plays);
        }

        public void TutorialDone(long time)
        {
            ui.FinishTutorial(time);
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

        public void SendCharactersUpdate(long time, byte[] chars)
        {
            string body = "{\"characters\": [";
            for (int i = 0; i < 16; i++)
            {
                if (chars[4 * i] == 1)
                {
                    body += "true,";
                } else
                {
                    body += "false,";
                }
            }
            body = body.TrimEnd(',');
            body += "]}";

            Http.sendUpdate(roomCode, time, host, body);
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
            if (!runManager.IsAchievementDone(Achievement.Casanova) && runManager.IsRunInProgress())
            {
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
            ui.SetASOStatus(status);
        }

        public void ASOSplit(int index, long time)
        {
            switch (index)
            {
                case 0:
                    ui.FinishASO1Bomb(time);
                    break;
                case 1:
                    ui.FinishASO1Rope(time);
                    break;
                case 2:
                    ui.FinishASO10k(time);
                    break;
                case 3:
                    ui.FinishASO2Bombs(time);
                    break;
                case 4:
                    ui.FinishASO2Ropes(time);
                    break;
                case 5:
                    ui.FinishASOShotgun(time);
                    break;
                case 6:
                    ui.FinishASO3Bombs(time);
                    break;
                case 7:
                    ui.FinishASO3Ropes(time);
                    break;
                case 8:
                    ui.FinishASOKey(time);
                    break;
            }
        }

        public void ASODone(long time)
        {
            ui.ASODone(time);
        }

        public async void MultiplayerMain()
        {
            // Check for multiplayer updates
            while (true)
            {
                long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long wakeUpTime = time + 5000;

                if (roomCode != null)
                {
                    string data = await Http.getUpdates(roomCode);

                    dynamic updates = JsonConvert.DeserializeObject(data);

                    // Check for run start
                    long startTime = updates.startTime;
                    if (startTime > 0)
                    {
                        RunStarted(startTime);
                    }

                    // Check for end time
                    long endTime = updates.endTime;
                    if (endTime > 0)
                    {
                        RunCompleted(endTime);
                    }

                    // Check for achievements
                    dynamic achievements = updates.data;

                    // Addicted
                    int plays = achievements.plays.host + achievements.plays.guest;
                    long addictedTime = achievements.addictedTime;
                    PlaysEvent(plays, addictedTime); // TODO update plays ?

                    // Speedlunky
                    long speedlunkyTime = achievements.speedlunkyTime;
                    if (speedlunkyTime > 0)
                    {
                        SpeedlunkyAchieved(speedlunkyTime, plays);
                    }

                    // Big Money
                    long bigMoneyTime = achievements.bigMoneyTime;
                    if (bigMoneyTime > 0)
                    {
                        BigMoneyAchieved(bigMoneyTime, plays);
                    }

                    // No Gold
                    long noGoldTime = achievements.noGoldTime;
                    if (noGoldTime > 0)
                    {
                        NoGoldAchieved(noGoldTime, plays);
                    }

                    // Teamwork Time
                    long teamworkTime = achievements.teamworkTime;
                    if (teamworkTime > 0)
                    {
                        TeamworkAchieved(teamworkTime, plays);
                    }

                    // Casanova Time
                    long casanovaTime = achievements.casanovaTime;
                    if (casanovaTime > 0)
                    {
                        DamselEvent(10, casanovaTime, plays);
                    }

                    // Public Enemy Time
                    long publicEnemyTime = achievements.publicEnemyTime;
                    if (publicEnemyTime > 0)
                    {
                        ShoppieEvent(12, publicEnemyTime, plays);
                    }

                    // Characters
                    JArray charArray = achievements.characters;
                    byte[] charBytes = charArray.Select(jv => (byte)jv).ToArray();
                    byte[] chars = new byte[64];
                    int charNum = 0;
                    for (int i = 0; i < 16; i ++)
                    {
                        chars[4 * i] = charBytes[i];
                        if (charBytes[i] > 0) charNum++;
                    }

                    long charactersTime = achievements.charactersTime;
                    CharactersEvent(charNum, charactersTime, plays, chars);

                    // TODO journal
                }

                long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long sleepTime = wakeUpTime - currentTime;
                if (sleepTime > 0)
                {
                    Thread.Sleep((int)sleepTime);
                }
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
                lock (_runManagerLock)
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
