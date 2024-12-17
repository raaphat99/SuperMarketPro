using Application.CustomValidators;
using System.ComponentModel.DataAnnotations;


namespace Presentation.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [MinLength(3, ErrorMessage = "Product Name must be at least 3 characters long.")]
        public string Name { get; set; }

        [Range(minimum: 0.0, maximum: double.MaxValue, ErrorMessage = "Please enter a valid price.")]
        public double Price { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Please enter a valid quantity.")]
        public int Quantity { get; set; }

        [FileValidation(new[] { "image/jpeg", "image/jpg", "image/png" }, 2)]
        [Display(Name = "Product Image")]
        public IFormFile ImageFile { get; set; }

        // These are used for image rendering
        public byte[]? ImageData { get; set; }
        public string? ImageContentType { get; set; }

        [Required(ErrorMessage = "Please select a valid category.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
