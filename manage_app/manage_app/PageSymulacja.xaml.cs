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
            //symulacja calosciowa
            OdswiezanieSymulacjiG();
            Odswiez_ComboBoxIDSym();
            //symulacja szczegolowa
            OdswiezanieSymulacji();
            OdswiezanieLozka();
            Odswiez_ComboBoxIDEnr();
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
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_sym as IDSym , opis as OpisSymulacji, data_w as DataUtworzenia, user_w as Użytkownik, status as Status FROM t_SymulacjaG", sqlCon);
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
                SqlDataAdapter sqlDA = new SqlDataAdapter("declare @max int; select @max = max(id_sym) from t_SymulacjaG; dbcc checkident(t_SymulacjaG ,reseed, @max); INSERT INTO t_SymulacjaG (opis, data_w, user_w) VALUES ('" + txtOpisSymulacjiG.Text + "','" + txtDataUtworzeniaG.Text + "','" + txtUzytkownikG.Text + "')", sqlCon);
                sqlDA.Fill(dt);
                MessageBox.Show("Pomyślnie dodano symulację");
                txtOpisSymulacjiG.Text = "";
                txtUzytkownikG.Text = "";
                txtDataUtworzeniaG.Text = "";
                OdswiezanieSymulacjiG(); //odswiezenie grida 
                Odswiez_ComboBoxIDSym(); //odswiezenie comboboxa

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

        private void Odswiez_ComboBoxIDSym()
        {
            ComboBoxIDSym.Items.Clear(); //czyszczenie comboboxa przed ponownym uzupelnieniem
            ComboBoxIDSym2.Items.Clear();
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
                    ComboBoxIDSym2.Items.Add(dr["id_sym"].ToString());
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
                Odswiez_ComboBoxIDSym();
                OdswiezanieSymulacji();

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

        private void btnOdswiezLozka_Click(object sender, RoutedEventArgs e)
        {
            OdswiezanieLozka();
            MessageBox.Show("Pomyślnie odświeżono łóżka");
        }

        private void OdswiezanieLozka()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_enr as IDLozka, opis as OpisLozka FROM t_LozkaG", sqlCon);
                sqlDA.Fill(dt);
                dg3.ItemsSource = dt.DefaultView;
                Odswiez_ComboBoxIDEnr();

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

        private void Odswiez_ComboBoxIDEnr()
        {
            ComboBoxIDEnr.Items.Clear(); //czyszczenie comboboxa przed ponownym uzupelnieniem
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_enr FROM t_LozkaG", sqlCon);
                sqlDA.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    ComboBoxIDEnr.Items.Add(dr["id_enr"].ToString());
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

        private void btnZnajdzLozko_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                if (ComboBoxIDEnr.SelectedItem != null)
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_enr as IDLozka, opis as OpisLozka FROM t_LozkaG WHERE id_enr='" + ComboBoxIDEnr.Text + "'", sqlCon);
                    sqlDA.Fill(dt);
                    dg3.ItemsSource = dt.DefaultView;
                    Odswiez_ComboBoxIDEnr();
                }
                else
                {
                    MessageBox.Show("Uzupełnij brakujące pole");
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

        private void btnOdswiezSymulacje_Click(object sender, RoutedEventArgs e)
        {
            OdswiezanieSymulacji();
            MessageBox.Show("Pomyślnie odświeżono symulacje");
        }

        private void OdswiezanieSymulacji()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_sym as IDSym, id_ez as IDLozka, ilosc as Ilosc, status as Status FROM t_Symulacja", sqlCon);
                sqlDA.Fill(dt);
                dg2.ItemsSource = dt.DefaultView;
                Odswiez_ComboBoxIDEnr();
               
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

        private void btnZnajdzSymulacje_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

                if ((ComboBoxIDSym2.SelectedItem != null) && (ComboBoxIDEnr.SelectedItem != null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_sym as IDSym, id_ez as IDLozka, ilosc as Ilosc, status as Status FROM t_Symulacja WHERE id_sym='" + ComboBoxIDSym2.Text + "' AND id_ez='" + ComboBoxIDEnr.Text + "'", sqlCon);
                    sqlDA.Fill(dt);
                    dg2.ItemsSource = dt.DefaultView;
                    Odswiez_ComboBoxIDEnr();
                }
                else if ((ComboBoxIDSym2.SelectedItem != null) && (ComboBoxIDEnr.SelectedItem == null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_sym as IDSym, id_ez as IDLozka, ilosc as Ilosc, status as Status FROM t_Symulacja WHERE id_sym='" + ComboBoxIDSym2.Text + "'", sqlCon);
                    sqlDA.Fill(dt);
                    dg2.ItemsSource = dt.DefaultView;
                    Odswiez_ComboBoxIDEnr();
                    //Odswiez_ComboBoxIDSym();
                }
                else
                {
                    MessageBox.Show("Uzupełnij brakujące pole");
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

        private void btnDodajSymulacje_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("INSERT INTO t_Symulacja (id_sym, id_ez, ilosc, status) VALUES ('" + ComboBoxIDSym2.Text + "','" + ComboBoxIDEnr.Text + "','" + txtIloscLozek.Text + "','" + 0 + "')", sqlCon);
                sqlDA.Fill(dt);
                MessageBox.Show("Pomyślnie dodano symulację");
                txtIloscLozek.Text = "";
                Odswiez_ComboBoxIDEnr();
                //Odswiez_ComboBoxIDSym();

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
