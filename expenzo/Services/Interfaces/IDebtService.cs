using expenzo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expenzo.Services.Interfaces
{
    public interface IDebtService
    {
        void CreateTable();
        void AddDebt(Debt debt);
        void UpdateDebt(Debt debt);
        void DeleteDebt(int debtId);
        List<Debt> GetDebts();
        decimal GetTotalDebtRemainingAmount();
        decimal GetTotalPaidAmount();
        decimal GetHighestDebtAmount();
        decimal GetLowestDebtAmount();
        int GetTotalDebtCount();
        List<Debt> GetTop5UpcomingDebts(int count);
    }
}