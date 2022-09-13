uint[] hpMP = new uint[2];
uint[] stats = new uint[8];
string? userInput = "";
string[] characterNames = { "Tidus", "Yuna", "Auron", "Kimahri", "Wakka", "Lulu", "Rikku", "Seymour" };
int randomSeed = 0;
int seyMan;
uint baseSeed = 0;
bool alsoSeymour = true;
byte[] byteArrayWrite = new byte[4];
byte[] byteArrayHP = new byte[4];
byte[] byteArrayMP = new byte[4];

//Declare variables
for (int i = 0; i < 8; i++)
{
    if (i < 2)
    {
        hpMP[i] = 0x00000000;
    }
    if (i < 4)
    {
        byteArrayWrite[i] = 0;
    }
    stats[i] = 0x00;
}
if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\"))
{
    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\");
}
if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\"))
{
    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\");
}
//Declare variables

Console.WriteLine("This is the Party Stat Randomizer. This randomizes the\nstarting stats for each party member in the game(including Seymour Guado).\n\nMake sure you placed your ply_save.bin in the \"Input\" folder, then press any key to continue.");

FILENOTFOUND:
Console.ReadKey();
Console.Clear();

if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\ply_save.bin"))
{
    Console.WriteLine("File does not exist. Make sure the bin file is placed in\nthat folder and the name is unchanged.");
    goto FILENOTFOUND;
}
try
{
    FileStream testFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\ply_save.bin", FileMode.Open, FileAccess.Read, FileShare.None);
    testFile.Close();
}
catch
{
    Console.WriteLine("File is open in another program. Please close that and try again.");
    goto FILENOTFOUND;
}

FileStream readFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\ply_save.bin", FileMode.Open, FileAccess.Read, FileShare.None);
Console.WriteLine("ply_save.bin has been found. Also randomize Seymour's stats?\n1 - Yes\n2 - No");

NULLINPUT:
userInput = Console.ReadLine();
Console.Clear();

if (userInput == null)
{
    Console.WriteLine("A null input? How silly... Try again.");
    goto NULLINPUT;
}

if (!int.TryParse(userInput, out randomSeed))
{
    Console.WriteLine("Try numbers.");
    goto NULLINPUT;
}

if (randomSeed == 1)
{
    alsoSeymour = true;
}
else if (randomSeed == 2)
{
    alsoSeymour = false;
}
else
{
    Console.WriteLine("Please choose from the given options.");
    goto NULLINPUT;
}

NULLINPUTTWO:
Console.WriteLine("Use a custom seed?\n1 - Yes\n2 - No");

userInput = Console.ReadLine();
Console.Clear();

if (userInput == null)
{
    Console.WriteLine("A null input? How silly... Try again.");
    goto NULLINPUTTWO;
}

if (!int.TryParse(userInput, out randomSeed))
{
    Console.WriteLine("Try numbers.");
    goto NULLINPUTTWO;
}

if (randomSeed == 1)
{
    Console.WriteLine("Input a custom seed within the following range without commas or spaces.\n0 - 4,294,967,295");

    NULLINPUTTHREE:
    userInput = Console.ReadLine();
    Console.Clear();

    if (userInput == null)
    {
        Console.WriteLine("Input was null. Try again.\n0 - 4,294,967,295");
        goto NULLINPUTTHREE;
    }
    if (!uint.TryParse(userInput, out baseSeed))
    {
        Console.WriteLine("Index out of range. Try again, and count your numbers.\n0 - 4,294,967,295");
        goto NULLINPUTTHREE;
    }
    else
    {
        randomSeed = (int)baseSeed;
    }
}
else if (randomSeed == 2)
{
    Random seedRandom = new Random();
    baseSeed = (uint)seedRandom.Next();
}
else
{
    Console.WriteLine("Please choose from the given options.");
    goto NULLINPUTTWO;
}

if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\"))
{
    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\");
}
THEIFANDTRYAGAIN:
Console.Clear();
if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\ply_save.bin"))
{
    try
    {
        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\ply_save.bin");
    }
    catch
    {
        Console.WriteLine("Seed exists and cannot be overwritten.\nMake sure that file is not in use with current seed. \n\nSeed: " + baseSeed.ToString() + "\n\n Press any key to try again.");
        Console.ReadKey();
        goto THEIFANDTRYAGAIN;
    }

}

FileStream saveFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\ply_save.bin", FileMode.Create, FileAccess.ReadWrite, FileShare.None);

readFile.CopyTo(saveFile);
readFile.Close();

Random random = new Random(randomSeed);

if (alsoSeymour)
{
    seyMan = 8;
}
else
{
    seyMan = 7;
}

for (int i = 0; i < seyMan; i++)
{
    saveFile.Position = 0x18 + (0x94 * i);
    //Get values
    for (int j = 0; j < 10; j++)
    {
        if (j < 2)
        {
            byteArrayWrite[0] = (byte)saveFile.ReadByte();
            byteArrayWrite[1] = (byte)saveFile.ReadByte();
            byteArrayWrite[2] = (byte)saveFile.ReadByte();
            byteArrayWrite[3] = (byte)saveFile.ReadByte();
            hpMP[j] = BitConverter.ToUInt32(byteArrayWrite);
        }
        else
        {
            stats[j - 2] = (uint)saveFile.ReadByte();
        }
    }
    //Get Values

    //Randomize them
    for (int j = 0; j < 8; j++)
    {
        if (j < 2)
        {
            hpMP[j] = (uint)random.Next((int)hpMP[j] / 2, ((int)hpMP[j] / 2 * 3) + 1);
            if (j == 1)
            {
                if (hpMP[j] >= 999)
                {
                    hpMP[j] = 999;
                }
            }
        }
        if (stats[j] == 0)
        {
            stats[j] = (uint)random.Next(5, 31);
        }
        else
        {
            stats[j] = (uint)random.Next((int)stats[j] / 2, ((int)stats[j] / 2 * 3) + 1);
        }
        randomSeed++;
    }
    //Randomize them

    //Write them back in
    saveFile.Position = 0x18 + (0x94 * i);

    byteArrayHP = BitConverter.GetBytes(hpMP[0]);
    byteArrayMP = BitConverter.GetBytes(hpMP[1]);
    saveFile.Write(byteArrayHP);
    saveFile.Write(byteArrayMP);
    for (int j = 0; j < 8; j++)
    {
        saveFile.WriteByte((byte)stats[j]);
    }
    for (int j = 0; j < 8; j++)
    {
        saveFile.ReadByte();
    }
    saveFile.Write(byteArrayHP);
    saveFile.Write(byteArrayMP);
    saveFile.Write(byteArrayHP);
    saveFile.Write(byteArrayMP);

    //Write them back in
    Console.WriteLine(characterNames[i] + "'s stats randomized!");
}

saveFile.Close();

Console.WriteLine("Your starting stats have been successfully randomized!\n\nSeed: " + baseSeed.ToString() + "\n\nPress any key to quit.");
Console.ReadKey();