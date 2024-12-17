using Application.Interfaces.IServices;
using Application.Interfaces.UOW;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;
        public TransactionController(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllAsync();

            if (transactions == null)
                return NotFound();

            TransactionSearchViewModel model = new TransactionSearchViewModel()
            {
                Transactions = transactions.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Search(TransactionSearchViewModel model)
        {
            IEnumerable<Transaction> resultSet = _transactionService.Search(model.CashierName, model.StartDate, model.EndDate);

            TransactionSearchViewModel tSearchVM = new TransactionSearchViewModel
            {
                Transactions = resultSet.ToList()
            };
            return View("Index", tSearchVM);
        }

    }
}
