using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Settings;

public class JwtSettings
{
    public const string Path = "Jwt";

    [Required(ErrorMessage = "Jwt Secret Is Required")]
    public string Secret { get; set; }

    [Required(ErrorMessage = "Jwt Issuer Is Required")]
    public string Issuer { get; set; }

    [Required(ErrorMessage = "Jwt Audience Is Required")]
    public string Audience { get; set; }

    [Required(ErrorMessage = "Jwt ExpirationInMinutes Is Required")]
    public int ExpirationInMinutes { get; set; }
}
