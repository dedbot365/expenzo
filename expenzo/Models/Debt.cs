using System;

namespace expenzo.Models
{
    public class Debt
    {
        public decimal DebtAmount { get; set; }
        public DateTime DebtTakenDate { get; set; } = DateTime.Now;
        public DateTime DebtDueDate { get; set; }
        public string DebtSource { get; set; }
    }
}