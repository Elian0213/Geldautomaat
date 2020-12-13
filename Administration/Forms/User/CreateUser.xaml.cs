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
using System.Windows.Shapes;
using Library.Models;
using Library.Controllers;
using Library.Helpers;

namespace Administration.Forms.User
{
    public partial class CreateUser : Window
    {
        IDictionary<string, object> user;

        AdminController adminController = new AdminController();

        public CreateUser(IDictionary<string, object> userData)
        {
            this.user = userData;

            InitializeComponent();

            userDisplayText.Text = this.user["first_name"] + " " + this.user["last_name"];
        }

        private void openOverviewPageButton_Click(object sender, RoutedEventArgs e)
        {
            new Overview(this.user).Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            this.storeUser();
        }

        public void storeUser()
        {
            UserModel userData = new UserModel()
            {
                Email = EmailInput.Text,
                Sex = GenderInput.SelectedIndex == 0 ? "male" : "female",
                Birthday = (DateTime)BirthdateInput.SelectedDate,
                Bsn_number = Int32.Parse(BsnInput.Text),
                First_name = FirstnameInput.Text,
                Last_name = LastnameInput.Text,
                Address = AddressInput.Text,
                Zipcode = ZipcodeInput.Text,
                Town = PlaceInput.Text,
                Pincode = new PasswordHelper().randomPincode(),
                Account_number = new Random().Next(10000000, 99999999),
                Balance = 0,
                Blocked = 0,
            };

            adminController.storeUser(userData);

            MessageBox.Show("Saved");
        }
    }
}
