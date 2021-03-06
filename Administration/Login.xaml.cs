﻿using System;
using System.Windows;
using Library.Helpers;

namespace Administration
{
    public partial class MainWindow : Window
    {
        PasswordHelper passwordHelper = new PasswordHelper();
        AuthenticationHelper authenticationHelper = new AuthenticationHelper();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // Turn pincode text into sha256
            var pincode = passwordHelper.ComputeSha256Hash(pincodeInput.Text);

            if (authenticationHelper.adminAuthentication(emailInput.Text, pincode))
            {
                if (!authenticationHelper.userIsBlocked(emailInput.Text))
                {
                    // Get user information to pass on.
                    var user = authenticationHelper.getUserAdmin(emailInput.Text, pincode);

                    // Login successful! Open overview panel!
                    new Forms.Overview(user).Show();
                    this.Close();
                } else
                {
                    MessageBox.Show("Account is geblokkeerd", "Error");
                }
            } else
            {
                MessageBox.Show("Ongeldig E-Mail adres of pincode!", "Inlog fout");
            }
        }
    }
}
