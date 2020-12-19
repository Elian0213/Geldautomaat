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
using System.Windows.Threading;
using Library.Models;
using Library.Controllers;

namespace Geldautomaat.Forms.Panels.Withdraw
{
    public partial class ConfirmationWithdraw : Window
    {
        UserModel user = new UserModel();

        UserController userController = new UserController();

        int amount = 0;

        public ConfirmationWithdraw(UserModel currentUser, int amount)
        {
            this.user = currentUser;
            this.amount = amount;

            InitializeComponent();

            balanceText.Text = "€ " + this.user.Balance.ToString();
            fullnameText.Text = this.user.First_name + " " + this.user.Last_name;

            confirmMessage.Content = "Weet je zeker dat je € " + decimal.Parse(amount.ToString()) + " wilt opnemen?";

            this.startClock();
        }

        void startClock()
        {
            DispatcherTimer timer = new DispatcherTimer();

            string date = DateTime.UtcNow.ToShortDateString();

            dateText.Text = date;
            clockText.Text = DateTime.Now.ToLongTimeString();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            clockText.Text = DateTime.Now.ToLongTimeString();
        }

        private void withdrawContinueButton_Click(object sender, RoutedEventArgs e)
        {
            userController.depostMoney(new TransactionModelStore
            {
                Type = "withdraw",
                Amount = Decimal.Parse(this.amount.ToString()),
                Created_at = DateTime.Now.ToString("yyyy-M-dd"),
                Users_id = this.user.Id,
            });

            user.Balance = user.Balance - Decimal.Parse(this.amount.ToString());
            userController.updateBalance(user);

            new Dashboard(this.user).Show();
            this.Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new Dashboard(this.user).Show();
            this.Close();
        }
    }
}
