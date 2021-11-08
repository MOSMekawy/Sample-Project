using Persistence.Entities;

namespace Persistence.Interfaces
{
    public interface IUserDAO
    {
        public User GetUser(User user);
        public User InsertUser(User user);
        public User UpdateUser(User user);
        public bool DeleteUser(int Id);

    }
}
