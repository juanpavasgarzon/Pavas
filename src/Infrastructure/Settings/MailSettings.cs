using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Settings;

public sealed class MailSettings
{
    public const string Path = "Mail";

    [Required(ErrorMessage = "Mail Host Is Required")]
    public string Host { get; set; }

    [Required(ErrorMessage = "Mail Port Is Required")]
    public int Port { get; set; }

    [Required(ErrorMessage = "Mail Username Is Required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Mail Password Is Required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Mail EnableSsl Is Required")]
    public bool EnableSsl { get; set; }
}
