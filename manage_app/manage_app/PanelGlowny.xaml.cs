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
    }
}
