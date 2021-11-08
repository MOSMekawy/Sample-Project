using MediatR;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.TodoListCommands
{
    public class CreateTodoListCommand : IRequest<TodoList> 
    {
       public TodoList inserted;
       public CreateTodoListCommand(TodoList list)
       {
            inserted = list;
       }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, TodoList>
    {
        private readonly ITodoListDAO _list;
        public CreateTodoListCommandHandler(ITodoListDAO dao)
        {
            _list = dao;
        }

        public Task<TodoList> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_list.InsertTodoList(request.inserted));
        }
    }
}
