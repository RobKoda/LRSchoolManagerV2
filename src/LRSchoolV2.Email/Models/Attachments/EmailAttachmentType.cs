using Ardalis.SmartEnum;
using MimeKit;

// ReSharper disable UnusedMember.Global - Available for library

namespace LRSchoolV2.Email.Models.Attachments;

public class EmailAttachmentType : SmartEnum<EmailAttachmentType>
{
    public static readonly EmailAttachmentType Pdf = new(nameof(Pdf), 1, new ContentType("application", "pdf"));

    public ContentType ContentType { get; }
    
    private EmailAttachmentType(string inName, int inValue, ContentType inContentType) : base(inName, inValue)
    {
        ContentType = inContentType;
    }
}