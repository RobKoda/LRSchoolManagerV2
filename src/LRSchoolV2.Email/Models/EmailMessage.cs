// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Property setters used externally
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
// ReSharper disable MemberCanBeProtected.Global - May be used externally

using MimeKit;

namespace LRSchoolV2.Email.Models;

public class EmailMessage
{
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public BodyType BodyType { get; set; } = BodyType.Plain;
    public BodyBuilder? BodyBuilder { get; set; }
}