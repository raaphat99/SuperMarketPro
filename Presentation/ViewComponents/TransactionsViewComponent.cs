using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
