using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace ADFVFXgui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<string> Kodtabla;

        public void BetoltKodtabla( string filename)
        {
            Kodtabla = new List<string>();
            StreamReader sr = new StreamReader(filename);
            while( !sr.EndOfStream )
                Kodtabla.Add(sr.ReadLine());
            sr.Close();
        }

        public void MegjelenitKodtabla()
        {
            racs.Children.Clear();
            int sor = 0;
            foreach( var i in Kodtabla )
            {
                for( int oszlop=0; oszlop < i.Length; oszlop++ )
                {
                    Label l = new Label();
                    l.Content = i[oszlop];
                    l.FontSize = 22;
                    l.Margin = new Thickness(10 + oszlop * 30, sor * 30, 0, 0);
                    racs.Children.Add(l);
                }
                ++sor;
            }
        }

        public void EllenorzesKodtabla()
        {
            lista.Items.Clear();

            if (Kodtabla.Count != 6)
                lista.Items.Add("Hiba a mátrix méretében!");
            else
            {
                foreach (var i in Kodtabla)
                    if (i.Length != 6)
                    {
                        lista.Items.Add("Hiba a mátrix méretében!");
                        break;
                    }
            }

            foreach (var i in Kodtabla)
                foreach (var j in i)
                    if (  !(j >= 'a' && j <= 'z' || j >= '0' && j <= '9') )
                        lista.Items.Add($"Hibás karakter {j} van a mátrixban.");

            /*
            int[] tomb = new int[100];
            for (int i = 0; i < 100; i++)
                tomb[i] = 0;
            tomb['k' - 'a'] = 13;
            lista.Items.Add($"{'k' - 'a'}: {tomb['k' - 'a']}");
            */

            Dictionary<char, int> tomb = new Dictionary<char, int>();

            for (char i = 'a'; i <= 'z'; i++)
                tomb.Add(i,0);

            for (char i = '0'; i <= '9'; i++)
                tomb.Add(i, 0);

            foreach (var i in Kodtabla)
                foreach (var j in i)
                    if (tomb.ContainsKey(j))
                        ++tomb[j];

            foreach (var i in tomb)
                if (i.Value > 1)
                    lista.Items.Add($"A(z) {i.Key} karakter {i.Value}x szerepel a mátrixban!");

            if (lista.Items.Count == 0)
                lista.Items.Add("A mátrix megfelelő!");
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.InitialDirectory = Directory.GetCurrentDirectory();

            if( opf.ShowDialog() == true)
            {
                // MessageBox.Show(opf.FileName);
                BetoltKodtabla(opf.FileName);
                MegjelenitKodtabla();
                EllenorzesKodtabla();
            }

        }
    }
}
