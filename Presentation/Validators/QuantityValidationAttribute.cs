using Domain.Models;
using Infrastructure.Data;
using Presentation.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Application.CustomValidators
{
    public class QuantityValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            CashierViewModel cashierViewModel = validationContext.ObjectInstance as CashierViewModel;
            int selectedQuantity = (int)value;
            ApplicationContext _context = (ApplicationContext)validationContext.GetService(typeof(ApplicationContext));
            Product product = _context.Products.Find(cashierViewModel.SelectedProductId);

            if (_context == null || cashierViewModel == null)
                return new ValidationResult("An error occurred during validation.");

            if (product == null)
                return new ValidationResult("No product found.");

            if (selectedQuantity <= 0)
                return new ValidationResult("Please enter a valid quantity.");

            if (selectedQuantity > product.Quantity)
                return new ValidationResult($"Insufficient stock. There are only {product.Quantity} items available.");

            return ValidationResult.Success;
        }
    }
}
