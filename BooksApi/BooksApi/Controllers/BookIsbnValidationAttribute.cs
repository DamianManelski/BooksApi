using BooksApi.Controllers.Requests;
using ProductCodeValidator;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Controllers
{
    public class BookIsbnValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var createBookRequest = (BookRequest)validationContext.ObjectInstance;

            var result = IsbnValidator.IsValidIsbnFormat(createBookRequest.Isbn);

            return result ? ValidationResult.Success : new ValidationResult($"Invalid ISBN number.");
        }
    }
}
