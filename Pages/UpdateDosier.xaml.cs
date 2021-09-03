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

namespace Projekat_WPF.Views
{
    /// <summary>
    /// Interaction logic for UpdateDosier.xaml
    /// </summary>
    public partial class UpdateDosier : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        public UpdateDosier()
        {
            InitializeComponent();
            fill_combo();
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
        void fill_combo()
        {
            try
            {
                con.Open();
                string querry = "select * from Competitors";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string firstname = dr.GetString(1);
                    string lastname = dr.GetString(2);
                    ComboName.Items.Add(firstname+" "+lastname);
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ComboName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                con.Open();
                string querry = "select * from Competitors";
                SqlCommand cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //string firstname = dr.GetString(1);
                    string IdC = dr[0].ToString();
                    string NameOfSchool = dr.GetString(12);

                    txtId.Text = IdC;
                    txtNameSch.Text = NameOfSchool;
                }
                con.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void Button_CreateTeam(object sender, RoutedEventArgs e)
        {
            if (ComboName.SelectedIndex != -1 && txtId.Text.Length != 0)
            {
                try
                {
                    string IdC = txtId.Text;
                    string Notes = txtNotes.Text;

                    con.Open();
                    string qry = " update  Competitors set Notes='" +Notes + "' where IdC='" +IdC + "'";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                        MessageBox.Show(i + "Update Dosie member!" );
                    con.Close();
                    Reset();
                }
                catch (System.Exception exp)
                {
                    MessageBox.Show("Error Update!" + exp.ToString());
                }
            }
            else
            {
                MessageBox.Show("Error! Field is empty");
            }
        }
        void Reset()
        {
            ComboName.SelectedIndex = -1;
            txtId.Text = "";
            txtNotes.Text = "";
            txtNameSch.Text = "";
        }

        private void CreateTeam_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          // main.Content = new SchoolPage(title);
        }
    }
}

