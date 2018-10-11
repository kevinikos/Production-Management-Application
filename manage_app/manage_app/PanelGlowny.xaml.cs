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
using System.Windows.Threading;
using manage_app.Properties;

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
            ZegarStart();
            txtPokazLogin.Text = Settings.Default.LoginUzytkownika; //pobranie loginu uzytkownika z zapisanej pamieci
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

        private void ZegarStart()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            Zegar.Text = DateTime.Now.ToString(@"hh\:mm\:ss");
           // throw new NotImplementedException();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    Main.NavigationService.Navigate(new PageSymulacja());
                    break;
                case 1:
                    Main.NavigationService.Navigate(new PageProdukcja());
                    break;
                case 2:
                    Main.NavigationService.Navigate(new PageMontaz());
                    break;
                case 3:
                    Main.NavigationService.Navigate(new PageCzas());
                    break;
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TransitioningContentPanel.OnApplyTemplate();
            Panel.Margin = new Thickness(0, (100 + (50 * index)), 0, 0);
        }
    }
}
