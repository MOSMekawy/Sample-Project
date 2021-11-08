using MediatR;
using Persistence.Entities;
using Persistence.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.UserQueries
{
    public class GetUserQuery: IRequest<User>
    {
        public User user { get; private set; }
        public GetUserQuery(User user)
        {
            this.user = user;
        }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private IUserDAO User;
        public GetUserQueryHandler(IUserDAO User)
        {
            this.User = User;
        }
        public Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            User result = User.GetUser(request.user);
            if (result != null) return Task.FromResult(result);
            else return Task.FromResult(new User());
        }
    }
}
