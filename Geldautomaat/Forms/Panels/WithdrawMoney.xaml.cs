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
using System.Windows.Threading;
using Library.Models;
using Library.Controllers;

namespace Geldautomaat.Forms.Panels
{
    /// <summary>
    /// Interaction logic for DepositMoney.xaml
    /// </summary>
    public partial class WithdrawMoney : Window
    {
        UserModel user = new UserModel();

        UserController userController = new UserController();

        public WithdrawMoney(UserModel currentUser)
        {
            this.user = currentUser;

            InitializeComponent();

            balanceText.Text = "€ " + this.user.Balance.ToString();
            fullnameText.Text = this.user.First_name + " " + this.user.Last_name;

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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new Forms.Dashboard(this.user).Show();
            this.Close();
        }

        private void moreOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            new Withdraw.WithdrawCustomAmount(this.user).Show();
            this.Close();
        }

        private void withdrawAction_Click(object sender, RoutedEventArgs e)
        {
            string amount = (e.Source as Button).Uid;

            if (userController.transactionsToday(this.user.Id) >= 3)
            {
                MessageBox.Show("Het limiet van 3 opnamens per dag is bereikt", "Validatie fout");
                return;
            } else if (decimal.Parse(amount) > this.user.Balance)
            {
                MessageBox.Show("Je hebt niet genoeg saldo.", "Validatie fout");
                return;
            }

            new Withdraw.ConfirmationWithdraw(this.user, Int32.Parse(amount)).Show();
            this.Close();
        }
    }
}
