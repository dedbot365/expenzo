namespace expenzo.Models
{
    public class Debt
    {
        public int DebtId { get; set; }
        public decimal DebtAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime DebtTakenDate { get; set; } = DateTime.Now;
        public DateTime DebtDueDate { get; set; }
        public string DebtSource { get; set; } = string.Empty;
        public string DebtStatus { get; set; } = "PendingDebt";
        public string Remark { get; set; } = string.Empty; 
    }
}