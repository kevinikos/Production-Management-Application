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
    /// Interaction logic for PageProdukcja.xaml
    /// </summary>
    public partial class PageProdukcja : Page
    {
        public PageProdukcja()
        {
            InitializeComponent();
            Odswiez_ComboBoxIDSymProdukcja();
            //Odswiez_ComboBoxIDEnrProdukcja();
        }

        private void Odswiez_ComboBoxIDSymProdukcja()
        {
            ComboBoxIDSymProdukcja.Items.Clear(); //czyszczenie comboboxa przed ponownym uzupelnieniem
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
                    ComboBoxIDSymProdukcja.Items.Add(dr["id_sym"].ToString());
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

        private void Odswiez_ComboBoxIDEnrProdukcja()
        {
            ComboBoxIDEnrProdukcja.Items.Clear(); //czyszczenie comboboxa przed ponownym uzupelnieniem
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_ez FROM t_Symulacja WHERE id_sym='" + ComboBoxIDSymProdukcja.SelectedItem + "'", sqlCon);
                sqlDA.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    ComboBoxIDEnrProdukcja.Items.Add(dr["id_ez"].ToString());
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

        private void btnSymulacjaProdukcja_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                if ((ComboBoxIDSymProdukcja.SelectedItem != null) && (ComboBoxIDEnrProdukcja.SelectedItem != null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT t_Symulacja.id_sym, t_MaterialyMaster.grupa, t_BaugrupaMaster.ID_m, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, Sum([t_BaugrupaMaster].[ilosc]*[t_BaugrupaMaster].[dlugosc]*[t_Komponenty].[ilosc]*[t_Lozka].[ilosc]*[t_Symulacja].[ilosc]) AS Ilość, t_MaterialyMaster.dispo " +
                        "FROM t_MaterialyMaster INNER JOIN (((t_Lozka INNER JOIN t_Komponenty ON t_Lozka.id_kp = t_Komponenty.id_kp) INNER JOIN t_Symulacja ON t_Lozka.id_enr = t_Symulacja.id_ez) INNER JOIN t_BaugrupaMaster ON t_Komponenty.id_b = t_BaugrupaMaster.ID_b) ON t_MaterialyMaster.ID = t_BaugrupaMaster.ID_m " +
                        "GROUP BY t_Symulacja.id_sym, t_Symulacja.id_ez, t_MaterialyMaster.grupa, t_BaugrupaMaster.ID_m, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, t_MaterialyMaster.dispo " +
                        "HAVING (t_Symulacja.id_sym='" + ComboBoxIDSymProdukcja.Text + "') AND (t_Symulacja.id_ez='" + ComboBoxIDEnrProdukcja.Text + "') ORDER BY t_MaterialyMaster.grupa, t_BaugrupaMaster.ID_m", sqlCon);
                    sqlDA.Fill(dt);
                    dg4.ItemsSource = dt.DefaultView;
                    ComboBoxIDEnrProdukcja.Text = "";
                }
                else if ((ComboBoxIDSymProdukcja.SelectedItem != null) && (ComboBoxIDEnrProdukcja.SelectedItem == null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT t_Symulacja.id_sym, t_MaterialyMaster.grupa, t_BaugrupaMaster.ID_m, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, Sum([t_BaugrupaMaster].[ilosc]*[t_BaugrupaMaster].[dlugosc]*[t_Komponenty].[ilosc]*[t_Lozka].[ilosc]*[t_Symulacja].[ilosc]) AS Ilość, t_MaterialyMaster.dispo " +
                        "FROM t_MaterialyMaster INNER JOIN (((t_Lozka INNER JOIN t_Komponenty ON t_Lozka.id_kp = t_Komponenty.id_kp) INNER JOIN t_Symulacja ON t_Lozka.id_enr = t_Symulacja.id_ez) INNER JOIN t_BaugrupaMaster ON t_Komponenty.id_b = t_BaugrupaMaster.ID_b) ON t_MaterialyMaster.ID = t_BaugrupaMaster.ID_m " +
                        "GROUP BY t_Symulacja.id_sym, t_MaterialyMaster.grupa, t_BaugrupaMaster.ID_m, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, t_MaterialyMaster.dispo " +
                        "HAVING (t_Symulacja.id_sym='" + ComboBoxIDSymProdukcja.Text + "') ORDER BY t_MaterialyMaster.grupa, t_BaugrupaMaster.ID_m", sqlCon);

                    sqlDA.Fill(dt);
                    dg4.ItemsSource = dt.DefaultView;
                    Odswiez_ComboBoxIDEnrProdukcja();
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
