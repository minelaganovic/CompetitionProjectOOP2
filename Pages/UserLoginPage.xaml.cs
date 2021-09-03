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

namespace Projekat_WPF.Pages
{
    /// <summary>
    /// Interaction logic for UserLoginPage.xaml
    /// </summary>
    public partial class UserLoginPage : Page
    {
        public UserLoginPage()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void btnStart1_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Login();
        }
    }
}
