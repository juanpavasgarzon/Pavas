using System.Net;
using System.Net.Mail;
using Application.Abstractions.Mail;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Mail;

internal class MailSender(IOptions<MailSettings> mailOptions) : IMailSender
{
    private readonly MailSettings _mailSettings = mailOptions.Value;

    public Task Send(MailSenderMessage payload, CancellationToken cancellationToken = default)
    {
        var smtpClient = new SmtpClient(_mailSettings.Host)
        {
            Port = _mailSettings.Port,
            Credentials = new NetworkCredential(_mailSettings.Username, _mailSettings.Password),
            EnableSsl = _mailSettings.EnableSsl
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_mailSettings.Username),
            Subject = payload.Subject,
            Body = payload.Message,
            IsBodyHtml = true
        };

        foreach (string recipient in payload.Recipients)
        {
            mailMessage.To.Add(recipient);
        }

        smtpClient.Send(mailMessage);

        return Task.CompletedTask;
    }
}
