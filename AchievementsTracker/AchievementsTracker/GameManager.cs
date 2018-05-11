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

        public GameManager(Tracker tracker, MemoryReader memoryReader)
        {
            this.tracker = tracker;
            this.memoryReader = memoryReader;
        }

        private void resetRun()
        {
            tracker.DamselEvent(0);
            tracker.ShoppieEvent(0);
        }

        public void update()
        {
            ScreenState newState = (ScreenState)memoryReader.ReadScreenState();
            if ((newState == ScreenState.DeathScreen && state != ScreenState.DeathScreen) || 
                (newState == ScreenState.MainMenu && state != ScreenState.MainMenu))
            {
                // run over
                resetRun();
            }
            if (newState == ScreenState.VictoryWalking && state != ScreenState.VictoryWalking)
            {
                // run victory
                resetRun();
            }
            state = newState;

            int newCharSelect = memoryReader.ReadCharSelect();
            if (state == ScreenState.ChooseCharacter && charSelect == 0 && newCharSelect != 0) {
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
        }
    }
}