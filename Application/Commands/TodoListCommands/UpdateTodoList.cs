using System;
using MediatR;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.TodoListCommands
{
    public class UpdateTodoListCommand : IRequest<TodoList>
    {
        public TodoList query;
        public UpdateTodoListCommand(TodoList list)
        {
            if (list.Id <= 0) throw new Exception("Please provide a valid TodoListId");

            query = list;
        }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, TodoList>
    {
        private readonly ITodoListDAO _list;
        public UpdateTodoListCommandHandler(ITodoListDAO dao)
        {
            _list = dao;
        }
        public Task<TodoList> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_list.UpdateTodoList(request.query));
        }
    }
}
