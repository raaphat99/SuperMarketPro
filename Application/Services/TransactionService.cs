using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Interfaces.UOW;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(IUnitOfWork unitOfWork, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions;
        }

        public Task<Transaction> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> Search(string? cashierName, DateTime? startDate, DateTime? endDate)
        {
            return _transactionRepository.Search(cashierName, startDate, endDate);
        }

        public async Task<bool> CreateAsync(Transaction transaction)
        {
            bool success = true;
            try
            {
                await _transactionRepository.AddAsync(transaction);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
