using System.Net;
using System.Net.Mail;
using Application.Abstractions.Mail;

namespace Infrastructure.Mail;

internal class MailSender : IMailSender
{
    public Task Send(MailSenderMessage payload, CancellationToken cancellationToken = default)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("garzonp2001@gmail.com", "fybc kdar yadh aszd"),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("garzonp2001@gmail.com"),
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
