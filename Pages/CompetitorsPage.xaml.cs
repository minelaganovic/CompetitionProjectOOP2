
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
    /// Interaction logic for CompetitorsPage.xaml
    /// </summary>
    public partial class CompetitorsPage : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public CompetitorsPage(string title)
        {
            InitializeComponent();
            this.txtLoginN.Content = $"{title}";
            Title = title;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Login();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        void fill_listBox()//prikaz rasporeda sedenja
        {
            try
            {
                con.Open();
                string querry = "Select * from Competitors where username='" + this.txtLoginN.Content + "';";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string SitingArea = dr.GetString(13);
                    txtListBox.Items.Add("       "+SitingArea);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fill_listBox();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new CompetitorsOverviewTeam(Title);
            ItemSchedule.Background = Brushes.Transparent;
            ItemTeam.Background = Brushes.White;
        }
        private void ItemSchedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new CompetitorsPage(Title);
        }
    }
}
