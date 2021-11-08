using Persistence.Entities;
using System.Collections.Generic;

namespace Persistence.Interfaces
{
    public interface ITodoItemDAO
    {
       public TodoItem GetTodoItem(TodoItem item);
       public TodoItem InsertTodoItem(TodoItem item);
       public TodoItem UpdateTodoItem(TodoItem item);
       public List<TodoItem> GetMultipleTodoItems(TodoItem item);
       public bool DeleteTodoItem(int Id);
       
    }
}
