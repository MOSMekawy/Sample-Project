using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Persistence.Entities;
using Persistence.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<User> 
    {
       [Required]
       public int Id { get; private set; }
       [Required]
       [StringLength(50, MinimumLength = 4)]
       public string Name { get; private set; }
       public UpdateUserCommand(int Id, string Name) 
       {
            this.Id = Id;
            this.Name = Name;
       }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
    {
        IUserDAO User;
        public UpdateUserCommandHandler(IUserDAO User)
        {
            this.User = User;
        }
        public Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(User.UpdateUser(new Persistence.Entities.User()
            {
              Id = request.Id,
              Name = request.Name
            }));
        }
    }
}
