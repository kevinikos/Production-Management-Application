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
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel : Window
    {
        public Panel()
        {
            InitializeComponent();
        }

        private void txtPokazDane_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-OIOAR14S\MYSQL2017; Initial Catalog=logowanie; User ID=sa; Password=whatever2424");
            DataTable datatable = new DataTable();
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MessageBox.Show("Połączono z bazą dancyh");
                }

                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM Uzytkownik", sqlCon);
                sqlDA.Fill(datatable);
                dg1.ItemsSource = datatable.DefaultView;

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
