using expenzo.Database;
using expenzo.Models;
using expenzo.Services.Interfaces;
using System.Collections.Generic;

namespace expenzo.Services
{
    public class DebtService : IDebtService
    {
        private readonly DebtDao _debtDao; // Data access object for debt-related operations
        // Constructor for the DebtService class
        public DebtService(DebtDao debtDao)
        {
            _debtDao = debtDao;
        }
        // Methods for creating, adding, updating, deleting, and retrieving debts, as well as calculating various debt statistics.
        public void CreateTable() => _debtDao.CreateTable();
        public void AddDebt(Debt debt) => _debtDao.AddDebt(debt);
        public void UpdateDebt(Debt debt) => _debtDao.UpdateDebt(debt);
        public void DeleteDebt(int debtId) => _debtDao.DeleteDebt(debtId);
        public List<Debt> GetDebts() => _debtDao.GetDebts();
        public decimal GetTotalDebtRemainingAmount() => _debtDao.GetTotalDebtRemainingAmount();
        public decimal GetTotalPaidAmount() => _debtDao.GetTotalPaidAmount();
        public decimal GetHighestDebtAmount() => _debtDao.GetHighestDebtAmount();
        public decimal GetLowestDebtAmount() => _debtDao.GetLowestDebtAmount();
        public int GetTotalDebtCount() => _debtDao.GetTotalDebtCount();
        public List<Debt> GetTop5UpcomingDebts(int count) => _debtDao.GetTop5UpcomingDebts(count);
    }
}