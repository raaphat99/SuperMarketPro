using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.CustomValidators
{
    public class FileValidationAttribute : ValidationAttribute
    {
        private readonly string[] allowedTypes;
        private readonly int maxSizeInMB;

        public FileValidationAttribute(string[] _allowedTypes, int _maxSizeInMB)
        {
            allowedTypes = _allowedTypes;
            maxSizeInMB = _maxSizeInMB;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
                return new ValidationResult("No file uploaded.");

            if (!allowedTypes.Contains(file.ContentType))
                return new ValidationResult($"Only {string.Join(", ", allowedTypes)} types are allowed.");

            if (file.Length > maxSizeInMB * 1024 * 1024)
                return new ValidationResult($"File size mustn't exceed {maxSizeInMB} MB");

            return ValidationResult.Success;
        }
    }
}
