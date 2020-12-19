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
    public partial class WithdrawCustomAmount : Window
    {
        UserModel user = new UserModel();

        UserController userController = new UserController();

        public WithdrawCustomAmount(UserModel currentUser)
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
            new Panels.WithdrawMoney(this.user).Show();
            this.Close();
        }

        private void withdrawMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Decimal.TryParse(withdrawAmountInput.Text, out _) || decimal.Parse(withdrawAmountInput.Text) < 0)
            {
                MessageBox.Show("Ongeldig bedrag!", "Validatie fout");
                return;
            } else if (decimal.Parse(withdrawAmountInput.Text) > 500)
            {
                MessageBox.Show("Bedrag mag niet groter 500", "Validatie fout");
                return;
            } else if (userController.transactionsToday(this.user.Id) >= 3)
            {
                MessageBox.Show("Het limiet van 3 opnamens per dag is bereikt", "Validatie fout");
                return;
            } else if (decimal.Parse(withdrawAmountInput.Text) > this.user.Balance)
            {
                MessageBox.Show("Je hebt niet genoeg saldo.", "Validatie fout");
                return;
            }

            new Withdraw.ConfirmationWithdraw(this.user, Int32.Parse(withdrawAmountInput.Text)).Show();
            this.Close();
        }
    }
}
