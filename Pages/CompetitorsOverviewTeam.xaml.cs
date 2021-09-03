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

namespace Projekat_WPF.Pages
{
    /// <summary>
    /// Interaction logic for CompetitorsOverviewTeam.xaml
    /// </summary>
    public partial class CompetitorsOverviewTeam : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public CompetitorsOverviewTeam(string title)
        {
            InitializeComponent();
            Title = title;
        }
        void Fill_combobox()
        {
            try
            {
                string querry = "Select Team.Teamname from Team inner join Members on Team.TeamId=Members.TeamId inner join Competitors on Members.IdC=Competitors.IdC where Competitors.username='"+Title+"';";
                con.Open();
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string Teamname = dr.GetString(0).Trim();
                    ListTeam.Items.Add(Teamname);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void ListTeam_Loaded(object sender, RoutedEventArgs e)
        {
           Fill_combobox();
        }
        private void BinddataGrid()
        {
            string querry = "select Team.TeamId, Competitors.firstname, Competitors.lastname from Team inner join Members on Team.TeamId=Members.TeamId inner join Competitors on Members.IdC=Competitors.IdC where Team.Teamname='" + ListTeam.SelectedItem + "';";
            con.Open();
            SqlCommand cmd1 = new SqlCommand(querry, con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataGridTeam.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);
            con.Close();
        }
        private void ListTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BinddataGrid();
        }
        private void DataGridTeam_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;

                if (grid != null && grid.SelectedItem != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    DataRowView dr = (DataRowView)dgr.Item;

                    txtfirstname.Text = dr[1].ToString();
                }
            }
        }
        void FillTableDetails()
        {
                string querry = "Select Team.Teamname,Competitors.firstname,Competitors.NameOfSchool,Competitors.lastname,Competitors.CountryOfBirth,Competitors.Class from Team inner join Members on Team.TeamId=Members.TeamId inner join Competitors on Members.IdC=Competitors.IdC where Competitors.firstname='"+txtfirstname.Text+"';";
                SqlCommand cmd1 = new SqlCommand(querry, con);
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                DataGridUser.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
                con.Close();
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FillTableDetails();
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            string qry = "delete from Members from Competitors where Members.IdC=Competitors.IdC and Competitors.username='" + Title+ "';";
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("You are logged out!");
            con.Close();
            BinddataGrid();
        }
    }
}
