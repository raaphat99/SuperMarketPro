using Domain.Models;

namespace Presentation.ViewModels
{
    public class TransactionSearchViewModel
    {
        public string? CashierName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
