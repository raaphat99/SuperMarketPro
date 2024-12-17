using Application.Interfaces.IServices;
using Application.Interfaces.UOW;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    [Authorize]
    public class CashierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;
        public CashierController(IUnitOfWork unitOfWork,
                                    IProductService productService,
                                    ICategoryService categoryService,
                                    ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categoriesWithProducts = _categoryService.GetCategoriesWithProducts().ToList();

            CashierViewModel cashierViewModel = new CashierViewModel()
            {
                Categories = categoriesWithProducts
            };

            return View(cashierViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessTransaction(CashierViewModel cashierViewModel)
        {
            List<Category> categoriesWithProducts = _categoryService.GetCategoriesWithProducts().ToList();
            Product product = await _productService.GetByIdAsync(cashierViewModel.SelectedProductId);

            if (product == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                cashierViewModel.Categories = categoriesWithProducts;
                cashierViewModel.SelectedCategoryId = product.CategoryId;
                return View("Index", cashierViewModel);
            }

            Transaction transaction = new Transaction
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                AvailableQuantityBeforeTransaction = product.Quantity,
                SoldQuantity = cashierViewModel.QuantityToSell,
                CashierName = "Ahmed Raafat",
                TimeStamp = DateTime.Now,
            };

            await _transactionService.CreateAsync(transaction);

            product.Quantity -= cashierViewModel.QuantityToSell;
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

    }
}

