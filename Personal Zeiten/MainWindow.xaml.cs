using Personal_Zeiten.schnuppDataSetTableAdapters;
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

namespace Personal_Zeiten
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (SqlConnection conn = new SqlConnection("server=lt-080120\\Sqlexpress01;database=schnupp; trusted_connection=yes"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select distinct kurzname from tbl_Personal " +
                    "inner join tbl_Zeiten  " +
                    "on tbl_Zeiten.pnr = tbl_Personal.pnr " +
                    "where Gruppe != 'Maschinen' and aktiv= 1", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cboDaten.Items.Add(reader[0].ToString());
                }
                reader.Close();
            }
        }
        //private void pull_Click(object sender, RoutedEventArgs e)
        //{
        //    using (SqlConnection conn = new SqlConnection("server=lt-080120\\Sqlexpress01;database=schnupp; trusted_connection=yes"))
        //    {
        //        conn.Open();
        //    SqlCommand cmd = new SqlCommand("Select distinct kurzname from tbl_Personal " +
        //        "inner join tbl_Zeiten  " +
        //        "on tbl_Zeiten.pnr = tbl_Personal.pnr " +
        //        "where Gruppe != 'Maschinen' and aktiv= 1" , conn);
            
        //        SqlDataReader reader = cmd.ExecuteReader();
                
        //        while (reader.Read())
        //        {
        //          cboDaten.Items.Add(reader[0].ToString());
        //        }
        //        reader.Close();
        //    }
            
        //}
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtbox.Text = cboDaten.SelectedItem.ToString();
            using (SqlConnection conn = new SqlConnection("server=lt-080120\\Sqlexpress01;database=schnupp; trusted_connection=yes"))
            {
                conn.Open();
                var kurzname = txtbox.Text.ToString();
                SqlCommand cmd = new SqlCommand("Select TOP 50 anfang, ende,  arbeitszeit from tbl_Personal " +
                    "inner join tbl_Zeiten  " +
                    "on tbl_Zeiten.pnr = tbl_Personal.pnr " +
                    "where kurzname='"+kurzname +"' Order BY anfang DESC ", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cboDaten.Items.Add(reader[0].ToString());
                    string zeile = reader[0].ToString()+" " + reader[1].ToString()+ " " + reader[2].ToString();
                    cboZeiten.Items.Add(zeile);
                }
                reader.Close();
            }

        }
    }
}
