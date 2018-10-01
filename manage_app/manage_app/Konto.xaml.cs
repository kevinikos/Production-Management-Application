using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace manage_app
{
    /// <summary>
    /// Interaction logic for Konto.xaml
    /// </summary>
    public partial class Konto : Window
    {
        public Konto()
        {
            InitializeComponent();
        }

        private void btnWyjdz_Click(object sender, RoutedEventArgs e)
        {
            PanelGlowny mw = new PanelGlowny();
            mw.Show();
            this.Close();
        }

        private void btnZmienHaslo_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=logowanie; User ID=sa; Password=whatever2424");
            try
            {
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT COUNT(*) FROM Uzytkownik WHERE Login='" + txtLogin.Text + "' AND Haslo='" + txtStareHaslo.Text + "'", sqlCon);
                DataTable dt = new DataTable();
                sqlDA.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    if (txtNoweHaslo.Text == txtPowtorzHaslo.Text)
                    {
                        SqlDataAdapter sqlPASS = new SqlDataAdapter("UPDATE Uzytkownik set Haslo='" + txtNoweHaslo.Text + "'WHERE Login='" + txtLogin.Text + "'AND Haslo='" + txtStareHaslo.Text + "'", sqlCon);
                        DataTable dtpass = new DataTable();
                        sqlPASS.Fill(dtpass);
                        MessageBox.Show("Hasło zmienione pomyślnie");
                    }
                    else
                    {
                        MessageBox.Show("Podane hasła nie są ze sobą zgodne");
                    }
                }
                else
                {
                    MessageBox.Show("Login lub Hasło jest nieprawidłowe");
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
    }
}
