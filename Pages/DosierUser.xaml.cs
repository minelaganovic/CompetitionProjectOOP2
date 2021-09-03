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
    /// Interaction logic for DosierUser.xaml
    /// </summary>
    public partial class DosierUser : Page
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VG9FJSM;Initial Catalog=Takmicenje;Integrated Security=True");
        
        public DosierUser(string title)
        {
            InitializeComponent();
            Title = title;
        }

        private void BinddataG()
        {        
            string querry = "select Team.Teamname,Competitors.NameOfSchool,Competitors.IdC, Competitors.Class from Team inner join Members on Team.TeamId=Members.TeamId inner join Competitors on Members.IdC=Competitors.IdC where Competitors.NameOfSchool='" + Title+ "' And Competitors.firstname='"+txtMemberN.Text.ToString()+"';";
            SqlCommand cmd1 = new SqlCommand(querry, con);
            con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            DataGridTeam.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);
            con.Close();
        }

        private void Button_CreateNotes(object sender, RoutedEventArgs e)
        {
            if (txtNotes.Text.Length != -1 && txtMemberN.Text.Length != 0)
            {
                try
                {
                    string name = txtMemberN.Text;
                    string Notes = txtNotes.Text;
                    con.Open();
                    string qry = " update  Competitors set Notes='" + Notes + "' where firstname='" +name+ "'";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                        MessageBox.Show(i + "Update Dosier member!");
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
            txtNotes.Text = "";
            txtMemberN.Text = "";
        }

        private void Button_SearchName(object sender, RoutedEventArgs e)
        {
            BinddataG();
        }
    }
}
