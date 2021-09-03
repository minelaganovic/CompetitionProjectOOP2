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

namespace Projekat_WPF.Pages
{
    /// <summary>
    /// Interaction logic for SchoolPage.xaml
    /// </summary>
    public partial class SchoolPage : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public SchoolPage(string title)
        {
            InitializeComponent();
            Title = title;
            this.txtLoginN.Content = $"{title}";
            Add_comboTeam();
        }
        private void BinddataG()
        {            
            string querry = "select * from Competitors where NameOfSchool='" + this.txtLoginN.Content + "' and Class='"+ txtClassM.Text +"';";
            SqlCommand cmd1 = new SqlCommand(querry, con);
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataGridTeam.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);
            con.Close();
        }
        private void UserEdit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new DosierUser(Title);
            ItemCreateT.Background = Brushes.Transparent;
            ItemRewiew.Background = Brushes.White;
        }
        private void CreateTeam_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new SchoolPage(Title);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           fill_comboClass();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Login();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            main.Content = new UserLoginPage();
        }
        void fill_comboClass()
        {
            try
            {
                con.Open();
                string querry = "Select DISTINCT Class from Competitors where NameOfSchool='" + this.txtLoginN.Content + "' ORDER BY Class;";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string Class = dr.GetString(0);
                    ComboboxClass.Items.Add(Class);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        void Add_comboTeam()
        {
            try
            {
                con.Open();
                string querry = "Select * from Team where Schoolname='" + this.txtLoginN.Content + "'";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string Teamname = dr.GetString(1);
                    ComboboxTeam.Items.Add(Teamname);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void ComboboxTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string querry = "Select * from Team where Teamname='" + ComboboxTeam.SelectedItem + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Class = dr.GetString(3).Trim();
                txtClassM.Text = Class;

                string TeamId = dr.GetInt32(0).ToString().Trim();
                txtIdTeam.Text = TeamId;
            }
            con.Close();
            BinddataG();
        }
        void Reset()
        {
            ComboboxClass.SelectedIndex = -1;
            ComboboxTeam.SelectedIndex = -1;
            txtClassM.Text = "";
            txtIdM.Text = "";
            txtIdTeam.Text = "";
            txtTeamName.Text="";
        }

        private void Button_Click_CreatT(object sender, RoutedEventArgs e)
        {
            if (ComboboxClass.SelectedIndex != -1 && txtTeamName.Text.Length != 0)
            {
                try
                {
                    string Class = ComboboxClass.Text;
                    string Teamname = txtTeamName.Text;
                    string che = "select count(*) from Team where Teamname='" + txtTeamName.Text + "';";
                    string qry = "insert into Team (Teamname,Class,Schoolname) values ('" + Teamname + "','" +Class + "','"+ this.txtLoginN.Content+"')";
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
                        MessageBox.Show("Team created!");
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
                MessageBox.Show("Error! Field is empty");
            }
        }

        private void Button_Click_AddMember(object sender, RoutedEventArgs e)
        {

            if (txtIdM.Text.Length != 0 && txtClassM.Text.Length != 0 && ComboboxTeam.SelectedIndex != -1 && txtIdTeam.Text.Length != 0)
            {
                try
                {

                    string IdC = txtIdM.Text.ToString();
                    string IdTeam = txtIdTeam.Text.ToString();
                    string che = "select count(*) from Members where IdC='" + txtIdM.Text + "';";
                    string query = "select count(*) from Members where TeamId='" + txtIdTeam.Text + "';";
                    string qry = "insert into Members (IdC,TeamId) values ('" + IdC + "','" + IdTeam + "')";
                    SqlCommand cmd3 = new SqlCommand(qry, con);
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(che, con);
                    SqlCommand cmd2 = new SqlCommand(query, con);
                    int count = (int)cmd1.ExecuteScalar();
                    int countt = (int)cmd2.ExecuteScalar();
                    if (count>0)
                    {
                        MessageBox.Show("This member already exist!");

                    }
                    else if(countt<3)
                    {
                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("Member added in the team!");
                    }
                    else
                    {
                        MessageBox.Show("Team is filled!");
                    }
                    con.Close();
                    Reset();
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    if(con.State==ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Field cannot be empty!");
            }
        }

        private void DataGridTeam_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;

                if (grid != null && grid.SelectedItem != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    DataRowView dr = (DataRowView)dgr.Item;

                    txtIdM.Text = dr[0].ToString();
                }
            }
        }
    }
}
