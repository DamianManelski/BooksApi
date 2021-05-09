using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Infrastructure.Services
{
    public class BooksService : IBooksService
    {
        private readonly BooksDbContext _booksDbContext;

        public BooksService(BooksDbContext booksDbContext)
        {
            _booksDbContext = booksDbContext;
        }

        public async Task<Guid> AddBook(Book book, CancellationToken token)
        {
            book.Id = Guid.NewGuid();
            _booksDbContext.Books.Add(book);
            await _booksDbContext.SaveChangesAsync(token);
            return book.Id;
        }

        public async Task<bool> DeleteBookIfExistsAsync(Guid bookId, CancellationToken token)
        {
            var booksToUpdate = _booksDbContext.Books.FirstOrDefault(t => t.Id == bookId);

            if (booksToUpdate == null)
            {
                return false;
            }
            booksToUpdate.DeletionDate = DateTime.UtcNow;

            await _booksDbContext.SaveChangesAsync(token);
            return true;
        }

        public Book GetBookById(Guid bookId)
        {
            return _booksDbContext.Books.FirstOrDefault(t => t.Id == bookId && t.DeletionDate == null);
        }

        public async Task<Guid> UpdateBookIfExistsAsync(Book book, CancellationToken token)
        {
            var booksToUpdate = _booksDbContext.Books.FirstOrDefault(t => t.Id == book.Id);

            if (booksToUpdate == null)
            {
                return Guid.Empty;
            }

            booksToUpdate.Author = book.Author;
            booksToUpdate.Isbn = book.Isbn;
            booksToUpdate.NumberOfPages = book.NumberOfPages;
            booksToUpdate.Title = book.Title;

            await _booksDbContext.SaveChangesAsync(token);
            return booksToUpdate.Id;
        }
    }
}
