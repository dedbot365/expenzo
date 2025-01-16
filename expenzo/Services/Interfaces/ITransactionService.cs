namespace expenzo.Services.Interfaces
{
    public interface ITransactionService
    {
        void CreateTable();
        void AddTransaction(Models.Transaction transaction);
        void UpdateTransaction(Models.Transaction transaction);
        void DeleteTransaction(int transactionId);
        List<Models.Transaction> GetTransactions();
        decimal GetTotalIncomeAmount();
        decimal GetTotalExpenseAmount();
        decimal GetHighestIncomeAmount();
        decimal GetLowestIncomeAmount();
        decimal GetHighestExpenseAmount();
        decimal GetLowestExpenseAmount();
        int GetTotalTransactionCount();
        List<Models.Transaction> GetTop5RecentTransactions();
    }
}