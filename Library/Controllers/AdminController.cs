using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;

namespace Library.Controllers
{
    public class AdminController : Database
    {
        public IEnumerable<UserModel> getAllUsers(string searchTerm)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            if (searchTerm == "")
            {
                IEnumerable<UserModel> allUsers = db.Query("users").Get<UserModel>();

                return allUsers;
            }

            IEnumerable<UserModel> result = db.Query("users").Where(q =>
                    q.Where("first_name", "like", "%"+ searchTerm + "%")
                    .OrWhere("last_name", "like", "%" + searchTerm + "%")
                    .OrWhere("email", "like", "%" + searchTerm + "%")
                ).Get<UserModel>();

            return result;
        }

        public string blockActionUser(string userID)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            IDictionary<string, object> user = (IDictionary<string, object>)db.Query("users")
                .Where("id", userID)
                .First();

            db.Query("users").Where("id", userID).Update(new
            {
                blocked = (bool)user["blocked"] == true ? 0 : 1
            });

            var word = (bool)user["blocked"] == true ? "gedeblokkeerd" : "geblokkeerd";

            return user["first_name"] + " " + user["last_name"] + " is nu " + word + "!";
        }

        public void storeUser(UserModel user)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            db.Query("users").Insert(user);
        }

        public void updateUser(int userID, UserModel user)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            db.Query("users")
                .Where("id", userID)
                .Update(user);
        }

        public UserModel getUser(int id)
        {
            var db = new QueryFactory(connection, new MySqlCompiler());

            return db.Query("users").Where("id", id).First<UserModel>();
        }
    }
}
