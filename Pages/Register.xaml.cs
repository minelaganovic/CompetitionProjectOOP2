using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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


namespace Projekat_WPF.Pages
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public Register()
        {
            InitializeComponent();
            Add_ComboSchool();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Login();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            main.Content = new UserLoginPage();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        void Add_ComboSchool()
        {
                con.Open();
                string querry = "Select username from UserLogin where usertype='S';";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string School = dr.GetString(0);
                    txtNameSchool.Items.Add(School);
                }
                con.Close();
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstname.Text=="" || txtLastname.Text=="" || txtEmail.Text=="" || txtBoxJmbg.Text=="" || TextBox1.SelectedIndex == -1 || TextBox2.SelectedIndex == -1 && txtNameSchool.SelectedIndex == -1 || txtPhone.Text== ""|| txtPlace.Text== ""|| txtUsername.Text == "" || txtPassword.Text == "")
            {
                errormessage.Text = "All field is required! ";
            }
            else if (txtUsername.Text.Length < 5)
            {
                errormessage.Text = "Username must be longer than 5 characters! ";
                txtUsername.Focus();
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                errormessage.Text = "Enter a valid email.";
                txtEmail.Select(0, txtEmail.Text.Length);
                txtEmail.Focus();
            }
            else if (!Regex.IsMatch(txtBoxJmbg.Text, @"^([\d])"))
            {
                errormessage.Text = "JMBG must have 13 numbers! ";
                txtBoxJmbg.Focus();
            }
            else if (txtBoxJmbg.Text.Length < 13 || txtBoxJmbg.Text.Length > 13 )
            {
                errormessage.Text = "JMBG must have 13 numbers! ";
                txtBoxJmbg.Focus();
            }
            else if (!Regex.IsMatch(txtPhone.Text, @"^([\d])"))
            {
                errormessage.Text = "Enter a valid phonenumber ";
                txtPhone.Focus();
            }
            else if (txtPhone.Text.Length < 9 || txtPhone.Text.Length > 10)
            {
                errormessage.Text = "Phonenumber must have 10 numbers! ";
                txtPhone.Focus();
            }
            else{
               try
                {
                    string firstname = txtFirstname.Text;
                    string lastname = txtLastname.Text;
                    string email = txtEmail.Text;
                    string password = txtPassword.Text;
                    long JMBG = Int64.Parse(txtBoxJmbg.Text);
                    string Gender = TextBox1.Text;
                    string CountryOfBirth = txtBoxCountry.Text;
                    string DateOfBirth = txtBoxDate.SelectedDate.ToString();
                    string PlaceOfBirth = txtPlace.Text;
                    long Phonenumber = Int64.Parse(txtPhone.Text);
                    string Class = TextBox2.Text;
                    string NameOfSchool = txtNameSchool.Text;
                    string username = txtUsername.Text;
                    string che = "select count(*) from UserRegister where username='" + username + "';";
                    string qry = "insert into UserRegister (firstname,lastname,email,JMBG,Gender,CountryOfBirth,DateOfBirth,PlaceOfBirth,Phonenumber,Class,NameOfSchool,username,password) values ('" + firstname + "','" + lastname + "','" + email + "','" + JMBG + "','" + Gender + "','" + CountryOfBirth + "','" + DateOfBirth + "','" + PlaceOfBirth + "','" + Phonenumber + "','" + Class + "','" + NameOfSchool + "','" + username + "','" + password + "')";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    SqlCommand cmda = new SqlCommand(che, con);
                    int count = (int)cmda.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("This member already exist!");
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registration request has been sent!");
                    }
                    con.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        public void readUsername()
        {
            string username = txtUsername.Text;
            string che = "select count(*) from UserLogin where username='"+username+"';";
            SqlCommand cmd = new SqlCommand(che, con);
            con.Open();
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                MessageBox.Show("Username je zauzet!");
            }
            else
            {
                MessageBox.Show("Username je slobodan!");
            }
            con.Close();
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public void Reset()
        {
            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtNameSchool.SelectedIndex = -1;
            txtPassword.Text= "";
            txtPhone.Text = "";
            txtPlace.Text = "";
            txtBoxJmbg.Text = "";
            txtBoxDate.Text = "";
            txtBoxCountry.Text = "";
            TextBox1.SelectedIndex = -1;
            TextBox2.SelectedIndex = -1;

        }

        private void txtPassword_SelectionChanged(object sender, RoutedEventArgs e)
        {
            readUsername();
        }
    }
}
