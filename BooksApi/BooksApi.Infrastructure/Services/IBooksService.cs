using System;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Infrastructure.Services
{
    public interface IBooksService
    {
        Task<Guid> AddBook(Book book, CancellationToken token);

        Task<Guid> UpdateBookIfExistsAsync(Book book, CancellationToken token);

        Task<bool> DeleteBookIfExistsAsync(Guid bookId, CancellationToken token);

        Book GetBookById(Guid bookId);
    }
}
