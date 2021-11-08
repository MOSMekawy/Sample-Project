using MediatR;
using System.Threading;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading.Tasks;
using System;

namespace Application.Commands.TodoItemCommands
{
    public class UpdateTodoItemCommand : IRequest<TodoItem>
    {
        public TodoItem item;
        public UpdateTodoItemCommand(TodoItem item)
        {
            if (item.Id <= 0) throw new Exception("Please provide a valid item id.");

            this.item = item;
        }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, TodoItem>
    {
        private readonly ITodoItemDAO _item;
        public UpdateTodoItemCommandHandler(ITodoItemDAO dao)
        {
            _item = dao;
        }
        public Task<TodoItem> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_item.UpdateTodoItem(request.item));
        }
    }
}
