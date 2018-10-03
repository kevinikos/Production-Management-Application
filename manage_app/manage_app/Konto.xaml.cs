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
using manage_app.Properties;

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
            txtLogin.Text = Settings.Default.LoginUzytkownika;
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
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT COUNT(*) FROM Uzytkownik WHERE Login='" + txtLogin.Text + "' AND Haslo='" + txtStareHaslo.Password + "'", sqlCon);
                DataTable dt = new DataTable();
                sqlDA.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    if (txtNoweHaslo.Password == txtPowtorzHaslo.Password)
                    {
                        if (txtNoweHaslo.Password.Length >= 5)
                        {
                            SqlDataAdapter sqlPASS = new SqlDataAdapter("UPDATE Uzytkownik set Haslo='" + txtNoweHaslo.Password + "'WHERE Login='" + txtLogin.Text + "'AND Haslo='" + txtStareHaslo.Password + "'", sqlCon);
                            DataTable dtpass = new DataTable();
                            sqlPASS.Fill(dtpass);
                            PanelGlowny PG = new PanelGlowny();
                            MessageBox.Show("Hasło zostało zmienione pomyślnie");
                            PG.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Hasło musi zawierać conajmniej 5 znaków");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Podane hasła nie są ze sobą zgodne");
                    }
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
    }
}
