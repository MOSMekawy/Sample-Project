using MediatR;
using Persistence.Entities;
using System.Threading;
using System.Threading.Tasks;
using Persistence.Interfaces;

namespace Application.Queries.TodoListQueries
{
    public class GetTodoListQuery : IRequest<TodoList>
    {
        public TodoList query;
        public GetTodoListQuery(TodoList list)
        {
            query = list;
        }
    }

    public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, TodoList>
    {
        private readonly ITodoListDAO _list;
        public GetTodoListQueryHandler(ITodoListDAO dao)
        {
            _list = dao;
        }
        public Task<TodoList> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_list.GetTodoList(request.query));
        }
    }
}
