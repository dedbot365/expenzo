using expenzo.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace expenzo.Database
{
    public class TransactionDao
    {
        private readonly DatabaseContext _context;

        public TransactionDao(DatabaseContext context)
        {
            _context = context;
        }

        public void CreateTable()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Transactions (
                    TransactionId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Amount DECIMAL NOT NULL,
                    Title TEXT NOT NULL,
                    Type TEXT NOT NULL,
                    CategoryName TEXT NOT NULL,
                    TransactionDate DATETIME NOT NULL,
                    Remarks TEXT
                )";
            command.ExecuteNonQuery();
        }

        public void AddTransaction(Transaction transaction)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Transactions (Amount, Title, Type, CategoryName, TransactionDate, Remarks) VALUES (@Amount, @Title, @Type, @CategoryName, @TransactionDate, @Remarks)";
            command.Parameters.AddWithValue("@Amount", transaction.Amount);
            command.Parameters.AddWithValue("@Title", transaction.Title);
            command.Parameters.AddWithValue("@Type", transaction.Type);
            command.Parameters.AddWithValue("@CategoryName", transaction.CategoryName);
            command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
            command.Parameters.AddWithValue("@Remarks", transaction.Remarks);
            command.ExecuteNonQuery();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Transactions SET Amount = @Amount, Title = @Title, Type = @Type, CategoryName = @CategoryName, TransactionDate = @TransactionDate, Remarks = @Remarks WHERE TransactionId = @TransactionId";
            command.Parameters.AddWithValue("@Amount", transaction.Amount);
            command.Parameters.AddWithValue("@Title", transaction.Title);
            command.Parameters.AddWithValue("@Type", transaction.Type);
            command.Parameters.AddWithValue("@CategoryName", transaction.CategoryName);
            command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
            command.Parameters.AddWithValue("@Remarks", transaction.Remarks);
            command.Parameters.AddWithValue("@TransactionId", transaction.TransactionId);
            command.ExecuteNonQuery();
        }

        public void DeleteTransaction(int transactionId)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Transactions WHERE TransactionId = @TransactionId";
            command.Parameters.AddWithValue("@TransactionId", transactionId);
            command.ExecuteNonQuery();
        }

        public List<Transaction> GetTransactions()
        {
            var transactions = new List<Transaction>();
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT TransactionId, Amount, Title, Type, CategoryName, TransactionDate, Remarks FROM Transactions";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                transactions.Add(new Transaction
                {
                    TransactionId = reader.GetInt32(0),
                    Amount = reader.GetDecimal(1),
                    Title = reader.GetString(2),
                    Type = reader.GetString(3),
                    CategoryName = reader.GetString(4),
                    TransactionDate = reader.GetDateTime(5),
                    Remarks = reader.GetString(6)
                });
            }
            return transactions;
        }

        public decimal GetTotalIncomeAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT SUM(Amount) FROM Transactions WHERE Type = 'Income'";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetTotalExpenseAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT SUM(Amount) FROM Transactions WHERE Type = 'Expense'";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetHighestIncomeAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(Amount) FROM Transactions WHERE Type = 'Income'";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetLowestIncomeAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT MIN(Amount) FROM Transactions WHERE Type = 'Income'";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetHighestExpenseAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(Amount) FROM Transactions WHERE Type = 'Expense'";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public decimal GetLowestExpenseAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT MIN(Amount) FROM Transactions WHERE Type = 'Expense'";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }




    }
}