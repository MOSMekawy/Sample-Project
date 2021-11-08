using MediatR;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.TodoItemCommands
{
    public class DeleteTodoItemCommand : IRequest<bool>
    {
        public int itemId;
        public DeleteTodoItemCommand(int id)
        {
            itemId = id;
        }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, bool>
    {
        private readonly ITodoItemDAO _item;
        public DeleteTodoItemCommandHandler(ITodoItemDAO dao)
        {
            _item = dao;
        }
        public Task<bool> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_item.DeleteTodoItem(request.itemId));
        }
    }
}
