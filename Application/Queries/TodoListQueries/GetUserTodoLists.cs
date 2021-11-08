using MediatR;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System;

namespace Application.Queries.TodoListQueries
{
    public class GetUserTodoListsQuery : IRequest<List<TodoList>>  
    {
        public int UserId;
        public GetUserTodoListsQuery(int id)
        {
            if (id <= 0) throw new Exception("Please provide a valid userId");
            UserId = id;
        }
    }

    public class GetUserTodoListsQueryHandler : IRequestHandler<GetUserTodoListsQuery, List<TodoList>>
    {
        private readonly ITodoListDAO _list;

        public GetUserTodoListsQueryHandler(ITodoListDAO dao)
        {
            _list = dao;
        }

        public Task<List<TodoList>> Handle(GetUserTodoListsQuery request, CancellationToken cancellationToken)
        {
            List<TodoList> list = _list.GetMultipleTodoLists(new TodoList()
            {
                UserId = request.UserId
            });

            return Task.FromResult(list);
        }
    }
}
