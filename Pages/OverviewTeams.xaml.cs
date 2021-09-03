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
using System.Configuration;

namespace Projekat_WPF.Pages
{
    /// <summary>
    /// Interaction logic for OverviewTeams.xaml
    /// </summary>
    public partial class OverviewTeams : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public OverviewTeams()
        {
            InitializeComponent();
            BinddataGMembers();
        }
        private void BinddataGMembers()
        {
            con.Open();
            string querry = "Select Team.Teamname,Competitors.firstname,Competitors.lastname, Competitors.NameOfSchool from Team inner join Members on Team.TeamId=Members.TeamId inner join Competitors on Members.IdC=Competitors.IdC";
            SqlCommand cmd1 = new SqlCommand(querry, con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataGridTeam.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);
            con.Close();
        }
        private void Button_Show(object sender, RoutedEventArgs e)
        {
            string qry = "Select Count(*) from Team where Schoolname='" + txtSchool.Text + "' ";
            con.Open();
            SqlCommand cmda = new SqlCommand(qry, con);
            int count = (int)cmda.ExecuteScalar();
            MessageBox.Show("Number of team: " +count);
            con.Close();
        }
        private void ItemsAccount_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new OverviewTeams();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            main.Content = new UserLoginPage();
        }
        private void ItemRequest_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new UserRequest();
        }

        private void ItemReview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new AdminPage();
        }

        private void ItemSchedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new SeatingSchedule();
        }
    }
}
