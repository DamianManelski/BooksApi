namespace BooksApi.Infrastructure.Models
{
    public class PagingParameters
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int AmountOfCommentsToInclude { get; set; } = 5;
    }
}
