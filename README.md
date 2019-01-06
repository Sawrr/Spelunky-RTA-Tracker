# Spelunky RTA Tracker
This tracker began life as a tracker for the All Achievements category. The tracker now supports major RTA categories including:
1. All Achievements
2. All Journal Entries
3. All Characters
4. All Shortcuts + Olmec
5. Tutorial%

<img src="https://github.com/Sawrr/Spelunky-RTA-Tracker/blob/master/SpelunkyRTATracker_ASO.png" width=321>

## The All Achievements category
*Complete all of the achievements starting from a brand new save file. The timer begins with the choice of player character and stops once the 20th achievement is completed.*

The AA run has been largely inaccessible until now due to the need for a fresh steam account as well as a tedious death grind. This tracker removes the need for a fresh steam account and provides an option to avoid unnecessary death grinding by extrapolating the time you would finish the run.

<img src="https://github.com/Sawrr/Spelunky-AllAchievements/blob/master/tracker-windows-update.PNG">

## Achievements
The 20 achievements can be covered by a subset of 9:
1. Speedlunky
2. Big Money
3. No Gold
4. Good Teamwork
5. All Journal Entries
6. All Characters
7. Casanova
8. Public Enemy
9. Addicted

The tracker displays the progress toward these achievements, and timestamps them upon completion. Achievements can be completed in any order.

## Extrapolation
The Addicted achievement is the most time-consuming achievement and some runners may not wish to death grind for up to 90 minutes. As an alternative, when every achievement except Addicted is unlocked the tracker will extrapolate the run completion time using a rate of 6 seconds per death. `extrapolated time = current time + 6 * (1000 - plays)`

## Visual Tracker
The tracker includes a window with icons for all of the characters and journal entries, which disappear upon being unlocked. The purpose of the included visual is to eliminate the need to run separate programs to have visual tracking aids.

## Settings
The application creates an icon in the taskbar which you can right click to access the Settings menu. You can set a hotkey for resetting the tracker, which will be triggered regardless of whether the tracker is in focus! Background color and text color is also configurable. Settings are saved in a settings file in the same directory as the .exe

## Note about Good Teamwork
The tracker only observes the health of Players 1 and 2 for the Good Teamwork achievement. Using more than one extra spelunker may cause the tracker to not consider the achievement completed if either P1 or P2 is dead when the run ends. Use only two spelunkers for the achievement to be properly tracked.

## Requirements
.NET Framework 4.6.1 or higher
- You probably already have this unless you run an older OS like Windows 7 and haven't updated
- Download here if you need it: https://www.microsoft.com/net/download/dotnet-framework-runtime

## Download
[Download the beta](https://github.com/Sawrr/Spelunky-AllAchievements/releases/download/1.7.0/AchievementsTracker.exe) or check the Releases tab
