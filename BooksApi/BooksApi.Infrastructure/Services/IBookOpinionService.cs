using BooksApi.Infrastructure.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Infrastructure.Services
{
    public interface IBookOpinionService
    {
        Task<Guid> AddBookOpinionForUser(UserBookOpinion userBookOpinion, CancellationToken token);

        Task<IQueryable<Book>> GetAllBooksWithOpinions(PagingParameters parameters);
    }
}
