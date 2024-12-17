using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        IEnumerable<Transaction> Search(string? cashierName, DateTime? startDate, DateTime? endDate);
        Task<bool> CreateAsync(Transaction transaction);
        bool Update(Transaction transaction);
        Task<bool> DeleteByIdAsync(int id);

    }
}
