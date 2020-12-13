using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class Testcase : Database
    {
        public void Uwusnugglet()
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            // or more simpler
            IEnumerable<dynamic> users = db.Query("users").Get();

            foreach(var user in users)
            {
                Console.WriteLine(user);
            }
        }

        public bool login(string email, string pincode)
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
            } else
            {
                return false;
            }
        }
    }
}
