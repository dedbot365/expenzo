using System;

namespace expenzo.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; }= string.Empty;
        public string CategoryName { get; set; }= string.Empty;
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string Remarks { get; set; }= string.Empty;
    }
}