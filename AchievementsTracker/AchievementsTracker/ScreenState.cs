using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementsTracker
{
    enum ScreenState
    {
        Running = 0, Loading1 = 1, Loading2 = 2, Loading3 = 3, PauseMenu = 4, TitleScreen = 5, Leaderboards = 7, HelpAndOptions = 8, LevelCompleted = 11, Intro = 14, MainMenu = 15, ChooseCharacter = 17, VictoryWalking = 18, VictoryEruption = 19, VictoryOutside = 20, PlayerStats = 21, OpeningScreen = 22, DeathmatchArena = 23, DeathmatchMenu = 25, Tutorial = 26, DeathmatchConfig = 27, DeathmatchResults = 28, Journal = 29, DeathScreen = 30, Credits = 31
    }
}