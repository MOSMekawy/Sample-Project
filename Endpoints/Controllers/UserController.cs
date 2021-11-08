using Microsoft.AspNetCore.Mvc;
using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using System.Threading.Tasks;
using Persistence.Entities;
using MediatR;

namespace Endpoints.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("[action]/{id:int}")]
        [HttpGet]
        public async Task<User> GetUser(int id)
        {
            return await _mediator.Send(new GetUserQuery(new User()
            {
              Id = id
            }));
        }

        [Route("[action]/")]
        [HttpPost]
        public async Task<User> CreateUser([FromBody] User user)
        {
            return await _mediator.Send(new CreateUserCommand(user.Name));
        }

        [Route("[action]/")]
        [HttpPost]
        public async Task<User> UpdateUser([FromBody] User user)
        {
            return await _mediator.Send(new UpdateUserCommand(user.Id, user.Name));
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<bool> DeleteUser(int id)
        {
            return await _mediator.Send(new DeleteUserCommand(id));
        }

    }
}
