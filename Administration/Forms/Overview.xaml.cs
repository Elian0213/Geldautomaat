using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Library.Controllers;
using Library.Models;

namespace Administration.Forms
{
    public class UserItem
    {
        public uint Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string BlockButtonText { get; set; }
    }

    public partial class Overview : Window
    {
        IDictionary<string, object> user;

        AdminController adminController = new AdminController();

        string searchTerm = "";

        public Overview(IDictionary<string, object> userData)
        {
            this.user = userData;

            InitializeComponent();

            this.fillUserData();

            userDisplayText.Text = this.user["first_name"] + " " + this.user["last_name"];
        }

        public void fillUserData()
        {
            List<UserItem> usersTable = new List<UserItem>();

            IEnumerable<UserModel> users = adminController.getAllUsers(this.searchTerm);

            foreach (UserModel user in users.ToArray())
            {
                usersTable.Add(new UserItem
                {
                    Id = user.Id,
                    FullName = user.First_name + " " + user.Last_name,
                    Email = user.Email,
                    BlockButtonText = user.Blocked == 1 ? "Deblokkeer" : "Blokkeer"
                });
            }

            base.DataContext = usersTable;
        }

        private void SearchbarButton_Click(object sender, RoutedEventArgs e)
        {
            this.searchTerm = SearchbarInput.Text;
            this.fillUserData();
        }

        private void SearchbarInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.searchTerm = SearchbarInput.Text;
                this.fillUserData();
            }
        }

        private void openAddUserPageButton_Click(object sender, RoutedEventArgs e)
        {
            new User.CreateUser(this.user).Show();
            this.Close();
        }

        private void BlockUserButton_Click(object sender, RoutedEventArgs e)
        {
            string userID = (e.Source as Button).Uid;

            string message = adminController.blockActionUser(userID);

            MessageBox.Show(message);
            this.fillUserData();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            string userID = (e.Source as Button).Uid;

            new User.UpdateUser(this.user, Int32.Parse(userID)).Show();

            this.Close();
        }
    }
}
