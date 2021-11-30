using System;
using System.IO;


namespace ZH2gyak
{
    class Program
    {
        static void Main(string[] args)
        {
            FileHandler.srcPath = "MONITORING.DAT";
            FileHandler.dstPath = "ANALYSIS.RESULT";
            Analyzer analyzer = new Analyzer();
            analyzer.Analysis();
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
            string[] lines = new string[int.Parse(sr.ReadLine())];

            while (!sr.EndOfStream)
            {
                lines[i] = sr.ReadLine();
                i++;
            }

            sr.Close();

            return lines;
        }

        public static void Append(string line)
        {
            StreamWriter sw = new StreamWriter(dstPath, true);
            sw.WriteLine(line);
            sw.Close();
        }
    }

    class Analyzer
    {
        Phone[] phones;

        public Analyzer()
        {
            string[] p = FileHandler.Load();
            phones = new Phone[p.Length];

            for (int i = 0; i < p.Length; i++)
            {
                string[] d = p[i].Split(':');
                phones[i] = new Phone(d[0], d[1], d[2].Split(';'));
            }
        }

        public void Analysis()
        {
            for (int i = 0; i < phones.Length; i++)
            {
                if (OSCause(phones[i])) FileHandler.Append(phones[i].ToString());
                else if (ACCause(phones[i])) FileHandler.Append(phones[i].ToString());
                else FileHandler.Append(phones[i].ToString());
            }
        }

        bool OSCause(Phone phone)
        {
            for (int i = 0; i < phone.BatteryLevels.Length - 1; i++)
            {
                if (phone.BatteryLevels[i] == 30 & phone.BatteryLevels[i + 1] < 5)
                {
                    phone.FC = FaultCause.OS;
                    return true;
                }
            }
            return false;
        }
        bool ACCause(Phone phone)
        {
            for (int i = 1; i < phone.BatteryLevels.Length; i++)
            {
                if (phone.BatteryLevels[i - 1] > 7 + phone.BatteryLevels[i])
                {
                    phone.FC = FaultCause.AC;
                    return true;
                }
            }
            return false;
        }
    }

    enum FaultCause { OS, AC, OK }
    class Phone
    {
        public string ID { get; }
        public string Type { get; }
        public int[] BatteryLevels { get; }
        public FaultCause FC { get; set; }
            
        public Phone(string id, string type, string[] batteryLevel)
        {
            ID = id;
            Type = type;

            BatteryLevels = new int[batteryLevel.Length];
            for (int i = 0; i < batteryLevel.Length; i++)
            {
                BatteryLevels[i] = int.Parse(batteryLevel[i]);
            }

            FC = FaultCause.OK;
        }

        public string ToString()
        {
            return ID + ":" + FC;
        }
    }
}
