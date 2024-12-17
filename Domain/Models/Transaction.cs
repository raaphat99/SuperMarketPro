using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int AvailableQuantityBeforeTransaction { get; set; }
        public int SoldQuantity { get; set; }
        public string CashierName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
