using System.ComponentModel.DataAnnotations;

namespace BooksApi.Controllers
{
    public class PagingParamsRequest
    {
        const int maxPageSize = 50;


        private int _pageSize = 10;

        [Range(1, long.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int PageNumber { get; set; } = 1;

        public int AmountOfCommentsToInclude { get; set; } = 5;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
