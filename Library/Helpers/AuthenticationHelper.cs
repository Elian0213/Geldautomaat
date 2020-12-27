using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;

namespace Library.Helpers
{
    public class AuthenticationHelper : Database
    {
        public bool adminAuthentication(string email, string pincode)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            dynamic result = db.Query("users")
                .Where("email", "=", email)
                .Where("pincode", "=", pincode)
                .Where("role", "=", 1)
                .AsCount()
                .First();

            dynamic count = ((IDictionary<string, object>)result)["count"];

            // Check if count is 1, because then an account exists.
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool userAuthentication(string accountNumber, string pincode)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            dynamic result = db.Query("users")
                .Where("account_number", "=", accountNumber)
                .Where("pincode", "=", pincode)
                .Where("role", "=", 0)
                .AsCount()
                .First();

            dynamic count = ((IDictionary<string, object>)result)["count"];

            // Check if count is 1, because then an account exists.
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool userIsBlocked(string accountNumberOrEmail)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            dynamic result = db.Query("users")
                .Where("account_number", "=", accountNumberOrEmail)
                .OrWhere("email", "=", accountNumberOrEmail)
                .Where("blocked", "=", 1)
                .AsCount()
                .First();

            dynamic count = ((IDictionary<string, object>)result)["count"];

            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IDictionary<string, object> getUserAdmin(string email, string pincode)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            dynamic result = db.Query("users")
                .Where("email", "=", email)
                .Where("pincode", "=", pincode)
                .Where("role", "=", 1)
                .First();

            return ((IDictionary<string, object>)result);
        }

        public UserModel getUser(string accountNumber)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            return db.Query("users").Where("account_number", accountNumber).First<UserModel>();
        }
    }
}
