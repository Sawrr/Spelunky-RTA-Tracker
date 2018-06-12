using System;

namespace AchievementsTracker
{
    class GameManager
    {
        private Tracker tracker;
        private MemoryReader memoryReader;

        private ScreenState state;
        private int charSelect;
        private int damselCount;
        private int shoppieCount;
        private int characters;
        private int journal;
        private int plays;

        private bool runInProgress;
        private bool runIsNoGold;
        private int score;
        private int bombs;
        private bool runIsValid;
        private int levelIdx;
        private int runTime;
        private int stageTime;

        private bool runIsTwoPlayer;
        private int p1Health;
        private int p2Health;
        private int p1HealthAddr;
        private int p2HealthAddr;

        private int tunnelManRemaining;

        public GameManager(Tracker tracker, MemoryReader memoryReader)
        {
            this.tracker = tracker;
            this.memoryReader = memoryReader;
        }

        private void startRun()
        {
            Log.WriteLine("Run started");
            runInProgress = true;
            if (levelIdx == 1)
            {
                Log.WriteLine("  from Level 1");
                runIsValid = true;
                runIsNoGold = true;
            }
            if (bombs < 4)
            {
                Log.WriteLine("  with multiple players");
                runIsTwoPlayer = true;
                p1HealthAddr = memoryReader.ReadPlayerOneHealthAddr();
                p2HealthAddr = memoryReader.ReadPlayerTwoHealthAddr();
            }
        }

        private void resetRun()
        {
            Log.WriteLine("Run status reset");
            runInProgress = false;
            runIsValid = false;
            runIsTwoPlayer = false;
            tracker.DamselEvent(0, 0, 0);
            tracker.ShoppieEvent(0, 0, 0);
        }

