using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Controllers.RequestsResponse
{
    public class UserBookOpinionRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [StringLength(300, ErrorMessage = "Comment is too long.")]
        public string Comment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int UserRating { get; set; }
    }
}
