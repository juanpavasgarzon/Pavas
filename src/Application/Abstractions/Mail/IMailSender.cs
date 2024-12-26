namespace Application.Abstractions.Mail;

public interface IMailSender
{
    public Task Send(MailSenderMessage payload, CancellationToken cancellationToken = default);
}
