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

namespace Geldautomaat.Forms
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        UserModel user = new UserModel();

        public Dashboard(UserModel currentUser)
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

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            new Geldautomaat.MainWindow().Show();
            this.Close();
        }

        private void withdrawButton_Click(object sender, RoutedEventArgs e)
        {
            new Panels.WithdrawMoney(this.user).Show();
            this.Close();
        }

        private void depositButton_Click(object sender, RoutedEventArgs e)
        {
            new Panels.DepositMoney(this.user).Show();
            this.Close();
        }

        private void transactionHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            new Panels.TransactionHistory(this.user).Show();
            this.Close();
        }
    }
}
