using System;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Infrastructure.Services
{
    public interface IUserService
    {
        Task<Guid> RegisterUser(User user, CancellationToken token);

        Task<User> GetUserById(Guid userId);
    }
}
