using Domain.Users;
using MediatR;

namespace Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(notification.UserId);
        return Task.CompletedTask;
    }
}
