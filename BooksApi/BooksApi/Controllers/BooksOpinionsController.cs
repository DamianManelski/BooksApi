using AutoMapper;
using BooksApi.Controllers.RequestsResponse;
using BooksApi.Infrastructure;
using BooksApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksOpinionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookOpinionService _bookOpinionService;
        private readonly IUserService _userService;
        private readonly IBooksService _booksService;

        public BooksOpinionsController(IBookOpinionService bookOpinionService, IUserService userService, IBooksService bookService, IMapper mapper)
        {
            _mapper = mapper;
            _bookOpinionService = bookOpinionService;
            _userService = userService;
            _booksService = bookService;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserBookOpinionRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.UserId);

            if (user == null)
            {
                return BadRequest($"User with id: {request.UserId} doesn't exisit. Created user first to add book readed by him.");
            }

            var book = _booksService.GetBookById(request.BookId);

            if (book == null)
            {
                return BadRequest($"Book with id: {request.BookId} doesn't exisit. Created book first.");
            }

            var userBookOpinion = _mapper.Map<UserBookOpinion>(request);

            userBookOpinion.UserId = user.Id;
            userBookOpinion.BookId = book.Id;

            var opinionId = await _bookOpinionService.AddBookOpinionForUser(userBookOpinion, cancellationToken);

            return Ok(opinionId);
        }

    }
}
