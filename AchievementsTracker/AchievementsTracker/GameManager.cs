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

        private bool runIsNoGold;
        private int score;
        private bool runIsValid;
        private int levelIdx;
        private int runTime;
        private int stageTime;

        public GameManager(Tracker tracker, MemoryReader memoryReader)
        {
            this.tracker = tracker;
            this.memoryReader = memoryReader;
        }

        private void startRun()
        {
            Console.WriteLine("A run has begun");
            runIsValid = true;
            runIsNoGold = true;
        }

        private void resetRun()
        {
            Console.WriteLine("A run has been lost!");
            runIsValid = false;
            tracker.DamselEvent(0);
            tracker.ShoppieEvent(0);
        }

        public void update()
        {
            // gold check
            int newScore = memoryReader.ReadScore();
            if (newScore != score && state == ScreenState.Running)
            {
                runIsNoGold = false;
                Console.WriteLine("got gold");
            }
            score = newScore;

            // Times
            int newRunTime = memoryReader.ReadRunTimeInMilliseconds();
            if (newRunTime - runTime < 0 && state == ScreenState.Running)
            {
                // Filter out beginning of olmec, yama cutscenes
                if (!((levelIdx == 20 || levelIdx == 16) && stageTime < 500))
                {
                    // Insta death
                    resetRun();
                }
            }
            runTime = newRunTime;

            // Level index
            levelIdx = memoryReader.ReadLevelIndex();

            // Screen state
            ScreenState newState = (ScreenState)memoryReader.ReadScreenState();
            if (newState == ScreenState.Running && state == ScreenState.Loading2 && levelIdx == 1)
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
                    tracker.HellAchieved();
                }
                // check for speedlunky
                if (runIsValid && runTime < 8 * 60 * 1000)
                {
                    tracker.SpeedlunkyAchieved();
                }
                // check for big money
                if (score >= 500000)
                {
                    tracker.BigMoneyAchieved();
                }
                // check for no gold
                if (runIsValid && runIsNoGold)
                {
                    tracker.NoGoldAchieved();
                }

                resetRun();
            }
            state = newState;

            int newCharSelect = memoryReader.ReadCharSelect();
            if (state == ScreenState.ChooseCharacter && charSelect == 0 && newCharSelect != 0)
            {
                // Start timer
                Console.WriteLine("Run is starting");
                tracker.RunStarted();
            }
            charSelect = newCharSelect;

            int newDamsels = memoryReader.ReadDamselCount();
            if (newDamsels != damselCount && state == ScreenState.Running)
            {
                tracker.DamselEvent(newDamsels);
            }
            damselCount = newDamsels;

            int newShoppies = memoryReader.ReadShoppieCount();
            if (newShoppies != shoppieCount && state == ScreenState.Running)
            {
                tracker.ShoppieEvent(newShoppies);
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
            if (newChars != characters)
            {
                tracker.CharactersEvent(newChars);
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

            if (newJournal != journal)
            {
                tracker.JournalEvent(newJournal);
            }
            journal = newJournal;
        }
    }
}