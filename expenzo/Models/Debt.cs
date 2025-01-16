using System.ComponentModel.DataAnnotations;

namespace expenzo.Models
{
    public class Debt
    {
        public int DebtId { get; set; }

        [Required(ErrorMessage = "Debt amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Debt amount must be greater than zero.")]
        public decimal DebtAmount { get; set; }

        public decimal RemainingAmount { get; set; }
        public decimal PaidAmount { get; set; }

        [Required(ErrorMessage = "Debt taken date is required.")]
        public DateTime DebtTakenDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Debt due date is required.")]
        public DateTime DebtDueDate { get; set; }

        [Required(ErrorMessage = "Debt source is required.")]
        public string DebtSource { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debt status is required.")]
        public string DebtStatus { get; set; } = "PendingDebt";

        [Required(ErrorMessage = "Remark is required.")]
        public string Remark { get; set; } = string.Empty;
    }
}