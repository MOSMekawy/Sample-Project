using Persistence.Entities;
using Persistence.Interfaces;
using System.Data.SqlClient;
using Simplified;
using System.Collections.Generic;

namespace Persistence.Implementation
{
    public class TodoItemDAO : ITodoItemDAO
    {
        private string ConnectionString;
        public TodoItemDAO(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        public bool DeleteTodoItem(int Id)
        {
            DBClient<TodoItem> client = new(ConnectionString);

            dynamic[,] param = {
                {"@Id", Id}
            };

            int affected = client.StoredProcedure()
                                 .Define("DeleteTodoItem")
                                 .Apply(param)
                                 .ExecuteNonQuery();

            client.StoredProcedure().Terminate();

            return affected == 1;
        }

        public List<TodoItem> GetMultipleTodoItems(TodoItem item)
        {
            DBClient<TodoItem> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", item.Id},
              {"@Name", item.Name},
              {"@Description", item.Description},
              {"@TodoList", item.TodoListId}
            };

            return client.StoredProcedure()
                         .Define("GetTodoItem")
                         .Apply(param)
                         .ExecuteReader()
                         .MultiMap(this.Map);
        }

        public TodoItem GetTodoItem(TodoItem item)
        {
            DBClient<TodoItem> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", item.Id},
              {"@Name", item.Name},
              {"@Description", item.Description},
              {"@TodoList", item.TodoListId}
            };

            return client.StoredProcedure()
                         .Define("GetTodoItem")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }

        public TodoItem InsertTodoItem(TodoItem item)
        {
            DBClient<TodoItem> client = new(ConnectionString);

            dynamic[,] param =
            {
              {"@Name", item.Name},
              {"@Description", item.Description},
              {"@TodoList", item.TodoListId}
            };

            return client.StoredProcedure()
                         .Define("InsertTodoItem")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }

        public TodoItem UpdateTodoItem(TodoItem item)
        {
            DBClient<TodoItem> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", item.Id},
              {"@Name", item.Name},
              {"@Description", item.Description},
              {"@TodoList", item.TodoListId}
            };

            return client.StoredProcedure()
                         .Define("UpdateTodoItem")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }

        private TodoItem Map(SqlDataReader Reader)
        {
            return new TodoItem()
            {
                Id = (int)Reader["Id"],
                Name = (string)Reader["Name"],
                Description = (string)Reader["Description"],
                TodoListId = (int)Reader["TodoList"]
            };
        }
    }
}
