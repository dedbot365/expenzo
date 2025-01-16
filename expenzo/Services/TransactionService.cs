using expenzo.Database;
using expenzo.Models;
using expenzo.Services.Interfaces;
using System.Collections.Generic;

namespace expenzo.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionDao _transactionDao; // Data access object for transaction operations
        // Constructor for the TransactionService class
        public TransactionService(TransactionDao transactionDao)
        {
            _transactionDao = transactionDao;
        }
        // Methods for creating, adding, updating, deleting, and retrieving transactions, as well as calculating various transaction statistics.
        public void CreateTable() => _transactionDao.CreateTable();
        public void AddTransaction(Transaction transaction) => _transactionDao.AddTransaction(transaction);
        public void UpdateTransaction(Transaction transaction) => _transactionDao.UpdateTransaction(transaction);
        public void DeleteTransaction(int transactionId) => _transactionDao.DeleteTransaction(transactionId);
        public List<Transaction> GetTransactions() => _transactionDao.GetTransactions();
        public decimal GetTotalIncomeAmount() => _transactionDao.GetTotalIncomeAmount();
        public decimal GetTotalExpenseAmount() => _transactionDao.GetTotalExpenseAmount();
        public decimal GetHighestIncomeAmount() => _transactionDao.GetHighestIncomeAmount();
        public decimal GetLowestIncomeAmount() => _transactionDao.GetLowestIncomeAmount();
        public decimal GetHighestExpenseAmount() => _transactionDao.GetHighestExpenseAmount();
        public decimal GetLowestExpenseAmount() => _transactionDao.GetLowestExpenseAmount();
        public int GetTotalTransactionCount() => _transactionDao.GetTotalTransactionCount();
        public List<Transaction> GetTop5RecentTransactions() => _transactionDao.GetTop5RecentTransactions();
    }
}