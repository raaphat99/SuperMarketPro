
using Application.CustomValidators;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class CashierViewModel
    {
        public string? CashierName { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        [Display(Name = "Category")]
        public int SelectedCategoryId { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public int SelectedProductId { get; set; }

        [Display(Name = "No. Items")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number.")]
        [QuantityValidation]
        public int QuantityToSell { get; set; } = 1;
    }
}
