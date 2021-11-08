using MediatR;
using System.Threading;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Queries.TodoItemQueries
{
    public class GetListTodoItemsQuery : IRequest<List<TodoItem>>
    {
        public int listId;
        public GetListTodoItemsQuery(int id)
        {
            listId = id;
        }
    }

    public class GetListTodoItemsHandler : IRequestHandler<GetListTodoItemsQuery, List<TodoItem>>
    {
        private readonly ITodoItemDAO _item;
        public GetListTodoItemsHandler(ITodoItemDAO dao)
        {
            _item = dao;
        }
        public Task<List<TodoItem>> Handle(GetListTodoItemsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_item.GetMultipleTodoItems(new TodoItem()
            {
                TodoListId = request.listId
            }));
        }
    }
}
