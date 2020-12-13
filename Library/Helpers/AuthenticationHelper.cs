using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Helpers
{
    public class AuthenticationHelper : Database
    {
        public bool adminAuthentication(string email, string pincode)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            dynamic result = db.Query("Users")
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

            dynamic result = db.Query("Users")
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

        public IDictionary<string, object> getUserAdmin(string email, string pincode)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            dynamic result = db.Query("Users")
                .Where("email", "=", email)
                .Where("pincode", "=", pincode)
                .Where("role", "=", 1)
                .First();

            return ((IDictionary<string, object>)result);
        }
    }
}
