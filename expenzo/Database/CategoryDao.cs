using expenzo.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace expenzo.Database
{
    public class CategoryDao
    {
        private readonly DatabaseContext _context;

        public CategoryDao(DatabaseContext context)
        {
            _context = context;
        }

        public void CreateTable()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Categories (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                )";
            command.ExecuteNonQuery();
        }

        public void AddCategory(Category category)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Categories (Name) VALUES (@Name)";
            command.Parameters.AddWithValue("@Name", category.Name);
            command.ExecuteNonQuery();
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name FROM Categories";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            return categories;
        }

        public void UpdateCategory(Category category)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
            command.Parameters.AddWithValue("@Name", category.Name);
            command.Parameters.AddWithValue("@Id", category.Id);
            command.ExecuteNonQuery();
        }

        public void DeleteCategory(int categoryId)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Categories WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", categoryId);
            command.ExecuteNonQuery();
        }
    }
}