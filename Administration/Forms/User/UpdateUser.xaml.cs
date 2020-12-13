using System;
using System.Collections.Generic;
using System.Windows;
using Library.Models;
using Library.Controllers;

namespace Administration.Forms.User
{
    public partial class UpdateUser : Window
    {
        IDictionary<string, object> user;

        AdminController adminController = new AdminController();

        int userID;

        UserModel editUserInfo = new UserModel();

        public UpdateUser(IDictionary<string, object> userData, int userID)
        {
            this.user = userData;

            this.userID = userID;

            this.editUserInfo = adminController.getUser(this.userID);

            InitializeComponent();

            this.displayInfo();

            userDisplayText.Text = this.user["first_name"] + " " + this.user["last_name"];
        }

        public void displayInfo()
        {
            UserModel user = this.editUserInfo;

            EmailInput.Text = user.Email;
            BirthdateInput.SelectedDate = user.Birthday;
            FirstnameInput.Text = user.First_name;
            AddressInput.Text = user.Address;
            PlaceInput.Text = user.Town;
            GenderInput.SelectedIndex = user.Sex == "male" ? 0 : 1;
            BsnInput.Text = user.Bsn_number.ToString();
            LastnameInput.Text = user.Last_name;
            ZipcodeInput.Text = user.Zipcode;
        }

        private void openOverviewPageButton_Click(object sender, RoutedEventArgs e)
        {
            new Overview(this.user).Show();
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.storeUser();
            } catch
            {
                MessageBox.Show("Niet alle velden zijn ingevuld!");
            }
        }

        public void storeUser()
        {
            UserModel user = this.editUserInfo;

            user.Email = EmailInput.Text;
            user.Sex = GenderInput.SelectedIndex == 0 ? "male" : "female";
            user.Birthday = (DateTime)BirthdateInput.SelectedDate;
            user.Bsn_number = Int32.Parse(BsnInput.Text);
            user.First_name = FirstnameInput.Text;
            user.Last_name = LastnameInput.Text;
            user.Address = AddressInput.Text;
            user.Zipcode = ZipcodeInput.Text;
            user.Town = PlaceInput.Text;

            adminController.updateUser(this.userID, user);

            MessageBox.Show("Opgeslagen!", "Bewerking opgeslagen!");
        }
    }
}
