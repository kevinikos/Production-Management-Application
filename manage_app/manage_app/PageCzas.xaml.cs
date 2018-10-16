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
    /// Interaction logic for PageCzas.xaml
    /// </summary>
    public partial class PageCzas : Page
    {
        public PageCzas()
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

        private void btnOdswiezCzasy_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtTB = new DataTable();
            DataTable dtM = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }
                if ((ComboBoxIDSym.SelectedItem != null) && (ComboBoxIDEnr.SelectedItem != null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT t_Symulacja.id_sym, k_BG_CzasyMPK.obszar, t_MPK.opis, ROUND(Sum([Czas]*[t_Symulacja].[ilosc]*[t_Lozka].[ilosc]*[t_Komponenty].[ilosc])/60,2) AS [CzasProdukcjiBG(h)] " +
                        "FROM t_Symulacja INNER JOIN ((t_Lozka INNER JOIN t_Komponenty ON t_Lozka.id_kp = t_Komponenty.id_kp) INNER JOIN (k_BG_CzasyMPK INNER JOIN t_MPK ON k_BG_CzasyMPK.mpk = t_MPK.mpk) ON t_Komponenty.id_b = k_BG_CzasyMPK.id_b) ON t_Symulacja.id_ez = t_Lozka.id_enr " +
                        "GROUP BY t_Symulacja.id_sym, t_Symulacja.id_ez, k_BG_CzasyMPK.obszar, k_BG_CzasyMPK.mpk, t_MPK.opis HAVING t_Symulacja.id_sym='" + ComboBoxIDSym.Text + "' AND t_Symulacja.id_ez='" + ComboBoxIDEnr.Text + "'ORDER BY t_Symulacja.id_sym, k_BG_CzasyMPK.obszar, k_BG_CzasyMPK.mpk;", sqlCon);
                    sqlDA.Fill(dt);
                    dg5.ItemsSource = dt.DefaultView;
                    //metoda 1- laczny czas dla 'czas produkcja'
                    double suma = 0;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        suma += Convert.ToDouble(dt.Rows[i][3].ToString());
                    }
                    txtPokazCzasBG.Text = suma.ToString() + "h";

                    //informacja dodatkowa - jaka ilosc zostala przypisana do danego modelu w symulacji
                    SqlDataAdapter sqlDATB = new SqlDataAdapter("SELECT ilosc FROM t_Symulacja WHERE (id_sym='" + ComboBoxIDSym.Text + "') AND (id_ez='" + ComboBoxIDEnr.Text + "')", sqlCon);
                    sqlDATB.Fill(dtTB);
                    txtPokazIlosc.Text = dtTB.Rows[0][0].ToString();
                    txtPokazIDEnr.Text = ComboBoxIDEnr.Text;

                    //metoda 2- laczny czas w zapytaniu dla 'czas montaz'
                    SqlDataAdapter sqlDAM = new SqlDataAdapter("SELECT ROUND(SUM(sub.[CzasMontaz(h)]),3) AS [CzasMontaz(h)] " +
                        "FROM (SELECT t_Symulacja.id_sym, k_LozkaSAP_SumaCzasMontaz.id_enr, (k_LozkaSAP_SumaCzasMontaz.czasEZ*t_Symulacja.ilosc)/60 AS [CzasMontaz(h)] " +
                        "FROM t_Symulacja INNER JOIN k_LozkaSAP_SumaCzasMontaz ON t_Symulacja.id_ez = k_LozkaSAP_SumaCzasMontaz.id_enr WHERE t_Symulacja.id_sym='" + ComboBoxIDSym.Text + "' AND t_Symulacja.id_ez='" + ComboBoxIDEnr.Text + "'" +
                        "GROUP BY t_Symulacja.id_sym, k_LozkaSAP_SumaCzasMontaz.id_enr, k_LozkaSAP_SumaCzasMontaz.czasEZ, t_Symulacja.ilosc) sub", sqlCon);
                    sqlDAM.Fill(dtM);
                    txtPokazCzasM.Text = dtM.Rows[0][0].ToString() + "h";

                    //czyszczenie dla wygody
                    ComboBoxIDEnr.Text = "";
                }

                else if ((ComboBoxIDSym.SelectedItem != null) && (ComboBoxIDEnr.SelectedItem == null))
                {
                    SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT t_Symulacja.id_sym, k_BG_CzasyMPK.obszar, t_MPK.opis, ROUND(Sum([Czas]*[t_Symulacja].[ilosc]*[t_Lozka].[ilosc]*[t_Komponenty].[ilosc])/60,2) AS [CzasProdukcjiBG(h)] " +
                        "FROM t_Symulacja INNER JOIN ((t_Lozka INNER JOIN t_Komponenty ON t_Lozka.id_kp = t_Komponenty.id_kp) INNER JOIN (k_BG_CzasyMPK INNER JOIN t_MPK ON k_BG_CzasyMPK.mpk = t_MPK.mpk) ON t_Komponenty.id_b = k_BG_CzasyMPK.id_b) ON t_Symulacja.id_ez = t_Lozka.id_enr " +
                        "GROUP BY t_Symulacja.id_sym, k_BG_CzasyMPK.obszar, k_BG_CzasyMPK.mpk, t_MPK.opis HAVING t_Symulacja.id_sym='" + ComboBoxIDSym.Text + "' ORDER BY t_Symulacja.id_sym, k_BG_CzasyMPK.obszar, k_BG_CzasyMPK.mpk;", sqlCon);
                    sqlDA.Fill(dt);
                    dg5.ItemsSource = dt.DefaultView;
                    //metoda 1- laczny czas dla 'czas produkcja'
                    double suma = 0;
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        suma += Convert.ToDouble(dt.Rows[i][3].ToString());
                    }
                    txtPokazCzasBG.Text = suma.ToString() + "h";

                    //informacja dodatkowa - jaka ilosc zostala przypisana do danego modelu w symulacji
                    SqlDataAdapter sqlDATB = new SqlDataAdapter("SELECT SUM(ilosc) FROM t_Symulacja WHERE id_sym='" + ComboBoxIDSym.Text + "'", sqlCon);
                    sqlDATB.Fill(dtTB);
                    txtPokazIlosc.Text = dtTB.Rows[0][0].ToString();
                    txtPokazIDEnr.Text = "Wszystkie";

                    //metoda 2- laczny czas w zapytaniu dla 'czas montaz'
                    SqlDataAdapter sqlDAM = new SqlDataAdapter("SELECT ROUND(SUM(sub.[CzasMontaz(h)]),3) AS [CzasMontaz(h)] " +
                        "FROM (SELECT t_Symulacja.id_sym, k_LozkaSAP_SumaCzasMontaz.id_enr, (k_LozkaSAP_SumaCzasMontaz.czasEZ*t_Symulacja.ilosc)/60 AS [CzasMontaz(h)] " +
                        "FROM t_Symulacja INNER JOIN k_LozkaSAP_SumaCzasMontaz ON t_Symulacja.id_ez = k_LozkaSAP_SumaCzasMontaz.id_enr WHERE t_Symulacja.id_sym='" + ComboBoxIDSym.Text + "'" +
                        "GROUP BY t_Symulacja.id_sym, k_LozkaSAP_SumaCzasMontaz.id_enr, k_LozkaSAP_SumaCzasMontaz.czasEZ, t_Symulacja.ilosc) sub", sqlCon);
                    sqlDAM.Fill(dtM);
                    txtPokazCzasM.Text = dtM.Rows[0][0].ToString() + "h";
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
