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
    public class TransactionItem
    {
        public string Amount { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Color { get; set; }
    }

    public partial class TransactionHistory : Window
    {
        UserModel user = new UserModel();

        UserController userController = new UserController();

        public TransactionHistory(UserModel currentUser)
        {
            this.user = currentUser;

            InitializeComponent();

            this.displayTransactionHistory();

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

        void displayTransactionHistory()
        {
            List<TransactionItem> transactionTable = new List<TransactionItem>();

            IEnumerable<TransactionModel> transactions = userController.getLastTransactions(this.user.Id.ToString());

            if (transactions.ToArray().Length == 0)
            {
                NoTransactionsText.Text = "Je hebt nog geen transacties uitgevoerd.";
            }

            foreach (TransactionModel transaction in transactions.ToArray())
            {
                transactionTable.Add(new TransactionItem
                {
                    Amount = "€ " + transaction.Amount,
                    Type = transaction.Type.ToString() == "deposit" ? "Gestort" : "Opgenomen",
                    Date = transaction.Created_at.ToShortDateString(),
                    Color = transaction.Type.ToString() == "deposit" ? "#4bcc83" : "#c2235b",
                });
            }

            base.DataContext = transactionTable;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new Forms.Dashboard(this.user).Show();
            this.Close();
        }
    }
}
