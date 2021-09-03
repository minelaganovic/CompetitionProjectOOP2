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
    /// Interaction logic for SeatingSchedule.xaml
    /// </summary>
    public partial class SeatingSchedule : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public SeatingSchedule()
        {
            InitializeComponent();
            BindDataGridMembers();
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

        private void ItemSchedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new SeatingSchedule();
        }
        private void ItemsAccount_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Content = new OverviewTeams();
        }
        void BindDataGridMembers()
        {
            con.Open();
            string querry = "Select Team.Teamname,Competitors.firstname,Competitors.NameOfSchool,Competitors.lastname,Competitors.SitingArea,Competitors.IdC,Competitors.Class,Competitors.Notes from Team inner join Members on Team.TeamId=Members.TeamId inner join Competitors on Members.IdC=Competitors.IdC";
            SqlCommand cmd1 = new SqlCommand(querry, con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataGridMembers.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);
            con.Close();
        }

        private void DataGridMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;

                    if (grid != null && grid.SelectedItem != null && grid.SelectedItems.Count == 1)
                    {
                        DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        DataRowView dr = (DataRowView)dgr.Item;

                        txtIdM.Text = dr[5].ToString().Trim();
                        txtClass.Text = dr[6].ToString();
                        txtSkola.Text = dr[2].ToString();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Some Error" + exp.ToString());
            }
        }
        private void Button_Scheduling(object sender, RoutedEventArgs e)
        {
            if (txtIdM.Text.Length != -1 && txtClass.Text.Length != 0)
            {
                try
                {
                    string IdM = txtIdM.Text.ToString();
                    string Notes = txtClass.Text.ToString();
                    string Place = txtBrMesta.Text.ToString();
                    string che = "select count(*) from Competitors where SitingArea='" + txtBrMesta.Text + "' And NameOfSchool='" + txtSkola.Text + "' And Class='" + txtClass.Text + "';";
                    string qry = "update  Competitors set SitingArea='" + Place + "' where IdC='" +IdM + "'";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    SqlCommand cmda = new SqlCommand(che, con);
                    int count = (int)cmda.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("This place already exist!");
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Place area create!");
                    }
                    con.Close();
                    BindDataGridMembers();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
