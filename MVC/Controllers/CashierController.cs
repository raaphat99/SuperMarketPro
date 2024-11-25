using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class CashierController : Controller
    {
        private readonly ApplicationContext _context;
        public CashierController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ICollection<Category> categoriesWithProducts = _context.Categories.AsNoTracking().Where(category => category.Products.Count > 0).ToList();

            CashierViewModel cashierViewModel = new CashierViewModel()
            {
                Categories = categoriesWithProducts
            };

            return View(cashierViewModel);
        }

        [HttpPost]
        public IActionResult ProcessTransaction(CashierViewModel cashierViewModel)
        {
            ICollection<Category> categoriesWithProducts = _context.Categories.AsNoTracking().Where(category => category.Products.Count > 0).ToList();
            Product product = _context.Products.Find(cashierViewModel.SelectedProductId);

            if (product == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                cashierViewModel.Categories = categoriesWithProducts;
                cashierViewModel.SelectedCategoryId = product.CategoryId;
                return View("Index", cashierViewModel);
            }

            AddTransaction(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Quantity,
                    cashierViewModel.QuantityToSell,
                    "Ahmed Raafat"
                );

            product.Quantity -= cashierViewModel.QuantityToSell;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void AddTransaction(
                        int productId,
                        string productName,
                        double price,
                        int availableQuantityBeforeTransaction,
                        int soldQuantity,
                        string? cashierName)
        {
            var transaction = new Transaction
            {
                TimeStamp = DateTime.Now,
                ProductId = productId,
                ProductName = productName,
                Price = price,
                AvailableQuantityBeforeTransaction = availableQuantityBeforeTransaction,
                SoldQuantity = soldQuantity,
                CashierName = cashierName
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

    }
}

