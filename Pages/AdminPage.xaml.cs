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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public AdminPage()
        {
            InitializeComponent();
            FillCombo_Name();
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
        void FillCombo_Name()
        {
            try
            {
                string querry = "Select Competitors.firstname from Competitors inner join Members on Competitors.IdC=Members.IdC";
                con.Open();
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string Firstname = dr.GetString(0);
                    txtIdM.Items.Add(Firstname);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ComboBoxId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string querry = "Select * from Competitors where firstname='" + txtIdM.SelectedItem + "'";
            SqlCommand cmd = new SqlCommand(querry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string Class = dr.GetString(10).Trim();
                txtRazred.Text = Class;
            }
            con.Close();
        }

        void ShowDataGridResult()
        {        
            con.Open();
            string querry = "Select ResultId,Date,Points,Membername, Competitors.Class from Result inner join Competitors on Result.Membername=Competitors.firstname where Competitors.Class='" + txtRazred.Text + "' order by Points DESC";
            SqlCommand cmd1 = new SqlCommand(querry, con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataGridResult.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);
            con.Close();
        }

        private void Button_AddPoints(object sender, RoutedEventArgs e)
        {
            if (txtIdM.SelectedIndex != -1 && txtPoints.Text.Length != 0 && txtBoxDate.Text.Length !=0 )
            {
                try
                {
                    string Membername = txtIdM.Text.ToString();
                    string date = txtBoxDate.Text.ToString();
                    float Points = Int64.Parse(txtPoints.Text);
                    string che = "select count(*) from Result where Membername='"+txtIdM.Text+"';";
                    string qry = "insert into Result  (Membername, Points, Date) values ('" + Membername + "','" + Points + "','" + date+ "') ";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    SqlCommand cmda = new SqlCommand(che, con);
                    int count = (int)cmda.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("This MemberId have points!");
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Result Added!");
                        con.Close();
                        ShowDataGridResult();
                    }
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
        private void Button_Click_Archived(object sender, RoutedEventArgs e)
        {
            con.Open();
            string querry = "Delete from Result where ResultId not in(Select Top 3 ResultId from Result inner join Competitors on Result.Membername=Competitors.firstname where Competitors.Class='" + txtRazred.Text + "' order by Points DESC)";
            SqlCommand cmd1 = new SqlCommand(querry, con);
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Competition archived!");
            con.Close();
            Archived();
        }

        void Archived()
        {
            con.Open();
            string querry = "Select Top 3 ResultId,Date,Points,Membername, Competitors.Class from Result inner join Competitors on Result.Membername=Competitors.firstname where Competitors.Class='" + txtRazred.Text + "' order by Points DESC";
            SqlCommand cmd1 = new SqlCommand(querry, con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataGridResult.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);
            con.Close();
        }
    }
}
//KRAJJJ, END, END, ELHAMDULILLAH