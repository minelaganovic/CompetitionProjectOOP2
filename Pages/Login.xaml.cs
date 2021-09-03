using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace Projekat_WPF.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public string usertype;
        public Login()
        {
            InitializeComponent();      
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Register();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Register();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            main.Content = new UserLoginPage();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {          
            if (this.textBoxUsername.Text == "")
            {
                errormessage.Text = "Please enter username!";
                textBoxUsername.Focus();
            }
           if (this.textBoxUsername.Text.Length<5)
           {
                errormessage.Text = "Username must be longer than 5 characters!.";
                textBoxUsername.Select(0, textBoxUsername.Text.Length);
                textBoxUsername.Focus();
            }
             if (this.textBoxUsername.Text.Length == 0 && this.txtPassword.Password.Length == 0)
            {
                errormessage.Text = "All field is required!";
            }            
            string username= textBoxUsername.Text.ToString();
            string password = txtPassword.Password.ToString();
            con.Open();
            string qry = " select * from UserLogin  where username='" +username+ "' and password='" +password+ "'";
            string title= username +"";
            SqlDataAdapter sda = new SqlDataAdapter(qry,con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1 )
            {
                usertype = dt.Rows[0][3].ToString().Trim();
                if(usertype=="A")
                {
                    MessageBox.Show("Valid user..." + username);
                    main.Content = new AdminPage();

                } else if(usertype == "S")
                {
                    MessageBox.Show("Valid user..." + username);
                    main.Content = new SchoolPage(title);
                }
                else
                {
                    MessageBox.Show("Valid user..." + username);
                    main.Content = new CompetitorsPage(title);
                }
            }
            else
            {
                MessageBox.Show("Invalid user..." + username);
                con.Close();
            }
            }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = txtPassword.Password;
            con.Open();
            string qry = " select * from UserLogin where username='" + username + "' and password='" + password + "'";
            string title = username + "";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                MessageBox.Show("Success.....");
                main.Content = new ChangePassword(title);
            }
            else
            {
                MessageBox.Show("Not Match");
            }
            dr.Close();
            con.Close();
        }
    }
    }

