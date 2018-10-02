using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using manage_app.Properties;

namespace manage_app
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

        private void btnZaloguj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=logowanie; User ID=sa; Password=whatever2424");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM Uzytkownik WHERE Login=@Login AND Haslo=@Haslo";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Login", txtLogin.Text);
                sqlCmd.Parameters.AddWithValue("@Haslo", txtHaslo.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    Settings.Default.LoginUzytkownika = txtLogin.Text; //zapisanie loginu uzytkownika w pamieci
                    Settings.Default.Save();
                    PanelGlowny PG = new PanelGlowny();
                    MessageBox.Show("Pomyślnie zalogowano");
                    PG.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login lub Hasło są nieprawidłowe");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnZamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private void btnMinimalizuj_Click(object sender, RoutedEventArgs e)
        //{
        //    this.WindowState = WindowState.Minimized;
        //}
        
    }
}
