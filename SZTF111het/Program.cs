using System;
using System.IO;

namespace SZTF111het
{
    class Country
    {
        public string IsoCode { get; }
        public string Continent { get; }
        public string Location { get; }

        public DateTime Date { get; }
        //Date = DateTime.ParseExact(date, "yyyy,MM,dd", null);

        public double ReproductionRate { get; }

        public Case Cases { get; }
        public Case Deaths { get; }
        public Hospital Hospital { get; }
        public Test Tests { get; }
        public Population Population { get; }

        public Country(string isocode, string continent, string location, string date, string reproductionrate, Case cases, Case deaths, Hospital hospital, Test test, Population population)
        {
            IsoCode = isocode;
            Continent = continent;
            Location = location;

            Date = DateTime.Parse(date); //2020.10.10 -> 2020,10,10
            Date = DateTime.ParseExact(date, "yyyy,MM,dd", null);

            if (reproductionrate != "")
                ReproductionRate = double.Parse(reproductionrate);

            Cases = cases;
            Deaths = deaths;
            Hospital = hospital;
            Tests = test;
            Population = population;
        }

        public override string ToString()
        {
            return $"{IsoCode}\t{Continent}\t{Location}\t{Date}\t{ReproductionRate}\t{Cases}\t{Deaths}\t{Hospital}\t{Tests}\t{Population}";
        }
    }
    class Case
    {
        public double Total { get; }
        public double New { get; }
        public double Smoothed { get; }

        public string Ammount { get; }
        public double TotalPerAmmount { get; }
        public double NewPerAmmount { get; }
        public double SmoothedPerAmmount { get; }

        public Case(string total, string _new, string smoothed, string totalperammount, string newperammount, string smoothedperammount, string ammount)
        {
            if (total != "")
                Total = double.Parse(total);
            if (_new != "")
                //New = double.Parse(_new, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo); //0,0
                New = double.Parse(_new);
            if (smoothed != "")
                Smoothed = double.Parse(smoothed);

            Ammount = ammount;
            if (totalperammount != "")
                TotalPerAmmount = double.Parse(totalperammount);
            if (newperammount != "")
                NewPerAmmount = double.Parse(newperammount);
            if (smoothedperammount != "")
                SmoothedPerAmmount = double.Parse(smoothedperammount);
        }

        public override string ToString()
        {
            return $"{Total}\t{New}\t{Smoothed}\t{Ammount}\t{TotalPerAmmount}\t{NewPerAmmount}\t{SmoothedPerAmmount}";
        }
    }
    class Hospital
    {
        public double Patients { get; }
        public double PatientsPerMillion { get; }

        public Hospital(string patients, string patientspermillion)
        {
            if (patients != "")
                Patients = double.Parse(patients);
            if (patientspermillion != "")
                PatientsPerMillion = double.Parse(patientspermillion);
        }

        public override string ToString()
        {
            return $"{Patients}\t{PatientsPerMillion}";
        }
    }
    class Test
    {
        public double TestPerCase { get; }
        public double PositiveRate { get; }
        public Case Tests { get; }

        public Test(string testpercase, string positiverate, Case test)
        {
            if (testpercase != "")
                TestPerCase = double.Parse(testpercase);
            if (positiverate != "")
                PositiveRate = double.Parse(positiverate);

            Tests = test;
        }

        public override string ToString()
        {
            return $"{TestPerCase}\t{PositiveRate}\t{Tests}";
        }
    }
    class Population
    {
        public double Populations { get; }
        public double Density { get; }
        public double MedianAge { get; }
        public double Aged65Older { get; }
        public double Aged70Older { get; }

        public Population(string populations, string density, string medianage, string aged65older, string aged70older)
        {
            if (populations != "")
                Populations = double.Parse(populations);
            if (density != "")
                Density = double.Parse(density);
            if (medianage != "")
                MedianAge = double.Parse(medianage);
            if (aged65older != "")
                Aged65Older = double.Parse(aged65older);
            if (aged70older != "")
                Aged70Older = double.Parse(aged70older);
        }

