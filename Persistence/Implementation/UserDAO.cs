using Simplified;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Data.SqlClient;

namespace Persistence.Implementation
{
    public class UserDAO : IUserDAO
    {
        private readonly string ConnectionString;
        public UserDAO(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public bool DeleteUser(int Id)
        {
            DBClient<User> client = new(ConnectionString);

            dynamic[,] param = { 
                {"@Id", Id} 
            };

            int affected = client.StoredProcedure()
                                 .Define("DeleteUser")
                                 .Apply(param)
                                 .ExecuteNonQuery();

            client.StoredProcedure().Terminate();

            return affected == 1;
        }

        public User GetUser(User user)
        {
            DBClient<User> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", user.Id},
              {"@Name", user.Name}
            };

            return client.StoredProcedure()
                         .Define("GetUser")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }

        public User InsertUser(User user)
        {
            DBClient<User> client = new(ConnectionString);

            dynamic[,] param =
            {
              {"@Name", user.Name}
            };

            return client.StoredProcedure()
                         .Define("InsertUser")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }

        public User UpdateUser(User user)
        {
            DBClient<User> client = new(ConnectionString);

            dynamic[,] param = {
              {"@Id", user.Id},
              {"@Name", user.Name}
            };

            return client.StoredProcedure()
                         .Define("UpdateUser")
                         .Apply(param)
                         .ExecuteReader()
                         .Map(this.Map);
        }
        private User Map(SqlDataReader Reader)
        {
           return new User()
           {
             Id = (int)Reader["Id"],
             Name = (string)Reader["Name"]
           };
            
        }
    }
}
