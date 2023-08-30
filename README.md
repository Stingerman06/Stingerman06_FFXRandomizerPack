# Final Fantasy X HD Randomizer Pack

This is a randomizer pack for Final Fantasy X HD. This pack randomizes files extracted from the Data VBF file that comes with the Steam release of the game. As I have no knowledge of the PS3 or PS2 versions of the game files, it's unknown if those will be able to be randomized the same.

These randomizers are all console window, but do not require much else, meaning you don't have to use something like PowerShell or Command Prompt(yay!). But these do use the .NET 6.0 framework. Make sure you have the required dependencies if these randomizers do not start up!

## What You Can Randomize

- Party Stats(including Seymour Guado)
- Aeon Growth Stats

### What You Will Need

1. VBF Browser(I used version 3.2.0.5) - This can be found on NexusMods under Final Fantasy 12's mod section.
2. A way to read .EXE files. This was made originally on a Windows computer, but can be compiled for other machines I'm sure(if not then I can look into it).
3. Backups of the files you will be randomizing(try to avoid backing up the VBF file itself since it's a massive file to begin with).

#### Planned Features For Current Randomizers(priority)

1. The ability to shuffle stats around(including stat shuffling between characters)
2. Options for unique run types
3. Addition of randomizing a starting ability for each character and Aeon(and include a starting ability for Tidus)
4. Utilize 2 and 3 together
5. Option to randomize names(and starting Aeon name choices) in addition to randomizing Party stats.

### Effects on using this Randomizer with other Randomizers

 - The Sphere Grid Randomizer works fine(as intended). However, the stats will not be randomized for one big reason. The Sphere Grid Randomizer creates a new save, which will have the original Stats for the Characters already made. This essentially renders these two Randomizers incompatible, as the game reads from the original files when making a new save file.
 - This randomizer affects the game files themselves, making all runs use the values in those files. Any randomizer(like the above mentioned Sphere Grid Randomizer), creates save files or uses external programs to randomize the game(like Cheat Engine), it is noted that things may differ when using this Randomizer.

#### Planned Randomizers

- Monster Stat Randomizer
- Monster Drops/Steal Randomizing(data needs to be researched more to utilize this)
- (safely)Randomize Monster Locations $
- Field Item Randomizer(from chests and other pickups)
- Shop Item Randomizer
- Unchangeable Equipment Ability Randomizer(such as Chappu's Sword or the Caladbolg for example)


$ This will need to be looked into before I can work on it, since some encounters have some AI scripts in them. An example being the Guado Guardian while leaving Macalania Temple, will "summon" enemies. They actually turn enemies Visible, and simply replacing the enemy with another that is already Visible, will cause a crash when that enemy is targeted to become visible.
