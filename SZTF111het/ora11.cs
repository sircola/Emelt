using System;
using System.IO;

namespace Ora11
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fájlok/Mappák elérése
            // abszolút útvonal                 - C:\Ora11\Fájlok\valami.txt
            // relatív útonal                   - ..\..\..\Fájlok\valami.txt
            // amennyiben az exe mellett van    - valami.txt

            DataHandler handler = new DataHandler();
            handler.Load("covid");

            string[] countries = handler.GetCountries();
            double nbr = handler.NewCaseAtDay(new DateTime(2020, 11, 15));
            string rank = handler.RankOfCountryByHospitalPatientsPerMillion("Hungary");
            TimeSpan doupling = handler.CountryCaseDoupling("Hungary");
            Console.WriteLine(doupling.Days);
            handler.SaveDataPerCountry();

            Console.ReadLine();
        }
    }

    #region Adat osztályok
    // Adott az exe mellett egy 'covid' mappa, amiben a 'covid.cs' fájlt kell feldolgozni.
    // (1) A fájl alapján az alábbi osztályokat, az alábbi tulajdonságokkal kell létrehozni:
    //      - Country: 
    //              (string) IsoCode, Continent, Location
    //              (DateTime) Date
    //              (double) ReproductionRate
    //              (Case) Cases, Deaths
    //              (Hospital) Hospital
    //              (Test) Tests
    //              (Population) Population
    //      - Case:
    //              (double) Total, New, Smoothed, TotalPerAmmount, NewPerAmmount, SmoothedPerAmmount
    //              (string) Ammount
    //      - Hospital:
    //              (double) Patients, PatientsPerMillion
    //      - Test:
    //              (double) TestPerCase, Positiverate
    //              (Case) Tests
    //      - Population
    //              (double) Populations, Density, MedianAge, Aged65Older, Aged70Older
    //  Az osztályok csak olvasható adatszerkezetek leszenek, létrejöttüket követően tartalmuk ne legyen módosítható!

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
    #endregion

    #region Lekérdezések
    // (2) Legyen egy 'DataHandler' osztály a következő metódusokkal:
    //      - void Load(string path): egyetlen paramétere az adatokat tartalmazó mappa útvonalát tartalmazza,
    //                                amiben megkeresi az összes *.cs fájlt, ami alapján létrehozza a Country
    //                                osztály tömbjét, amit egy belső adatmezőben eltárol
    //                                A Country osztályt egészítse ki egy konstruktorral.

    /**/
    // (3) Egészítse ki a 'DataHandler' osztályt a következő "lekérdezésekkel":
    //      - string[] GetCountries()
    //                  visszaadja egy string tömbben az országok neveit
    //      - SaveDataPerCountry()
    //                  országonként hozzon létre egy mappát és abba mentse el külön fájlba az egyes bejegyzéseit a tömbnek tabulátorral elválasztva,
    //                  a fájlok neve az ország iso kódja, dátum yyMMdd formában legyen alulvonással elválasztva
    //      - double NewCaseAtDay(DateTime day)
    //                  az adott napi új esetek számát adja vissza
    //      - string RankOfCountryByHospitalPatientsPerMillion(string country)
    //                  rendezze az országokat az adott napi korházi betegek száma / millió lakos szerint, majd adja meg,
    //                  hogy az adott ország hanyadik a sorrendben, zárójelben a sorszámot kövesse az országok száma
    //                  --az eredeti tömb sorrendje maradjon változatlan!
    //      - TimeSpan CountryCaseDoupling(string country)
    //                  adja meg hogy az adott ország fertőzött esetek száma hány nap alatt duplázódott meg (első duplázódás)
    //                  --ha ez nem történt meg akkor 0 időponttal térjen vissza
    class DataHandler
    {
        Country[] countries = new Country[0];

        /// <summary>
        /// Létrehoz egy egyel nagyobb elemű tömböt, amibe átmásolja a régi tömb elemeit, majd az új tömb utolsó helyére
        /// értékül adja a paraméterül átadott elemet, végül az új tömböt értékül adja a régi tömbnek.
        /// </summary>
        /// <param name="country">Az új elem, amivel bővíti a tömböt.</param>
        private void ExtendArray(Country country)
        {
            Country[] newCountries = new Country[countries.Length + 1];
            for (int i = 0; i < countries.Length; i++)
                newCountries[i] = countries[i];
            newCountries[newCountries.Length - 1] = country;
            countries = newCountries;
        }

        public void Load(string path)
        {
            string[] files = Directory.GetFiles(path, "*.csv"); // a patternnél csak egyszerű feltételt lehet megadni

            for (int i = 0; i < files.Length; i++)
            {
                //string[] lines = File.ReadAllLines(files[i]);
                StreamReader reader = new StreamReader(files[i]);
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine().Replace('.', ',').Split(';');

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

                    Case cases = new Case(line[4], line[5], line[6], line[10], line[11], line[12], "Million");
                    Case deaths = new Case(line[7], line[8], line[9], line[13], line[14], line[15], "Million");
                    Hospital hospital = new Hospital(line[17], line[18]);
                    Test tests = new Test(line[25], line[26], new Case(line[19], line[20], line[21], line[22], line[23], line[24], "Thousands"));
                    Population population = new Population(line[27], line[28], line[29], line[30], line[31]);

                    ExtendArray(new Country(line[0], line[1], line[2], line[3], line[4], cases, deaths, hospital, tests, population));
                }
                reader.Close();
            }
        }

        public string[] GetCountries()
        {
            string ret = "?";
            for (int i = 0; i < countries.Length; i++)
                if (!ret.Contains("?" + countries[i].Location + "?"))
                    ret += countries[i].Location + "?";
            return ret.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public void SaveDataPerCountry()
        {
            for (int i = 0; i < countries.Length; i++)
            {
                //if (Directory.Exists($"del//{countries[i].Location}"))
                if (!Directory.Exists(Path.Combine("del", countries[i].Location)))
                    Directory.CreateDirectory(Path.Combine("del", countries[i].Location));

                File.WriteAllText(
                    Path.Combine("del", countries[i].Location, $"{countries[i].IsoCode}_{countries[i].Date.ToString("yyMMdd")}.txt")
                    , countries[i].ToString());
            }
        }
        public double NewCaseAtDay(DateTime day)
        {
            double nbr = 0;
            for (int i = 0; i < countries.Length; i++)
            {
                if (countries[i].Date == day)
                    nbr += countries[i].Cases.New;
            }
            return nbr;
        }
        public string RankOfCountryByHospitalPatientsPerMillion(string country)
        {
            string[] c = GetCountries();
            double[] p = new double[c.Length];

            for (int i = 0; i < countries.Length; i++)
            {
                int j = 0;
                while (j < c.Length && countries[i].Location != c[j])
                    j++;

                p[j] += countries[i].Hospital.PatientsPerMillion;
            }

            for (int i = 0; i < p.Length - 1; i++) // egyszerű cserés rendezés
                for (int j = i + 1; j < p.Length; j++)
                    if (p[i] < p[j])
                    {
                        double temp = p[i];
                        p[i] = p[j];
                        p[j] = temp;

                        string temp2 = c[i];
                        c[i] = c[j];
                        c[j] = temp2;
                    }


            int k = 0;
            while (k < c.Length && c[k] != country) k++;

            return $"{k} ({c.Length})";
        }
        public TimeSpan CountryCaseDoupling(string country)
        {
            string[] c = GetCountries();

            int idx = 0;
            double init = 0;
            for (int i = 0; i < countries.Length; i++)
            {
                if (countries[i].Location == country)
                    if (init == 0)
                    {
                        idx = i;
                        init = countries[i].Cases.Total;
                    }
                    else if (countries[i].Cases.Total / 2 > init)
                    {
                        return countries[i].Date - countries[idx].Date;
                    }
            }
            return new TimeSpan(0);
        }
    }
    #endregion
}
