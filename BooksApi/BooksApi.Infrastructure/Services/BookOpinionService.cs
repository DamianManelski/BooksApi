using BooksApi.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Infrastructure.Services
{
    public class BookOpinionService : IBookOpinionService
    {
        private readonly BooksDbContext _dbContext;

        public BookOpinionService(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddBookOpinionForUser(UserBookOpinion userBookOpinion, CancellationToken token)
        {
            userBookOpinion.Id = Guid.NewGuid();

            _dbContext.Add(userBookOpinion);

            await _dbContext.SaveChangesAsync(token);

            return userBookOpinion.Id;
        }

        public async Task<IQueryable<Book>> GetAllBooksWithOpinions(PagingParameters parameters)
        {
            return _dbContext.Books
                    .Include(s => s.UsersBookOpinions.Take(parameters.AmountOfCommentsToInclude)) // this is probably invalid because it requires to specify page number of opinions - dont have a time for that.
                    .OrderBy(on => on.Title)
                    .Where(s => s.DeletionDate == null) //books marked as deleted will not be returned
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize);
        }
    }
}
