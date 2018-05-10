using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementsTracker
{
    class GameEvents
    {
        public event DamselEventHandler DamselEvent;
        public event ShoppieEventHandler ShoppieEvent;

        public delegate void DamselEventHandler(int num);
        public delegate void ShoppieEventHandler(int num);

        public void DamselChange(int num)
        {
            DamselEvent(num);
        }

        public void ShoppieChange(int num)
        {
            ShoppieEvent(num);
        }
    }
}
