using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Transaction> Search(string? cashierName, DateTime? startDate, DateTime? endDate)
        {
            var transactions = _context.Transactions.AsQueryable();

            if (!string.IsNullOrEmpty(cashierName))
            {
                transactions = transactions.Where(tr => tr.CashierName.Contains(cashierName));
            }

            if (startDate.HasValue)
            {
                transactions = transactions.Where(tr => tr.TimeStamp >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                transactions = transactions.Where(tr => tr.TimeStamp <= endDate.Value);
            }

            return transactions.ToList();

        }
    }
}
