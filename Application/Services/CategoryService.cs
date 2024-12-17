using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Interfaces.UOW;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductService _productService;
        public CategoryService(ICategoryRepository categoryRepository, IProductService productService)
        {
            _categoryRepository = categoryRepository;
            _productService = productService;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category;
        }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            var categories = _categoryRepository.GetCategoriesWithProducts();
            return categories;
        }

        public async Task<bool> CreateAsync(Category category)
        {
            bool success = true;
            try
            {
                await _categoryRepository.AddAsync(category);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool Update(Category category)
        {
            bool success = true;
            try
            {
                _categoryRepository.Update(category);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            bool success = true;
            Category category = await _categoryRepository.GetByIdAsync(id);
            try
            {
                _productService.DeleteProductsByCategory(id);
                _categoryRepository.Remove(category);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }
    }
}
