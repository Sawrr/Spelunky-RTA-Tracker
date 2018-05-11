using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementsTracker
{
    class RunManager
    {
        private Tracker tracker;
        //private RunState state;
        private bool[] achievements;

        public RunManager(Tracker tracker)
        {
            this.tracker = tracker;
            int numAchievements = Enum.GetNames(typeof(Achievement)).Length;
            achievements = new bool[numAchievements];
        }

        public void FinishAchievement(Achievement ach)
        {
            int idx = (int)ach;
            if (!achievements[idx])
            {
                achievements[idx] = true;
                Console.WriteLine("Achievement finished: " + ach);
                checkForAllAchievements();
            }
        }

        private void checkForAllAchievements()
        {
            for (int i = 0; i < achievements.Length; i++)
            {
                if (!achievements[i])
                {
                    return;
                }
            }

            // Run complete
            Console.WriteLine("You did it!");
            tracker.RunCompleted();
        }
    }
}
