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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace manage_app
{
    /// <summary>
    /// Interaction logic for PanelGlowny.xaml
    /// </summary>
    public partial class PanelGlowny : Window
    {
        public PanelGlowny()
        {
            InitializeComponent();
            Main.NavigationService.Navigate(new PageSymulacja());
        }

        private void btnKonto_Click(object sender, RoutedEventArgs e)
        {
            Konto mw = new Konto();
            mw.Show();
            this.Close();
        }

        private void btnWyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    
        private void btnZamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ///Application.Current.Shutdown();
        }

        private void btnSymulacja_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new PageSymulacja());
        }

        private void btnProdukcja_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new PageProdukcja());
        }

        private void btnMontaz_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new PageMontaz());
        }

        private void btnCzas_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new PageCzas());
        }
    }
}
