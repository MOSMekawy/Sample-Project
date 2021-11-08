using Persistence.Entities;
using Persistence.Interfaces;
using Simplified;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistence.Implementation
{
    public class TodoListDAO : ITodoListDAO
    {
        private string ConnectionString;
        public TodoListDAO(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        public bool DeleteTodoList(int Id)
        {
            DBClient<TodoList> client = new(ConnectionString);

            dynamic[,] param = {
                {"@Id", Id}
            };

            int affected = client.StoredProcedure()
                                 .Define("DeleteTodoList")
                                 .Apply(param)
                                 .ExecuteNonQuery();

            client.StoredProcedure().Terminate();

            return affected == 1;
        }

        public List<TodoList> GetMultipleTodoLists(TodoList list)
        {
            DBClient<TodoList> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", list.Id},
              {"@Name", list.Name},
              {"@Description", list.Description},
              {"@User", list.UserId}
            };

            return client.StoredProcedure()
                         .Define("GetTodoList")
                         .Apply(param)
                         .ExecuteReader()
                         .MultiMap(this.Map);
        }

        public TodoList GetTodoList(TodoList list)
        {
            DBClient<TodoList> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", list.Id},
              {"@Name", list.Name},
              {"@Description", list.Description},
              {"@User", list.UserId}
            };

            return client.StoredProcedure()
                         .Define("GetTodoList")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }

        public TodoList InsertTodoList(TodoList list)
        {
            DBClient<TodoList> client = new(ConnectionString);

            dynamic[,] param =
            {
              {"@Name", list.Name},
              {"@Description", list.Description},
              {"@User", list.UserId}
            };

            return client.StoredProcedure()
                         .Define("InsertTodoList")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }

        public TodoList UpdateTodoList(TodoList list)
        {
            DBClient<TodoList> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", list.Id},
              {"@Name", list.Name},
              {"@Description", list.Description},
              {"@User", list.UserId}
            };

            return client.StoredProcedure()
                         .Define("UpdateTodoList")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }
        private TodoList Map(SqlDataReader Reader)
        {
            return new TodoList()
            {
                Id = (int)Reader["Id"],
                Name = (string)Reader["Name"],
                Description = (string)Reader["Description"],
                UserId = (int)Reader["User"]
            };

        }
    }
}
