using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.TodoItemCommands;
using Application.Queries.TodoItemQueries;


namespace Endpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : Controller
    {
        private readonly IMediator _mediator;

        public TodoItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("[action]/{id:int}")]
        [HttpGet]
        public async Task<TodoItem> GetTodoItem(int id)
        {
            return await _mediator.Send(new GetTodoItemQuery(new TodoItem()
            {
                Id = id
            }));
        }

        [Route("[action]/{id:int}")]
        [HttpGet]
        public async Task<List<TodoItem>> GetAllTodoItemsPerList(int id)
        {
            return await _mediator.Send(new GetListTodoItemsQuery(id));
        }

        [Route("[action]/")]
        [HttpPost]
        public async Task<TodoItem> CreateTodoItem([FromBody] TodoItem item)
        {
            return await _mediator.Send(new CreateTodoItemCommand(item));
        }

        [Route("[action]/")]
        [HttpPost]
        public async Task<TodoItem> UpdateTodoItem([FromBody] TodoItem item)
        {
            return await _mediator.Send(new UpdateTodoItemCommand(item));
        }

        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<bool> DeleteTodoItem(int id)
        {
            return await _mediator.Send(new DeleteTodoItemCommand(id));
        }

    }
}
