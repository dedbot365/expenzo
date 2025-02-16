using expenzo.Models;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace expenzo.Database
{
    public class UserDao
    {
        private readonly DatabaseContext _context;
        // Constructor for the UserDao class
        public UserDao(DatabaseContext context)
        {
            _context = context;
        }
        // Create table for the Users

        public void CreateTable()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserName TEXT NOT NULL,
                    Password TEXT NOT NULL,
                    Currency TEXT NOT NULL
                )";
            command.ExecuteNonQuery();
        }
        // Add values to the users table
        public void AddUser(User user)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (UserName, Password, Currency) VALUES (@UserName, @Password, @Currency)";
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Currency", user.Currency);
            command.ExecuteNonQuery();
        }
        // Get user by username and password
        public async Task<User> GetUser(string username, string password)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT UserId, UserName, Password, Currency FROM Users WHERE UserName = @UserName AND Password = @Password";
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@Password", password);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new User
                {
                    UserId = reader.GetInt32(0),
                    UserName = reader.GetString(1),
                    Password = reader.GetString(2),
                    Currency = reader.GetString(3)
                };
            }
            return null;
        }
        // Check if the user exists
        public async Task<bool> UserExists(string username)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(1) FROM Users WHERE UserName = @UserName";
            command.Parameters.AddWithValue("@UserName", username);
            var count = (long)await command.ExecuteScalarAsync();
            return count > 0;
        }
        // Check if any user exists
        public async Task<bool> AnyUserExists()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(1) FROM Users";
            var count = (long)await command.ExecuteScalarAsync();
            return count > 0;
        }
        // Update the values in the users table
        public async Task<bool> UpdateUser(User user) // Add this method
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Users SET UserName = @UserName, Password = @Password, Currency = @Currency WHERE UserId = @UserId";
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Currency", user.Currency);
            command.Parameters.AddWithValue("@UserId", user.UserId);
            var result = await command.ExecuteNonQueryAsync();
            return result > 0;
        }
    }
}