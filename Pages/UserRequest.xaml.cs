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
using System.Data;
using System.Data.SqlClient;

namespace Projekat_WPF.Pages
{
    /// <summary>
    /// Interaction logic for UserRequest.xaml
    /// </summary>
    
    public partial class UserRequest : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public UserRequest()
        {
            InitializeComponent();
            BinddataGrid();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Login();
        }
        private void ItemRequest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new UserRequest();
        }

        private void ItemReview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new AdminPage();
        }
        private void ItemsAccount_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new OverviewTeams();
        }
        private void ItemSchedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new SeatingSchedule();
        }
        private void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Length != 0 && txtFirstname.Text.Length != 0 && txtLastname.Text.Length != 0 && txtEmail.Text.Length != 0 && txtPassword.Text.Length != 0 && txtBoxJmbg.Text.Length != 0 && txtNameSchool.Text.Length != 0 && txtPhone.Text.Length != 0 && txtPlace.Text.Length != 0)
            { 
                try
                {
                string firstname = txtFirstname.Text;
                string username = txtUsername.Text;
                string lastname = txtLastname.Text;
                string email = txtEmail.Text;
                string password = txtPassword.Text;
                long JMBG = Int64.Parse(txtBoxJmbg.Text);
                string Gender = TextBox1.Text;
                string CountryOfBirth = txtBoxCountry.Text;
                string DateOfBirth = txtBoxDate.Text;
                string PlaceOfBirth = txtPlace.Text;
                long Phonenumber = Int64.Parse(txtPhone.Text);
                string Class = TextBox2.Text;
                string NameOfSchool = txtNameSchool.Text;
                string che = "select count(*) from Competitors where username='" + txtUsername.Text + "';";
                string qry = "insert into Competitors (firstname,lastname,email,JMBG,Gender,CountryOfBirth,DateOfBirth,PlaceOfBirth,Phonenumber,Class,NameOfSchool,username,password) values ('"+firstname +"','"+lastname +"','"+email +"','"+JMBG +"','"+Gender +"','"+CountryOfBirth +"','"+DateOfBirth +"','"+ PlaceOfBirth +"','"+Phonenumber +"','"+Class +"','"+NameOfSchool + "','" + username + "','" + password + "')";
                string querry = "insert into UserLogin (username,password) values ('"+username +"','"+password +"')";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlCommand cmd1 = new SqlCommand(querry, con);
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
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("User is Added!");
                        Reset();
                        BinddataGrid();
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
            else
            {
                MessageBox.Show("Error! All field is required!");
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if ( txtUsername.Text.Length != 0 && txtPassword.Text.Length != 0 )
            {
                try
                {
                    string firstname = txtFirstname.Text;
                    string lastname = txtLastname.Text;
                    string username = txtUsername.Text;
                    string email = txtEmail.Text;
                    string password = txtPassword.Text;
                    long JMBG = Int64.Parse(txtBoxJmbg.Text);
                    string Gender = TextBox1.Text;
                    string CountryOfBirth = txtBoxCountry.Text;
                    string DateOfBirth = txtBoxDate.Text;
                    string PlaceOfBirth = txtPlace.Text;
                    long Phonenumber = Int64.Parse(txtPhone.Text);
                    string Class = TextBox2.Text;
                    string NameOfSchool = txtNameSchool.Text;
                    string che = "select count(*) from Competitors where username='" + username + "';";
                    string qry = "delete from UserRegister where username='" + username + "'";
                    string query = "insert into Competitors (firstname,lastname,email,JMBG,Gender,CountryOfBirth,DateOfBirth,PlaceOfBirth,Phonenumber,Class,NameOfSchool,username,password) values ('" + firstname + "','" + lastname + "','" + email + "','" + JMBG + "','" + Gender + "','" + CountryOfBirth + "','" + DateOfBirth + "','" + PlaceOfBirth + "','" + Phonenumber + "','" + Class + "','" + NameOfSchool + "','" + username + "','" + password + "')";
                    string querry = "insert into UserLogin (username,password) values ('" + username + "','" + password + "')";
                    SqlCommand cmd = new SqlCommand(querry, con);
                    SqlCommand cmd2 = new SqlCommand(qry, con);
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    con.Open();
                    SqlCommand cmda = new SqlCommand(che, con);
                    int count = (int)cmda.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("This member already exist!");
                    }
                    else
                    {
                        cmd1.ExecuteNonQuery();
                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Accept Request!");
                        BinddataGrid();
                        Reset();
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
            else
            {
                MessageBox.Show("Error!");
            }

        }
        public void Reset()
        {
            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtNameSchool.Text = "";
            txtPassword.Text = "";
            txtPhone.Text = "";
            txtPlace.Text = "";
            txtBoxJmbg.Text = "";
            txtBoxDate.Text = "";
            txtBoxCountry.Text = "";
            TextBox1.Text="";
            TextBox2.Text="";

        }

        private void BinddataGrid()
        {
                con.Open();
                string querry = "select * from UserRegister";
                SqlCommand cmd1 = new SqlCommand(querry, con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable("UserRegister");
                dataAdapter.Fill(dt);
                DG1.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
                con.Close();
        }

        private void DG1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;

                    if (grid!=null && grid.SelectedItem!=null && grid.SelectedItems.Count==1)
                    {
                        DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        DataRowView dr = (DataRowView)dgr.Item;

                        txtFirstname.Text = dr[1].ToString();
                        txtLastname.Text = dr[2].ToString();
                        txtEmail.Text = dr[3].ToString();
                        txtBoxJmbg.Text = dr[4].ToString();
                        TextBox1.Text = dr[5].ToString();
                        txtBoxCountry.Text = dr[6].ToString();
                        txtBoxDate.Text = dr[7].ToString();
                        txtPlace.Text = dr[8].ToString();
                        txtPhone.Text = dr[9].ToString();
                        TextBox2.Text = dr[10].ToString();
                        txtNameSchool.Text = dr[11].ToString();
                        txtUsername.Text = dr[12].ToString();
                        txtPassword.Text = dr[13].ToString();

                    }
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show("Some Error" + exp.ToString());
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if ( txtEmail.Text.Length != 0)
            {
                try
                {
                    string username = txtUsername.Text;
                    con.Open();
                    string qry = "delete from Competitors where username='"+username +"'";
                    string querry = "delete from UserLogin where username='"+username+"'";

                    SqlCommand cmd = new SqlCommand(qry, con);
                    SqlCommand cmd1 = new SqlCommand(querry, con);
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("User is Deleted:" + username);
                    con.Close();
                    BinddataGrid();
                    Reset();
                }
                catch (System.Exception exp)
                {
                    MessageBox.Show("Error at Add User!" + exp.ToString());
                }
            }
            else
            {
                MessageBox.Show("Error! Field is required!");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.Length != 0)
            {
                try
                {
                    string username = txtUsername.Text;
                    con.Open();
                    string qry = "delete from UserRegister where username='"+username + "'";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Request is reject:" + username);
                    con.Close();
                    BinddataGrid();
                    Reset();
                }
                catch (System.Exception exp)
                {
                    MessageBox.Show("Error at Add User!" + exp.ToString());
                }
            }
            else
            {
                MessageBox.Show("Error! Field is required!");
            }
        }
    }
    }

