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

namespace playfairGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtChanged(object sender, TextChangedEventArgs e)
        {
            cimke.Foreground = Brushes.Green;

            // MessageBox.Show(textBox.Text);

            string[] m = textBox.Text.Split();
            foreach (var s in m) {
                
                // MessageBox.Show(s);
                if( s.Length != 2 )
                {
                    cimke.Foreground = Brushes.Red;
                    return;
                }
            }

            foreach( var s in m)
            {
                if( s[0] < 'A' || s[0] > 'Z' || s[1] < 'A' || s[1] > 'Z' )
                {
                    cimke.Foreground = Brushes.Blue;
                    // MessageBox.Show(s);
                    return;
                }
            }

            foreach( var s in m )
            {
                if( s[0] == s[1] )
                {
                    cimke.Foreground = Brushes.Magenta;
                    return;
                }
            }
        }
    }
}
