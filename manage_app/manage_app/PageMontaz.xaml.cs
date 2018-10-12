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
    /// Interaction logic for PageMontaz.xaml
    /// </summary>
    public partial class PageMontaz : Page
    {
        public PageMontaz()
        {
            InitializeComponent();
            Odswiez_ComboBoxIDSym();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");

        private void Odswiez_ComboBoxIDSym()
        {
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

        private void ComboBoxIDSym_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxIDEnr.Items.Clear();
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT id_ez FROM t_Symulacja WHERE id_sym='" + ComboBoxIDSym.SelectedItem + "'", sqlCon);
                sqlDA.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    ComboBoxIDEnr.Items.Add(dr["id_ez"].ToString());
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

        private void btnSymulacja_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                if ((ComboBoxIDSym.SelectedItem != null) && (ComboBoxIDEnr.SelectedItem != null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT t_Symulacja.id_sym, t_MaterialyMaster.grupa, t_Komponenty.id_b, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, Sum([t_Komponenty].[ilosc]*[t_Lozka].[ilosc]*[t_Symulacja].[ilosc]) AS Ilość, t_MaterialyMaster.dispo " +
                        "FROM ((t_Lozka INNER JOIN t_Komponenty ON t_Lozka.id_kp = t_Komponenty.id_kp) INNER JOIN t_Symulacja ON t_Lozka.id_enr = t_Symulacja.id_ez) INNER JOIN t_MaterialyMaster ON t_Komponenty.id_b = t_MaterialyMaster.ID " +
                        "GROUP BY t_Symulacja.id_sym, t_Symulacja.id_ez, t_MaterialyMaster.grupa, t_Komponenty.id_b, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, t_MaterialyMaster.dispo " +
                        "HAVING (t_Symulacja.id_sym='" + ComboBoxIDSym.Text + "') AND (t_Symulacja.id_ez='" + ComboBoxIDEnr.Text + "') ORDER BY t_MaterialyMaster.grupa, t_Komponenty.id_b", sqlCon);
                    sqlDA.Fill(dt);
                    dg4.ItemsSource = dt.DefaultView;
                    txtPokazIDEnr.Text = ComboBoxIDEnr.Text;
                    ComboBoxIDEnr.Text = "";

                }
                else if ((ComboBoxIDSym.SelectedItem != null) && (ComboBoxIDEnr.SelectedItem == null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT t_Symulacja.id_sym, t_MaterialyMaster.grupa, t_Komponenty.id_b, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, Sum([t_Komponenty].[ilosc]*[t_Lozka].[ilosc]*[t_Symulacja].[ilosc]) AS Ilość, t_MaterialyMaster.dispo " +
                        "FROM ((t_Lozka INNER JOIN t_Komponenty ON t_Lozka.id_kp = t_Komponenty.id_kp) INNER JOIN t_Symulacja ON t_Lozka.id_enr = t_Symulacja.id_ez) INNER JOIN t_MaterialyMaster ON t_Komponenty.id_b = t_MaterialyMaster.ID " +
                        "GROUP BY t_Symulacja.id_sym, t_MaterialyMaster.grupa, t_Komponenty.id_b, t_MaterialyMaster.SAP, t_MaterialyMaster.nazwa, t_MaterialyMaster.jednostka, t_MaterialyMaster.dispo " +
                        "HAVING (t_Symulacja.id_sym='" + ComboBoxIDSym.Text + "') ORDER BY t_MaterialyMaster.grupa, t_Komponenty.id_b", sqlCon);
                    sqlDA.Fill(dt);
                    dg4.ItemsSource = dt.DefaultView;
                    txtPokazIDEnr.Text = "Wszystkie";
                }
                else
                {
                    MessageBox.Show("Wybierz numer symulacji");
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
