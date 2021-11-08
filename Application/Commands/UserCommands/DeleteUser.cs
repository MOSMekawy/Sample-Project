using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Persistence.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<bool>
    {   
        [Required]
        public int Id { get; private set; }
        public DeleteUserCommand(int Id)
        {
            if (Id <= 0) throw new Exception("INVALID_ID");
            this.Id = Id;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private IUserDAO User;
        public DeleteUserCommandHandler(IUserDAO User)
        {
            this.User = User;
        }
        public Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(User.DeleteUser(request.Id));
        }
    }
}
