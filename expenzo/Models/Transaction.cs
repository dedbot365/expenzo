using System;

namespace expenzo.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string CategoryName { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
    }
}