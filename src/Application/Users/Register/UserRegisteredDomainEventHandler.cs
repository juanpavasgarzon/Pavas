using Application.Abstractions.Data;
using Application.Abstractions.Mail;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler(
    IMailSender mailSender,
    IApplicationDbContext context,
    ILogger<UserRegisteredDomainEventHandler> logger
) : INotificationHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        User? user = await context.Users.FindAsync([notification.UserId], cancellationToken: cancellationToken);

        if (user is null)
        {
            logger.LogError("The user with the Id = '{UserId}' was not found", notification.UserId);
            return;
        }

        var message = new MailSenderMessage
        {
            Subject = $"Hi {user.FirstName}, you has been registered",
            Message = $"{user.FirstName} {user.LastName} Your account has been registered, your Id is '{notification.UserId}'",
            Recipients = ["garzonp2001@gmail.com"]
        };

        await mailSender.Send(message, cancellationToken);
    }
}
