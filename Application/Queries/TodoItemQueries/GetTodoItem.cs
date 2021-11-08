using MediatR;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.TodoItemQueries
{
    public class GetTodoItemQuery : IRequest<TodoItem>
    {
        public TodoItem query;
        public GetTodoItemQuery(TodoItem item)
        {
            this.query = item;
        }
    }

    public class GetTodoItemHandler : IRequestHandler<GetTodoItemQuery, TodoItem>
    {
        private readonly ITodoItemDAO _item;
        public GetTodoItemHandler(ITodoItemDAO dao)
        {
            _item = dao;
        }
        public Task<TodoItem> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_item.GetTodoItem(request.query));
        }
    }
}
