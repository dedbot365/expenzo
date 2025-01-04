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
            command.CommandText = "INSERT INTO Debts (DebtAmount, DebtTakenDate, DebtDueDate, DebtSource, DebtStatus, Remark) VALUES (@DebtAmount, @DebtTakenDate, @DebtDueDate, @DebtSource, @DebtStatus, @Remark)";
            command.Parameters.AddWithValue("@DebtAmount", debt.DebtAmount);
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
            command.CommandText = "UPDATE Debts SET DebtAmount = @DebtAmount, DebtStatus = @DebtStatus WHERE DebtId = @DebtId";
            command.Parameters.AddWithValue("@DebtAmount", debt.DebtAmount);
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
            command.CommandText = "SELECT DebtId, DebtAmount, DebtTakenDate, DebtDueDate, DebtSource, DebtStatus, Remark FROM Debts";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                debts.Add(new Debt
                {
                    DebtId = reader.GetInt32(0),
                    DebtAmount = reader.GetDecimal(1),
                    DebtTakenDate = reader.GetDateTime(2),
                    DebtDueDate = reader.GetDateTime(3),
                    DebtSource = reader.GetString(4),
                    DebtStatus = reader.GetString(5),
                    Remark = reader.GetString(6)
                });
            }
            return debts;
        }
    }
}