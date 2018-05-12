using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementsTracker
{
    class Log
    {
        public static void WriteLine(string str)
        {
            Console.WriteLine(DateTime.Now.ToString("[HH:mm:ss.fff] ") + str);
        }
    }
}
