using Application.Abstractions.Data;
using Application.Abstractions.Mail;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.Extensions.Logging;

namespace Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler(
    IMailSender mailSender,
    IApplicationDbContext context,
    ILogger<UserRegisteredDomainEventHandler> logger
) : IDomainEventHandler<UserRegisteredDomainEvent>
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
            Recipients = [user.Email],
            Subject = $"Welcome, {user.FirstName}!",
            Message = $"""
                         Hello {user.FirstName} {user.LastName}, Your account has been successfully registered.
                         Your User ID is: {notification.UserId}.
                         Feel free to reach out if you have any questions.
                       """
        };

        await mailSender.Send(message, cancellationToken);
    }
}
