using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace Pars2012GUI
{
    class Versenyző
    {
        public string Név { get; private set; }
        public char Csoport { get; private set; }
        private string NemzetÉsKód { get; set; }
        public string Sorozat { get; set; }
        public double[] Dobások { get; private set; }

        // 8. feladat: Nemzet kódtag
        public string Nemzet
        {
            get
            {
                string[] m = NemzetÉsKód.Split(' ');
                return String.Join(" ", m.Take(m.Length - 1));
            }
        }

        // 8. feladat: Kód kódtag, zárójelek eltávolítása
        public string Kód => NemzetÉsKód.Split(' ').Last().Replace("(", "").Replace(")", "");

        public bool Bejutott78m => Dobások.Contains(-2);

        // 7. feladat: Legnagyobb dobás, vagy -1 ha nem volt érvényes kísérlet:
        public double Eredmény
        {
            get
            {
                double max = Dobások[0];
                foreach (var i in Dobások.Skip(1)) if (i > max) max = i;
                return max;
            }
        }

        // 3. feladat: Konstruktor kódolása
        public Versenyző(string sor)
        {
            string[] m = sor.Split(';');
            Név = m[0];
            Csoport = char.Parse(m[1]);
            NemzetÉsKód = m[2];
            Sorozat = $"{m[3]};{m[4]};{m[5]}";
            Dobások = new double[3];
            for (int i = 0; i < Dobások.Length; i++)
            {
                if (m[i + 3] == "X") Dobások[i] = -1.0; // érvénytelen kísérlet: -1.0
                else if (m[i + 3] == "-") Dobások[i] = -2.0; // már nem dobott, mert bejutott 78m feletti korábbi dobással: -2.0
                else Dobások[i] = double.Parse(m[i + 3]);
            }
        }
    }

    public partial class MainWindow : Window
    {
        private List<Versenyző> versenyzők = new List<Versenyző>(); // áthozva a konzol alkalmazásból

        public MainWindow()
        {
            foreach (var i in File.ReadAllLines("../../Selejtezo2012.txt", Encoding.UTF8).Skip(1))
            {
                versenyzők.Add(new Versenyző(i));
            }
            InitializeComponent();
            foreach (var i in versenyzők)
            {
                cbNév.Items.Add(i.Név);
                if (i.Név == "Pars Krisztián") cbNév.SelectedItem = i.Név;
            }
        }

        private void cbNév_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Versenyző kiválasztottVersenyző = versenyzők[cb.SelectedIndex];
            lbCsoport.Content = $"Csoport: {kiválasztottVersenyző.Csoport}";
            lbNemzet.Content = $"Nemzet: {kiválasztottVersenyző.Nemzet}";
            lbNemzetKód.Content = $"Nemzet kód: {kiválasztottVersenyző.Kód}";
            lbSorozat.Content = $"Sorozat: {kiválasztottVersenyző.Sorozat}";
            lbEredmény.Content = $"Eredmény: {kiválasztottVersenyző.Eredmény}";
            Uri uri = new Uri("Images/" + kiválasztottVersenyző.Kód + ".png", UriKind.Relative);
            imgZászló.Source = new BitmapImage(uri);
        }
    }
}
