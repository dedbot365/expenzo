using System;
using System.ComponentModel.DataAnnotations;

namespace expenzo.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Transaction date is required")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public string Remarks { get; set; } = string.Empty;
    }
}