using Persistence.Entities;
using System.Collections.Generic;

namespace Persistence.Interfaces
{
    public interface ITodoListDAO
    {
        public TodoList GetTodoList(TodoList list);
        public TodoList InsertTodoList(TodoList list);
        public TodoList UpdateTodoList(TodoList list);
        public List<TodoList> GetMultipleTodoLists(TodoList list);
        public bool DeleteTodoList(int Id);
    }
}
