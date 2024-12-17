using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Interfaces.UOW;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var products = _productRepository.Find(p => p.CategoryId == categoryId).ToList();
            return products;
        }

        public async Task<bool> CreateAsync(Product product)
        {
            bool success = true;
            try
            {
                await _productRepository.AddAsync(product);
            }
            catch (Exception)
            {
                success = false;
                throw;
            }
            return success;
        }

        public async Task HandleImageUpload(IFormFile imageFile, Product product)
        {
            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);
            product.ImageData = memoryStream.ToArray();
            product.ImageContentType = imageFile.ContentType;
        }

        public bool Update(Product product)
        {
            bool success = true;
            try
            {
                _productRepository.Update(product);
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
            Product product = await _productRepository.GetByIdAsync(id);

            try
            {
                _productRepository.Remove(product);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public bool DeleteProductsByCategory(int categoryId)
        {
            bool success = true;
            var products = GetProductsByCategory(categoryId);

            if (products.Count() > 0)
            {
                try
                {
                    _productRepository.RemoveRange(products);
                }
                catch (Exception)
                {
                    success = false;
                }
            }
            return success;
        }



    }
}
