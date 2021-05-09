using AutoMapper;
using BooksApi.Controllers.Requests;
using BooksApi.Controllers.RequestsResponse;
using BooksApi.Infrastructure;
using BooksApi.Infrastructure.Models;
using BooksApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBooksService _booksService;
        private readonly IBookOpinionService _bookOpinionService;

        public BooksController(IBooksService booksService, IBookOpinionService bookOpinionService, IMapper mapper)
        {
            _mapper = mapper;
            _booksService = booksService;
            _bookOpinionService = bookOpinionService;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BookRequest request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);

            var bookId = await _booksService.AddBook(book, cancellationToken);

            return Ok(bookId);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(request);

            var result = await _booksService.UpdateBookIfExistsAsync(book, cancellationToken);

            if (result == Guid.Empty)
            {
                return BadRequest("Book doesn't exist in the system.");
            }

            return Ok("Book has been updated.");
        }

        [HttpDelete]
        [Route("{bookId}")]
        public async Task<ActionResult> Delete([FromRoute] Guid bookId, CancellationToken cancellationToken)
        {

            var result = await _booksService.DeleteBookIfExistsAsync(bookId, cancellationToken);

            if (!result)
            {
                return BadRequest("Book doesn't exist in the system.");
            }

            return Ok("Book has been marked as deleted.");
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PagingParamsRequest pagingRequest)
        {
            var pagingModel = _mapper.Map<PagingParameters>(pagingRequest);

            var booksWithOpinions = await _bookOpinionService.GetAllBooksWithOpinions(pagingModel);

            return Ok(booksWithOpinions);
        }
    }
}
