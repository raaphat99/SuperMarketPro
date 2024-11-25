using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.ViewComponents
{
    [ViewComponent]
    public class TransactionsViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;
        public TransactionsViewComponent(ApplicationContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ICollection<Transaction> transactions = _context.Transactions.AsNoTracking().ToList();
            return View(transactions);
        }
    }
}
