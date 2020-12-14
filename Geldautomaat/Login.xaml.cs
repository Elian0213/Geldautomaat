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
using Library.Helpers;

namespace Geldautomaat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthenticationHelper authHelper = new AuthenticationHelper();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string pincode = new PasswordHelper().ComputeSha256Hash(pincodeInput.Text);
            if (authHelper.userAuthentication(AccountnumberInput.Text, pincode))
            {
                new Forms.Dashboard(authHelper.getUser(AccountnumberInput.Text)).Show();
                this.Close();
            } else
            {
                MessageBox.Show("Rekening nummer of pincode is incorrect!");
            }
        }
    }
}
