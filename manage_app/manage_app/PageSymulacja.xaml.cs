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
        }

        private void btnWszystkieSymulacje_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=BazaTest; User ID=sa; Password=whatever2424");
            DataTable dt = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MessageBox.Show("Połączono z bazą dancyh");
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
    }
}
