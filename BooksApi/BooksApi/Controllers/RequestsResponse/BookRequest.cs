using System.ComponentModel.DataAnnotations;

namespace BooksApi.Controllers.Requests
{
    public class BookRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [BookIsbnValidation]
        public string Isbn { get; set; }

        [Required]
        [Range(2, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")] //int.MaxValue -> assumption for simplicity   
        public int NumberOfPages { get; set; }
    }
}
