using MediatR;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.TodoListCommands
{
    public class DeleteTodoListCommand : IRequest<bool>
    {
        public int listId;
        public DeleteTodoListCommand(int id)
        {
            listId = id;
        }
    }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand, bool>
    {
        private readonly ITodoListDAO _list;
        public DeleteTodoListCommandHandler(ITodoListDAO dao)
        {
            _list = dao;
        }

        public Task<bool> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_list.DeleteTodoList(request.listId));
        }
    }
}
