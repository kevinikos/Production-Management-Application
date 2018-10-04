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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace manage_app
{
    /// <summary>
    /// Interaction logic for PageSymulacja.xaml
    /// </summary>
    public partial class PageSymulacja : Page
    {
        public PageSymulacja()
        {
            InitializeComponent();
            OdswiezanieSymulacjiG();
            Uzupelnij_ComboBox();
        }

        private void btnOdswiezSymulacjeG_Click(object sender, RoutedEventArgs e)
        {
            OdswiezanieSymulacjiG();
            MessageBox.Show("Pomyślnie odświeżono symulacje");
        }

        private void OdswiezanieSymulacjiG()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_sym, opis, data_w, user_w, status FROM t_SymulacjaG", sqlCon);
                sqlDA.Fill(dt);
                dg1.ItemsSource = dt.DefaultView;
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

        private void btnDodajSymulacjeG_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("INSERT INTO t_SymulacjaG (opis, data_w, user_w) VALUES ('" + txtOpisSymulacjiG.Text + "','" + txtDataUtworzeniaG.Text + "','" + txtUzytkownikG.Text + "')", sqlCon);
                sqlDA.Fill(dt);
                MessageBox.Show("Pomyślnie dodano symulację");
                txtOpisSymulacjiG.Text = "";
                txtUzytkownikG.Text = "";
                txtDataUtworzeniaG.Text = "";
                OdswiezanieSymulacjiG(); //odswiezenie grida 
                Odswiez_ComboBox(); //odswiezenie comboboxa

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

        private void Uzupelnij_ComboBox()
        {
            Odswiez_ComboBox();
        }

        private void btnUsunSymulacjeG_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("DELETE FROM t_SymulacjaG WHERE id_sym='" + ComboBoxIDSym.Text + "'", sqlCon);
                sqlDA.Fill(dt);
                MessageBox.Show("Pomyślnie usunięto symulację");
                ComboBoxIDSym.Text = "";
                OdswiezanieSymulacjiG();
                Odswiez_ComboBox();

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

        private void Odswiez_ComboBox()
        {
            ComboBoxIDSym.Items.Clear(); //czyszczenie comboboxa przed ponownym uzupelnieniem
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_sym FROM t_SymulacjaG", sqlCon);
                sqlDA.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    ComboBoxIDSym.Items.Add(dr["id_sym"].ToString());
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
