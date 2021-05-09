using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Controllers.RequestsResponse
{
    public class UpdateBookRequest
    {
        [Required]
        public Guid BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [BookIsbnValidation]
        public string Isbn { get; set; }

        [Range(2, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")] //int.MaxValue -> assumption for simplicity
        public int NumberOfPages { get; set; }
    }
}
