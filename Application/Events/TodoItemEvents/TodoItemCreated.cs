using System;
using MediatR;
using System.Threading.Tasks;
using Persistence.Entities;
using System.Threading;

namespace Application.Events.TodoItemEvents
{
    public class TodoItemCreated : INotification
    {   
        // Contains your message
        public TodoItemCreated()
        {

        }
    }

    public class TodoItemCreatedNotificationHandler : INotificationHandler<TodoItemCreated>
    {
        public Task Handle(TodoItemCreated notification, CancellationToken cancellationToken)
        {

            // Execute some code

            Console.WriteLine("I am notified.");

            return Task.FromResult(0);
        }
    }

    public class TodoItemCreatedNotificationAltHandler : INotificationHandler<TodoItemCreated>
    {
        // You have access to notification
        public Task Handle(TodoItemCreated notification, CancellationToken cancellationToken)
        {

            // Execute some other code

            Console.WriteLine("I am also notified.");

            return Task.FromResult(0);
        }
    }

}
