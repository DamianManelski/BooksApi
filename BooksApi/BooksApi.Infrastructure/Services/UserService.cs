using System;
using System.Threading;
using System.Threading.Tasks;

namespace BooksApi.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly BooksDbContext _dbContext;

        public UserService(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<Guid> RegisterUser(User user, CancellationToken token)
        {
            user.Id = Guid.NewGuid();
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(token);
            return user.Id;
        }
    }
}
