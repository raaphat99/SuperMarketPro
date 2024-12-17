using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [MinLength(3, ErrorMessage = "Category Name must be at least 3 characters long.")]
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
    }
}
