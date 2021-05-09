using System.ComponentModel.DataAnnotations;

namespace BooksApi.Controllers.RequestsResponse
{
    public class UserRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }
    }
}