        public void update()
        {
            long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // gold check
            int newScore = memoryReader.ReadScore();
            if (newScore != score && state == ScreenState.Running)
            {
                if (runIsNoGold)
                {
                    runIsNoGold = false;
                    Log.WriteLine("No gold lost");
                }
                if (newScore >= 500000)
                {
                    tracker.BigMoneyAchieved(time, plays);
                }
            }
            score = newScore;

            // Bombs
            bombs = memoryReader.ReadBombs();

            // Two player hp's
            if (runIsTwoPlayer)
            {
                p1Health = memoryReader.ReadExactMemory(p1HealthAddr);
                p2Health = memoryReader.ReadExactMemory(p2HealthAddr);
            }

            // Times
            int newRunTime = memoryReader.ReadRunTimeInMilliseconds();
            int newStageTime = memoryReader.ReadStageTimeInMilliseconds();
            if (newRunTime - runTime < 0 && state == ScreenState.Running)
            {
                // Filter out beginning of olmec, yama cutscenes
                if (!((levelIdx == 20 || levelIdx == 16) && stageTime < 500))
                {
                    // Filter out death of P1 in two player games
                    if (!(runIsTwoPlayer && p2Health > 0))
                    {
                        // Insta death
                        resetRun();
                    }
                }
            }
            runTime = newRunTime;
            stageTime = newStageTime;

            // Level index
            levelIdx = memoryReader.ReadLevelIndex();

            // Screen state
            ScreenState newState = (ScreenState)memoryReader.ReadScreenState();
            if (newState == ScreenState.Running && state == ScreenState.Loading2 && runInProgress == false)
            {
                // run started
                startRun();
            }
            if ((newState == ScreenState.DeathScreen && state != ScreenState.DeathScreen) ||
                (newState == ScreenState.MainMenu && state != ScreenState.MainMenu))
            {
                // run over
                resetRun();
            }
            if (newState == ScreenState.VictoryWalking && state != ScreenState.VictoryWalking)
            {
                // run victory
                if (levelIdx == 16)
                {
                    score += 50000;
                }
                else if (levelIdx == 20)
                {
                    score += 100000;
                }
                // check for speedlunky
                if (runIsValid && runTime < 8 * 60 * 1000)
                {
                    tracker.SpeedlunkyAchieved(time, plays);
                }
                // check for big money
                if (score >= 500000)
                {
                    tracker.BigMoneyAchieved(time, plays);
                }
                // check for no gold
                if (runIsValid && runIsNoGold)
                {
                    tracker.NoGoldAchieved(time, plays);
                }
                // check for good teamwork
                if (runIsValid && runIsTwoPlayer && p1Health != 0 && p2Health != 0)
                {
                    tracker.TeamworkAchieved(time, plays);
                }

                resetRun();
            }
            state = newState;

            int newCharSelect = memoryReader.ReadCharSelect();
            if (state == ScreenState.ChooseCharacter && charSelect == 0 && newCharSelect != 0)
            {
                // Start timer
                Log.WriteLine("Character selected!");
                tracker.RunStarted();
            }
            charSelect = newCharSelect;

            int newDamsels = memoryReader.ReadDamselCount();
            if (newDamsels != damselCount && newDamsels != 0 && state == ScreenState.Running)
            {
                tracker.DamselEvent(newDamsels, time, plays);
            }
            damselCount = newDamsels;

            int newShoppies = memoryReader.ReadShoppieCount();
            if (newShoppies != shoppieCount && newShoppies != 0 && state == ScreenState.Running)
            {
                tracker.ShoppieEvent(newShoppies, time, plays);
            }
            shoppieCount = newShoppies;

            byte[] chars = memoryReader.ReadCharacters();
            int newChars = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] != 0)
                {
                    newChars++;
                }
            }
            if (newChars != characters && newChars > 0 && newChars <= 16)
            {
                tracker.CharactersEvent(newChars, time, plays, chars);
            }
            characters = newChars;

            byte[] journalPlaces = memoryReader.ReadJournalPlaces();
            byte[] journalMonsters = memoryReader.ReadJournalMonsters();
            byte[] journalItems = memoryReader.ReadJournalItems();
            byte[] journalTraps = memoryReader.ReadJournalTraps();
            int newJournal = 0;
            for (int i = 0; i < journalPlaces.Length; i++)
            {
                if (journalPlaces[i] != 0)
                {
                    newJournal++;
                }
            }
            for (int i = 0; i < journalMonsters.Length; i++)
            {
                if (journalMonsters[i] != 0)
                {
                    newJournal++;
                }
            }
            for (int i = 0; i < journalItems.Length; i++)
            {
                if (journalItems[i] != 0)
                {
                    newJournal++;
                }
            }
            for (int i = 0; i < journalTraps.Length; i++)
            {
                if (journalTraps[i] != 0)
                {
                    newJournal++;
                }
            }

            if (newJournal != journal && newJournal > 0 && newJournal <= 144)
            {
                tracker.JournalEvent(newJournal, time, plays, journalMonsters, journalItems, journalTraps);
            }
            journal = newJournal;

            int newPlays = memoryReader.ReadPlays();
            if (newPlays != plays && newPlays > 0 && newPlays <= 1000)
            {
                tracker.PlaysEvent(newPlays, time);
            }
            plays = newPlays;

            // Tunnel man progress
            int chapter = memoryReader.ReadTunnelChapter();
            int newRemaining = memoryReader.ReadTunnelRemaining();
            if (newRemaining != tunnelManRemaining)
            {
                // Determine which deliverable is next
                if (chapter == 1)
                {
                    if (newRemaining == 2)
                    {
                        // rope
                        tracker.TunnelManEvent("1 Rope");
                    }
                    else if (newRemaining == 1)
                    {
                        // 10k
                        tracker.TunnelManEvent("10k");
                    }
                }
                else if (chapter == 2)
                {
                    // 2 bombs
                    tracker.TunnelManEvent("2 Bombs");
                }
                else if (chapter == 3)
                {
                    if (newRemaining == 2)
                    {
                        // 2 ropes
                        tracker.TunnelManEvent("2 Ropes");
                    }
                    else if (newRemaining == 1)
                    {
                        // shotgun
                        tracker.TunnelManEvent("Shotgun");
                    }
                }
                else if (chapter == 4)
                {
                    // 3 bombs
                    tracker.TunnelManEvent("3 Bombs");
                }
                else if (chapter == 5)
                {
                    if (newRemaining == 2)
                    {
                        // 3 ropes
                        tracker.TunnelManEvent("3 Ropes");
                    }
                    else if (newRemaining == 1)
                    {
                        // key
                        tracker.TunnelManEvent("Key");
                    }
                }
                else if (chapter == 6)
                {
                    // done
                    tracker.TunnelManEvent("Done");
                }
            }
            tunnelManRemaining = newRemaining;
        }
    }
}