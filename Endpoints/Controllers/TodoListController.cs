using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;
using System.Threading.Tasks;
using Application.Queries.TodoListQueries;
using Application.Commands.TodoListCommands;
using System.Collections.Generic;

namespace Endpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : Controller
    {
        private readonly IMediator _mediator;

        public TodoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("[action]/{id:int}")]
        [HttpGet]
        public async Task<TodoList> GetTodoList(int id)
        {
            return await _mediator.Send(new GetTodoListQuery(new TodoList()
            {
                Id = id
            }));
        }

        [Route("[action]/{id:int}")]
        [HttpGet]
        public async Task<List<TodoList>> GetAllTodoListsPerUser(int id)
        {
            return await _mediator.Send(new GetUserTodoListsQuery(id));
        }

        [Route("[action]/")]
        [HttpPost]
        public async Task<TodoList> CreateTodoList([FromBody] TodoList list)
        {
            return await _mediator.Send(new CreateTodoListCommand(list));
        }

        [Route("[action]/")]
        [HttpPost]
        public async Task<TodoList> UpdateTodoList([FromBody] TodoList list)
        {
            return await _mediator.Send(new UpdateTodoListCommand(list));
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<bool> DeleteTodoList(int id)
        {
            return await _mediator.Send(new DeleteTodoListCommand(id));
        }

    }
}
