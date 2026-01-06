using SaloonApp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace SaloonApp.Services
{
    public class UserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        //public static string Hash(string password)
        //{
        //    using var sha = SHA256.Create();
        //    return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        //}

        public void Register(string username, string password, string role)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("AddUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PasswordHash", password);   // <-- FIXED
            cmd.Parameters.AddWithValue("@Role", role);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public UserAccount GetUser(string username)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetUserByUsername", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);

            con.Open();
            using var r = cmd.ExecuteReader();

            if (!r.Read()) return null;

            return new UserAccount
            {
                UserId = (int)r["UserId"],
                Username = r["Username"].ToString(),
                PasswordHash = r["PasswordHash"].ToString(),
                Role = r["Role"].ToString()
            };
        }
    }
}
