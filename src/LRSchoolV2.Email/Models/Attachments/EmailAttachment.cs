// ReSharper disable ClassNeverInstantiated.Global - Instantiation through library usage
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Assignations through library usage
// ReSharper disable PropertyCanBeMadeInitOnly.Global - May be used externally
namespace LRSchoolV2.Email.Models.Attachments;

public class EmailAttachment
{
    public string Filename { get; set; } = string.Empty;
    public byte[] Content { get; set; } = [];
    public EmailAttachmentType Type { get; set; } = EmailAttachmentType.Pdf;
}
