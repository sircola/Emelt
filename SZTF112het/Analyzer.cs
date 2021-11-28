using System.IO;

class Analyzer
{
    Phone[] phones;

    public Analyzer()
    {
        string[] adatok = FileHandler.Load();
        phones = new Phone[adatok.Length];
        for (int i = 0; i < adatok.Length; i++)
        {
            string[] tmpString = adatok[i].Split(':');
            phones[i] = new Phone(tmpString[0], tmpString[1], tmpString[2].Split(';'));
        }
    }

    private bool OSCause(Phone phone)
    {
        for (int i = 1; i < phone.BatteryLevels.Length; i++)
        {

            if (phone.BatteryLevels[i - 1] == 30 && phone.BatteryLevels[i] < 5)
            {
                phone.FC = FaultCause.OS;
                return true;
            }
        }
        return false;
    }
    private bool ACCause(Phone phone)
    {
        for (int i = 1; i < phone.BatteryLevels.Length; i++)
        {
            if (phone.BatteryLevels[i - 1] - phone.BatteryLevels[i] > 7)
            {
                phone.FC = FaultCause.AC;
                return true;
            }
        }
        return false;
    }

    public void Analysis()
    {
        for (int i = 0; i < phones.Length; i++)
        {
            if (!OSCause(phones[i]))
                ACCause(phones[i]);
            FileHandler.Append(phones[i].ToString());
        }
    }

}

static class FileHandler
{
    public static string srcPath { get; set; }
    public static string dstPath { get; set; }

    public static string[] Load()
    {
        StreamReader sr = new StreamReader(srcPath);
        int i = 0;
        string[] tomb = new string[int.Parse(sr.ReadLine())];
        while (!sr.EndOfStream)
            tomb[i++] = sr.ReadLine();
        sr.Close();
        return tomb;
    }
    public static void Append(string hozzafuzendo)
    {
        StreamWriter sw = new StreamWriter(dstPath, true);
        sw.WriteLine(hozzafuzendo);
        sw.Close();
    }
}

enum FaultCause { OS, AC, OK }
class Phone
{
    public string ID { get; }
    public string Type { get; }
    public int[] BatteryLevels { get; }
    public FaultCause FC { get; set; }

    public Phone(string id, string type, string[] batterLevels)
    {
        ID = id;
        Type = type;
        BatteryLevels = new int[batterLevels.Length];
        for (int i = 0; i < batterLevels.Length; i++)
        {
            BatteryLevels[i] = int.Parse(batterLevels[i]);
        }
        FC = FaultCause.OK;
    }

    public override string ToString()
    {
        return $"{ID}:{FC}";
    }
}

class ZHPrg
{
    static void Main(string[] args)
    {
        FileHandler.srcPath = "MONITORING.DAT";
        FileHandler.dstPath = "ANALYSIS.RESULT";
        Analyzer analyzer = new Analyzer();
        analyzer.Analysis();
        ;
    }
}
