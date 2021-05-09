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
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var userId = await _userService.RegisterUser(user, cancellationToken);

            return Ok(userId);
        }
    }
}
