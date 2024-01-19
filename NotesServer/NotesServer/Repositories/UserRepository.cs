using Dapper;
using Microsoft.Data.SqlClient;
using NotesServer.Models;
using System.Data;
using NotesServer.Configuration;

namespace NotesServer.Repositories
{
    public class UserRepository : IUserRepository
    {public string toHash(string password)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            var hash = Convert.ToHexString(hashBytes);
            password = hash;
            return password;
        }

        public int Login(User user)
        {
            user.Password = toHash(user.Password);
            using (var connection = new SqlConnection(ConfigurationData.ConnectionString))
            {
                var sqlQuery = "SELECT Id FROM [dbo].[Users] WHERE Login = @Login AND Password = @Password";
                var result = connection.Query<int>(sqlQuery, new User { Login = user.Login, Password = user.Password }).SingleOrDefault();

                return result;
            }
        }

        public void Create(User user)
        {
            user.Password = toHash(user.Password);
            using (IDbConnection db = new SqlConnection(ConfigurationData.ConnectionString))
            {
                var sqlQuery = "INSERT INTO [dbo].[Users] (Login, Password) VALUES(@Login, @Password)";
                db.Execute(sqlQuery, user);
            }
        }
    }
}
