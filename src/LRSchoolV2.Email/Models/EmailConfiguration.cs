// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use through DI options
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
namespace LRSchoolV2.Email.Models;

public class EmailConfiguration
{
    public string SmtpHost { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public string DefaultSenderName { get; set; } = string.Empty;
    public string DefaultSenderAddress { get; set; } = string.Empty;
}