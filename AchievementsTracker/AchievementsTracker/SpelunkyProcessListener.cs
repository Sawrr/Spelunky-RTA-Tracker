using System;
using System.Diagnostics;

namespace AchievementsTracker
{
    class SpelunkyProcessListener
    {
        private const int SLEEP_TIME = 1000;

        public static Process listenForSpelunkyProcess()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(SLEEP_TIME);

                Process[] spelunkies = Process.GetProcessesByName("spelunky");
                if (spelunkies.Length == 1)
                {
                    return spelunkies[0];
                }
                else if (spelunkies.Length > 1)
                {
                    throw new Exception("You have more than one spelunky process open O__O");
                }
            }
        }
    }
}