using System;

namespace BooksApi.Infrastructure
{
    public class UserBookOpinion
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int UserRating { get; set; }
        public string Comment { get; set; }
    }
}
