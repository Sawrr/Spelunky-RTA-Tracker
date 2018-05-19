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
        private RunState state;
        private bool[] achievements;
        private bool nineteenDone;

        public RunManager(Tracker tracker)
        {
            this.tracker = tracker;
            state = RunState.Waiting;
            int numAchievements = Enum.GetNames(typeof(Achievement)).Length;
            achievements = new bool[numAchievements];
            nineteenDone = false;
        }

        public bool IsAchievementDone(Achievement ach)
        {
            return achievements[(int)ach];
        }

        public bool IsRunInProgress()
        {
            return state == RunState.InProgress;
        }

        public void FinishAchievement(Achievement ach)
        {
            int idx = (int)ach;
            if (!achievements[idx])
            {
                achievements[idx] = true;
                Log.WriteLine("Achievement finished: " + ach);
                checkForAllAchievements();
                checkForNineteenAchievements();
            }
        }

        public void StartRun()
        {
            if (state == RunState.Waiting)
            {
                state = RunState.InProgress;
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
            Log.WriteLine("Achievements run completed");
            state = RunState.Done;
            tracker.RunCompleted();
        }

        public bool isNineteenDone()
        {
            return nineteenDone;
        }

        private void checkForNineteenAchievements()
        {
            if (nineteenDone) return;

            for (int i = 0; i < achievements.Length; i++)
            {
                if (!achievements[i] && (Achievement)i != Achievement.Addicted)
                {
                    return;
                }
            }

            // Run complete
            Log.WriteLine("19/20 run completed");
            nineteenDone = true;
            tracker.NineteenAchieved();
        }
    }
}
