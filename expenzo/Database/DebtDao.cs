using expenzo.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;


namespace expenzo.Database
{
    public class DebtDao
    {
        private readonly DatabaseContext _context;
        // Constructor for the DebtDao class
        public DebtDao(DatabaseContext context)
        {
            _context = context;
        }
        //Create table for the Debts
        public void CreateTable()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Debts (
                    DebtId INTEGER PRIMARY KEY AUTOINCREMENT,
                    DebtAmount DECIMAL NOT NULL,
                    RemainingAmount DECIMAL NOT NULL,
                    PaidAmount DECIMAL NOT NULL,
                    DebtTakenDate DATETIME NOT NULL,
                    DebtDueDate DATETIME NOT NULL,
                    DebtSource TEXT NOT NULL,
                    DebtStatus TEXT NOT NULL,
                    Remark TEXT NOT NULL
                )";
            command.ExecuteNonQuery();
        }
        // Add values to the debts table   
        public void AddDebt(Debt debt)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Debts (DebtAmount, RemainingAmount, PaidAmount, DebtTakenDate, DebtDueDate, DebtSource, DebtStatus, Remark) VALUES (@DebtAmount, @RemainingAmount, @PaidAmount, @DebtTakenDate, @DebtDueDate, @DebtSource, @DebtStatus, @Remark)";
            command.Parameters.AddWithValue("@DebtAmount", debt.DebtAmount);
            command.Parameters.AddWithValue("@RemainingAmount", debt.RemainingAmount);
            command.Parameters.AddWithValue("@PaidAmount", debt.PaidAmount);
            command.Parameters.AddWithValue("@DebtTakenDate", debt.DebtTakenDate);
            command.Parameters.AddWithValue("@DebtDueDate", debt.DebtDueDate);
            command.Parameters.AddWithValue("@DebtSource", debt.DebtSource);
            command.Parameters.AddWithValue("@DebtStatus", debt.DebtStatus);
            command.Parameters.AddWithValue("@Remark", debt.Remark);
            command.ExecuteNonQuery();
        }

        // Update the values in the debts table
        public void UpdateDebt(Debt debt)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Debts SET DebtAmount = @DebtAmount, RemainingAmount = @RemainingAmount, PaidAmount = @PaidAmount, DebtStatus = @DebtStatus, DebtDueDate = @DebtDueDate, DebtSource = @DebtSource, Remark = @Remark WHERE DebtId = @DebtId";
            command.Parameters.AddWithValue("@DebtAmount", debt.DebtAmount);
            command.Parameters.AddWithValue("@RemainingAmount", debt.RemainingAmount);
            command.Parameters.AddWithValue("@PaidAmount", debt.PaidAmount);
            command.Parameters.AddWithValue("@DebtStatus", debt.DebtStatus);
            command.Parameters.AddWithValue("@DebtDueDate", debt.DebtDueDate);
            command.Parameters.AddWithValue("@DebtSource", debt.DebtSource);
            command.Parameters.AddWithValue("@Remark", debt.Remark);
            command.Parameters.AddWithValue("@DebtId", debt.DebtId);
            command.ExecuteNonQuery();
        }
        // Get the values from the debts table
        public List<Debt> GetDebts()
        {
            var debts = new List<Debt>();
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT DebtId, DebtAmount, RemainingAmount, PaidAmount, DebtTakenDate, DebtDueDate, DebtSource, DebtStatus, Remark FROM Debts";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                debts.Add(new Debt
                {
                    DebtId = reader.GetInt32(0),
                    DebtAmount = reader.GetDecimal(1),
                    RemainingAmount = reader.GetDecimal(2),
                    PaidAmount = reader.GetDecimal(3),
                    DebtTakenDate = reader.GetDateTime(4),
                    DebtDueDate = reader.GetDateTime(5),
                    DebtSource = reader.GetString(6),
                    DebtStatus = reader.GetString(7),
                    Remark = reader.GetString(8)
                });
            }
            return debts;
        }
        // Get the total debt remaining amount
        public decimal GetTotalDebtRemainingAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT SUM(RemainingAmount) FROM Debts";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
        // Get the total paid amount
        public decimal GetTotalPaidAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT SUM(PaidAmount) FROM Debts";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
        // Get the highest debt amount
        public decimal GetHighestDebtAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(DebtAmount) FROM Debts";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
        // Get the lowest debt amount
        public decimal GetLowestDebtAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT MIN(DebtAmount) FROM Debts";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
        // Get the total debt count
        public int GetTotalDebtCount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Debts";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }
        // Delete the debt from the debts table
        public void DeleteDebt(int debtId)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Debts WHERE DebtId = @DebtId";
            command.Parameters.AddWithValue("@DebtId", debtId);
            command.ExecuteNonQuery();
        }
        // Get the top 5 upcoming debts
        public List<Debt> GetTop5UpcomingDebts(int count)
        {
            var debts = new List<Debt>();
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Remark, RemainingAmount, PaidAmount, DebtDueDate FROM Debts WHERE DebtStatus = 'PendingDebt' ORDER BY DebtDueDate ASC LIMIT @Count";
            command.Parameters.AddWithValue("@Count", count);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                debts.Add(new Debt
                {
                    Remark = reader.GetString(0),
                    RemainingAmount = reader.GetDecimal(1),
                    PaidAmount = reader.GetDecimal(2),
                    DebtDueDate = reader.GetDateTime(3)
                });
            }
            return debts;
        }

    }
}