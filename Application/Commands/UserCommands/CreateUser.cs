using MediatR;
using Persistence.Interfaces;
using Persistence.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; private set; }
        public CreateUserCommand(string Name)
        {
            if (Name.Length < 4 || Name.Length > 50) throw new System.Exception("Name must be between 4 & 50 chars in Length.");
            this.Name = Name;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private IUserDAO User;
        public CreateUserCommandHandler(IUserDAO User)
        {
            this.User = User; 
        }
        public Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(User.InsertUser(new User()
            {
                Name = request.Name
            }));
        }
    }
}
