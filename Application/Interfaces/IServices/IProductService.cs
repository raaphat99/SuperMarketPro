using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        Task<bool> CreateAsync(Product product);
        Task HandleImageUpload(IFormFile imageFile, Product product);
        bool Update(Product product);
        Task<bool> DeleteByIdAsync(int id);
        public bool DeleteProductsByCategory(int categoryId);
    }
}
