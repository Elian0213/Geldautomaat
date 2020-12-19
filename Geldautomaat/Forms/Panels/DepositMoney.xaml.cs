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

namespace Geldautomaat.Forms.Panels
{
    /// <summary>
    /// Interaction logic for DepositMoney.xaml
    /// </summary>
    public partial class DepositMoney : Window
    {
        UserModel user = new UserModel();
        UserController userController = new UserController();

        public DepositMoney(UserModel currentUser)
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

        private void depostMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if input is a valid decimal
            if (!decimal.TryParse(depositAmountInput.Text, out _) || decimal.Parse(depositAmountInput.Text) < 0)
            {
                MessageBox.Show("Ingevulde bedrag is ongeldig!", "Validatie fout");
                return;
            }

            userController.depostMoney(new TransactionModelStore
            {
                Type = "deposit",
                Amount = Decimal.Parse(depositAmountInput.Text),
                Created_at = DateTime.Now.ToString("yyyy-M-dd"),
                Users_id = this.user.Id,
            });

            user.Balance = user.Balance + Decimal.Parse(depositAmountInput.Text);

            userController.updateBalance(user);

            //Update view of balance
            balanceText.Text = "€ " + this.user.Balance.ToString();

            depositAmountInput.Text = "";
            MessageBox.Show("Geld is gestort!");
        }
    }
}
