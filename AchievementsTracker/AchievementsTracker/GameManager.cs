using System;

namespace AchievementsTracker
{
    class GameManager
    {
        private GameEvents events;
        private MemoryReader memoryReader;

        private ScreenState screenState;
        private int damselCount;
        private int shoppieCount;

        public GameManager(GameEvents events, MemoryReader memoryReader)
        {
            this.events = events;
            this.memoryReader = memoryReader;
        }

        public void update()
        {
            screenState = (ScreenState)memoryReader.ReadScreenState();

            int newDamsels = memoryReader.ReadDamselCount();
            if (newDamsels != damselCount && screenState == ScreenState.Running)
            {
                events.DamselChange(newDamsels);
            }
            damselCount = newDamsels;

            int newShoppies = memoryReader.ReadShoppieCount();
            if (newShoppies != shoppieCount && screenState == ScreenState.Running)
            {
                events.ShoppieChange(newShoppies);
            }
            shoppieCount = newShoppies;
        }
    }
}