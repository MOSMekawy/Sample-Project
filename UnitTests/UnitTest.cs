using Application.Commands.UserCommands;
using Persistence.Entities;
using Persistence.Mockups;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System;

namespace UnitTests
{
    public class UnitTest
    {
        private readonly UserDAO _user = new UserDAO(@"D:\Work-Repository-main\Sample-Project\Persistence\Mockups\Mockup.Data\Users.csv");

        [Fact]
        public void CreateUserValidatesName()
        {
            var ex = Record.Exception(() => new CreateUserCommand(""));

            // We can assert exception message

            Assert.Equal("Name must be between 4 & 50 chars in Length.", ex.Message);

            // We can check exception type 
            // Assert.IsType<InvalidFormat>(ex);
        }
        [Fact]
        public void CreateUserReturnsCreatedUser()
        {
            CreateUserCommandHandler handler = new CreateUserCommandHandler(_user);

            Task<User> user = handler.Handle(new CreateUserCommand("Salah"), new CancellationToken());

            user.Wait();

            User res = user.Result;

            Assert.Equal("Salah", res.Name);
        }
    }
}
