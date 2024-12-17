using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        { }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            return _context.Categories
                .Where(category => category.Products.Count() > 0)
                .ToList();
        }
    }
}
