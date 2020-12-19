using System;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Text;
using Library.Models;

namespace Library.Controllers
{
    public class UserController : Database
    {
        public IEnumerable<TransactionModel> getLastTransactions(string userID)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            IEnumerable<TransactionModel> transactions = db.Query("transactions")
                .Where("users_id", userID)
                .OrderByDesc("id")
                .Limit(3)
                .Get<TransactionModel>();

            return transactions;
        }

        public void depostMoney(TransactionModelStore transaction)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            db.Query("transactions").Insert(transaction);
        }

        public void updateBalance(UserModel user)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            db.Query("users")
                .Where("id", user.Id)
                .Update(user);
        }

        public int transactionsToday(uint userID)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            dynamic result = db.Query("transactions")
                .Where("users_id", "=", userID)
                .Where("type", "=", "withdraw")
                .Where("created_at", "=", DateTime.Now.ToString("yyyy-M-dd"))
                .AsCount()
                .First();

            long count = (long)((IDictionary<string, object>)result)["count"];

            return Int32.Parse(count.ToString());
        }
    }
}
