using expenzo.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace expenzo.Database
{
    public class DebtDao
    {
        private readonly DatabaseContext _context;

        public DebtDao(DatabaseContext context)
        {
            _context = context;
        }

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

        public void UpdateDebt(Debt debt)
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Debts SET DebtAmount = @DebtAmount, RemainingAmount = @RemainingAmount, PaidAmount = @PaidAmount, DebtStatus = @DebtStatus WHERE DebtId = @DebtId";
            command.Parameters.AddWithValue("@DebtAmount", debt.DebtAmount);
            command.Parameters.AddWithValue("@RemainingAmount", debt.RemainingAmount);
            command.Parameters.AddWithValue("@PaidAmount", debt.PaidAmount);
            command.Parameters.AddWithValue("@DebtStatus", debt.DebtStatus);
            command.Parameters.AddWithValue("@DebtId", debt.DebtId);
            command.ExecuteNonQuery();
        }

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

        public decimal GetTotalDebtRemainingAmount()
        {
            using var connection = _context.GetConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT SUM(RemainingAmount) FROM Debts";
            var result = command.ExecuteScalar();
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
    }
}