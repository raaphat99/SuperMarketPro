using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        IEnumerable<Category> GetCategoriesWithProducts();
        Task<bool> CreateAsync(Category category);
        bool Update(Category category);
        Task<bool> DeleteByIdAsync(int id);
    }
}
