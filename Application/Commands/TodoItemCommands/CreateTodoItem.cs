using Application.Events.TodoItemEvents;
using MediatR;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.TodoItemCommands
{
    public class CreateTodoItemCommand : IRequest<TodoItem>
    {
        public TodoItem item;
        public CreateTodoItemCommand(TodoItem item)
        {
            this.item = item;
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItem>
    {
        private readonly ITodoItemDAO _item;
        private readonly IMediator _mediator;
        public CreateTodoItemCommandHandler(IMediator mediator, ITodoItemDAO dao)
        {
            _mediator = mediator;
            _item = dao;
        }
        public Task<TodoItem> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var result = _item.InsertTodoItem(request.item);

            // Message published
            _mediator.Publish(new TodoItemCreated());

            return Task.FromResult(result);
        }
    }

}
