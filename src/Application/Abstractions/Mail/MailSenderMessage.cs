namespace Application.Abstractions.Mail;

public sealed class MailSenderMessage
{
    public string Subject { get; set; }
    public string Message { get; set; }
    public string[] Recipients { get; set; }
}
