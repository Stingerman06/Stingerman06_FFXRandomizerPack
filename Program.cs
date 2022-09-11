//Stat Arrays for later
string[] aeonName = { "Valefor", "Ifrit", "Ixion", "Shiva", "Bahamut", "Anima", "Yojimbo", "Cindy", "Sandy", "Mindy" };
uint[] healthPoints = new uint[20];
uint[] magicPoints = new uint[20];
uint[] strength = new uint[20];
uint[] defense = new uint[20];
uint[] magicAttack = new uint[20];
uint[] magicDefense = new uint[20];
uint[] agility = new uint[20];
uint[] evasion = new uint[20];
uint[] accuracy = new uint[20];
byte[] byteBuffer = new byte[20];
int randomSeed;
string? userInput = "";
int baseSeed;
//Stat Arrays for later

//Some declarations
for (int i = 0; i < 20; i++)
{
    healthPoints[i] = 0x0000;
    magicPoints[i] = 0x0000;
    strength[i] = 0x00;
    defense[i] = 0x00;
    magicAttack[i] = 0x00;
    magicDefense[i] = 0x00;
    agility[i] = 0x00;
    evasion[i] = 0x00;
    accuracy[i] = 0x00;
}
if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\"))
{
    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\");
}
if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\"))
{
    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\");
}
//Some declarations

CHECKFAIL:
Console.WriteLine("Place your 'sum_assure.bin' file into the \"Input\" folder, then press any key to continue.");
Console.ReadKey();
Console.Clear();

if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\" + "sum_assure.bin"))
{
    Console.WriteLine("File does not exist. Make sure the file is spelled correctly and try again.");
    goto CHECKFAIL;
}

FileStream readFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Input\\" + "sum_assure.bin", FileMode.Open, FileAccess.Read, FileShare.Read);

Console.WriteLine("File exists. Custom seed?\n1 - Yes\n2 - No");
BADINPUT:
userInput = Console.ReadLine();
Console.Clear();

if (userInput == null)
{
    Console.WriteLine("Null input. Plase input a number.");
    goto BADINPUT;
}
else
{
    if (!int.TryParse(userInput, out randomSeed))
    {
        Console.WriteLine("There's a stray character that isn't a number. Try again.");
        goto BADINPUT;
    }
    else
    {
        if (randomSeed != 1 && randomSeed != 2)
        {
            Console.WriteLine("Invalid input. Try again.\n");
            goto BADINPUT;
        }
    }
}

if (randomSeed == 1)
{
    NOTSOBADINPUT:
    Console.WriteLine("Input any 32 bit number for the seed.");
    userInput = Console.ReadLine();
    Console.Clear();
    if (!int.TryParse(userInput, out randomSeed))
    {
        Console.WriteLine("Invalid input. Try again, but with numbers.");
        goto NOTSOBADINPUT;
    }
}
else
{
    Random seedRNG = new Random();
    randomSeed = seedRNG.Next();
}

baseSeed = randomSeed;

if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\"))
{
    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\");
}

TRYFILEAGAIN:
try
{
    FileStream testFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\sum_assure.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
    {
        testFile.Close();
    }
}
catch
{
    Console.WriteLine("File is open. Please close anything that may have this file open before continuing.\nThen press any key to continue.");
    Console.ReadKey();
    Console.Clear();
    goto TRYFILEAGAIN;
}

FileStream saveFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Output\\seed " + baseSeed.ToString() + "\\sum_assure.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
readFile.CopyTo(saveFile);
readFile.Close();

for (int i = 0; i < 10; i++)
{
    saveFile.Position = 0x14 + (0x0C * i);
    for (int j = 0; j < 20; j++)//Get the values
    {
        saveFile.Position = 0x14 + (0x0C * i) + (0x78 * j);
        byteBuffer[0] = (byte)saveFile.ReadByte();
        byteBuffer[1] = (byte)saveFile.ReadByte();
        healthPoints[j] = (uint)BitConverter.ToInt16(byteBuffer, 0);
        byteBuffer[0] = (byte)saveFile.ReadByte();
        byteBuffer[1] = (byte)saveFile.ReadByte();
        magicPoints[j] = (uint)BitConverter.ToInt16(byteBuffer, 0);
        strength[j] = (uint)saveFile.ReadByte();
        defense[j] = (uint)saveFile.ReadByte();
        magicAttack[j] = (uint)saveFile.ReadByte();
        magicDefense[j] = (uint)saveFile.ReadByte();
        agility[j] = (uint)saveFile.ReadByte();
        evasion[j] = (uint)saveFile.ReadByte();
        accuracy[j] = (uint)saveFile.ReadByte();
    }//Get the values
    for (int j = 0; j < 20; j++)//Randomize them
    {
        Random random = new Random(randomSeed);
        healthPoints[j] = (uint)random.Next((int)healthPoints[j] / 2, 8001);
        randomSeed++;
        magicPoints[j] = (uint)random.Next((int)magicPoints[j] / 2, 501);
        randomSeed++;
        strength[j] = (uint)random.Next((int)strength[j] / 2, 129);
        defense[j] = (uint)random.Next((int)defense[j] / 2, 129);
        magicAttack[j] = (uint)random.Next((int)magicAttack[j] / 2, 129);
        magicDefense[j] = (uint)random.Next((int)magicDefense[j] / 2, 129);
        agility[j] = (uint)random.Next((int)agility[j] / 2, 129);
        evasion[j] = (uint)random.Next((int)evasion[j] / 2, 129);
        accuracy[j] = (uint)random.Next((int)accuracy[j] / 2, 129);
    }//Randomize them
    Array.Sort(healthPoints);
    Array.Sort(magicPoints);
    Array.Sort(strength);
    Array.Sort(defense);
    Array.Sort(magicAttack);
    Array.Sort(magicDefense);
    Array.Sort(agility);
    Array.Sort(evasion);
    Array.Sort(accuracy);
    for (int j = 0; j < 20; j++)//Reinsert them
    {
        saveFile.Position = 0x14 + (0x0C * i) + (0x78 * j);
        byteBuffer = BitConverter.GetBytes(healthPoints[j]);
        saveFile.Write(byteBuffer, 0, byteBuffer.Length / 2);
        byteBuffer = BitConverter.GetBytes(magicPoints[j]);
        saveFile.Write(byteBuffer, 0, byteBuffer.Length / 2);
        saveFile.WriteByte((byte)strength[j]);
        saveFile.WriteByte((byte)defense[j]);
        saveFile.WriteByte((byte)magicAttack[j]);
        saveFile.WriteByte((byte)magicDefense[j]);
        saveFile.WriteByte((byte)agility[j]);
        saveFile.WriteByte((byte)evasion[j]);
        saveFile.WriteByte((byte)accuracy[j]);
    }//Reinsert them

    Console.WriteLine("Aeon \"" + aeonName[i] + "\" randomized!");
}
saveFile.Close();

Console.WriteLine("\nAeon stats successfully randomized!\n\nSeed: " + baseSeed + "\n\nPress any key to exit.");
Console.ReadKey();