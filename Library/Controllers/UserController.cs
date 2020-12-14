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
        public IEnumerable<TransactionModel> getLastTransactions(uint userID)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            IEnumerable<TransactionModel> transactions = db.Query("transactions")
                .Where("users_id", userID)
                .OrderByDesc("id")
                .Limit(3)
                .Get<TransactionModel>();

            return transactions;
        }
    }
}
