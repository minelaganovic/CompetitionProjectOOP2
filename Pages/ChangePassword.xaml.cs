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

namespace Projekat_WPF.Pages

{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        public ChangePassword(string title)
        {
            InitializeComponent();
            this.txtUsername.Text= $"{title}";
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Login();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnSavePas_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string novasifra = txtNewPassword.Text;
            string potvrdasifre = txtConfirmPassword.Text;
            if ( this.txtNewPassword.Text.Length == 0 && this.txtConfirmPassword.Text.Length == 0)
            {
                MessageBox.Show("Field cannot be  empty!");
            }
            else if (this.txtNewPassword.Text.Length != this.txtConfirmPassword.Text.Length)
            {
                MessageBox.Show("Passwords not macth!");
            }
            else
            { 
            con.Open();
            string qry = " update  UserLogin set password='" + novasifra + "' where username='" + username + "'";
            string query = " update  Competitors set password='" + novasifra + "' where username='" + username + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlCommand cmd1 = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Password change!");
            main.Content = new Login();
        }
        }
    }  
}
