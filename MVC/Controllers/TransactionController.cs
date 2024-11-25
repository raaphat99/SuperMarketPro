using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationContext _context;
        public TransactionController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ICollection<Transaction> transactions = _context.Transactions.AsNoTracking().ToList();
            if (transactions == null)
                return NotFound();

            TransactionSearchViewModel model = new TransactionSearchViewModel()
            {
                Transactions = transactions
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(TransactionSearchViewModel model)
        {
            var query = _context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(model.CashierName))
            {
                query = query.Where(tr => tr.CashierName.Contains(model.CashierName));
            }

            if (model.StartDate.HasValue)
            {
                query = query.Where(tr => tr.TimeStamp >= model.StartDate.Value);
            }

            if (model.EndDate.HasValue)
            {
                query = query.Where(tr => tr.TimeStamp <= model.EndDate.Value);
            }

            model.Transactions = query.ToList();
            return View("Index", model);
        }

    }
}