        public override string ToString()
        {
            return $"{Populations}\t{Density}\t{MedianAge}\t{Aged65Older}\t{Aged70Older}";
        }
    }

    class DataHandler
    {
        Country[] countries = new Country[0];   // null

        void ExtendArray(Country country)
        {
            Country[] newCountries = new Country[countries.Length + 1];
            for (int i = 0; i < countries.Length; i++)
                newCountries[i] = countries[i];
            newCountries[countries.Length] = country;
            countries = newCountries;
        }

        public void Load(string path)
        {
            string[] fájlok = Directory.GetFiles(path, "*.csv");

            for (int i = 0; i < fájlok.Length; i++)
            {
                // string[] sorok = File.ReadAllLines(fájlok[i]);

                StreamReader sr = new StreamReader(fájlok[i]);
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string[] sorok = sr.ReadLine().Replace('.', ',').Split(';');

                    //0-3   (Country)     iso_code	                continent	            location	        date
                    //4-6   (Case1)       total_cases	            new_cases	            new_cases_smoothed
                    //7-9   (Case2)       total_deaths	            new_deaths	            new_deaths_smoothed
                    //10-12 (Case1)       total_cases_per_million   new_cases_per_million	new_cases_smoothed_per_million
                    //13-15 (Case2)       total_deaths_per_million	new_deaths_per_million	new_deaths_smoothed_per_million
                    //16    (Country)     reproduction_rate
                    //17-18 (Hospital)    hosp_patients	            hosp_patients_per_million
                    //19-26 (Test)     
                    //                    (Case3)                   total_tests	            new_tests	            total_tests_per_thousand
                    //                                              new_tests_per_thousand	new_tests_smoothed	    new_tests_smoothed_per_thousand
                    //                    tests_per_case            positive_rate
                    //27-31 (Population)  population	            population_density	    median_age	            aged_65_older	    aged_70_older

                    Case cases = new Case(sorok[4], sorok[5], sorok[6], sorok[10], sorok[11], sorok[12], "Million");
                    Case deaths = new Case(sorok[7], sorok[8], sorok[9], sorok[13], sorok[14], sorok[15], "Million");
                    Hospital hospital = new Hospital(sorok[17], sorok[18]);
                    Test tests = new Test(sorok[25], sorok[26], new Case(sorok[19], sorok[20], sorok[21], sorok[22], sorok[23], sorok[24], "Thousands"));
                    Population population = new Population(sorok[27], sorok[28], sorok[29], sorok[30], sorok[31]);

                    ExtendArray(new Country(sorok[0], sorok[1], sorok[2], sorok[3], sorok[16], cases, deaths, hospital, tests, population));
                }

                sr.Close();
            }
        }

        public void SaveDataPerCountry()
        {
            for (int i = 0; i < countries.Length; i++)
            {
                if (!Directory.Exists(Path.Combine("eredmények", countries[i].Location)))
                    Directory.CreateDirectory(Path.Combine("eredmények", countries[i].Location));

                File.WriteAllText(Path.Combine("eredmények", countries[i].Location, $"{countries[i].IsoCode}_{countries[i].Date.ToString("yyMMdd")}.txt"), countries[i].ToString());
            }
        }

        public double NewCaseAtDayDAy(DateTime day)
        {
            double nbr = 0;
            for (int i = 0; i < countries.Length; i++)
            {
                if (countries[i].Date == day)
                    nbr += countries[i].Cases.New;
            }
            return nbr;
        }
    }

    class Program
    {
        static void Dátum()
        {
            DateTime start = DateTime.Now;

            DateTime dátum1 = new DateTime(2021, 11, 16);
            DateTime dátum2 = DateTime.Parse("2021.11.10");

            DateTime end = DateTime.Now;
            Console.WriteLine($"futási idő: {end - start}");
        }

        static void Fájl()
        {

        }

        static void Main(string[] args)
        {
            Dátum();

            DataHandler handler = new DataHandler();
            handler.Load(".");
            // handler.SaveDataPerCountry();
            Console.WriteLine($"Esetek: {handler.NewCaseAtDayDAy(new DateTime(2020, 11, 15))}");
        }
    }
}
