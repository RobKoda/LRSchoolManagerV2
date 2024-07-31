// ReSharper disable UnusedMember.Global - Intended use is external
// ReSharper disable UnusedMemberInSuper.Global - Intended use is external

using LRSchoolV2.Email.Models;
using LRSchoolV2.Email.Models.Actors;
using LRSchoolV2.Email.Models.Attachments;

namespace LRSchoolV2.Email.Abstractions;

public interface IEmailSender
{
    Task SendEmailAsync(string inRecipient, EmailMessage inMessage, string? inAttachmentPath = null, Sender? inSender = null);
    Task SendEmailAsync(string inRecipient, EmailMessage inMessage, EmailAttachment inAttachment, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, string? inAttachmentPath = null, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, EmailAttachment inAttachment, Sender? inSender = null);
    Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, string? inAttachmentPath = null, Sender? inSender = null);
    Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, EmailAttachment inAttachment, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, string? inAttachmentPath = null, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, EmailAttachment inAttachment, Sender? inSender = null);
    Task SendEmailAsync(string inRecipient, EmailMessage inMessage, Sender inSender);
    Task SendEmailAsync(IEnumerable<string> inRecipient, EmailMessage inMessage, Sender inSender);
    Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, Sender inSender);
    Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, Sender inSender);
    Task SendEmailAsync(string inRecipient, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null);
    Task SendEmailAsync(string inRecipient, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null);
    Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null);
    Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<Recipient> inToRecipients,IEnumerable<Recipient> inCcRecipients, IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage);
    Task SendEmailAsync(IEnumerable<Recipient> inToRecipients,IEnumerable<Recipient> inCcRecipients, IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage, Sender inSender);
    Task SendEmailAsync(IEnumerable<Recipient> inToRecipients,IEnumerable<Recipient> inCcRecipients, IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null);
    Task SendEmailAsync(IEnumerable<Recipient> inToRecipients,IEnumerable<Recipient> inCcRecipients, IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null);
}