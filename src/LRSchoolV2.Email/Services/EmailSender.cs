using System.Diagnostics.CodeAnalysis;
using LRSchoolV2.Email.Abstractions;
using LRSchoolV2.Email.Models;
using LRSchoolV2.Email.Models.Actors;
using LRSchoolV2.Email.Models.Attachments;
using MailKit.Net.Smtp;
using Mapster;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LRSchoolV2.Email.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfiguration;
    private readonly ISmtpClient _smtpClient;

    public EmailSender(IOptions<EmailConfiguration> inEmailConfiguration, ISmtpClient inSmtpClient)
    {
        _smtpClient = inSmtpClient;
        _smtpClient.ServerCertificateValidationCallback = (_,_,_,_) => true;
        _emailConfiguration = inEmailConfiguration.Value;
    }

    public Task SendEmailAsync(string inRecipient, EmailMessage inMessage, string? inAttachmentPath = null,
        Sender? inSender = null) =>
        SendEmailAsync(Recipient.FromEmail(inRecipient), inMessage, inAttachmentPath, inSender);

    public Task SendEmailAsync(string inRecipient, EmailMessage inMessage, EmailAttachment inAttachment,
        Sender? inSender = null) =>
        SendEmailAsync(Recipient.FromEmail(inRecipient), inMessage, inAttachment, inSender);

    public Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, string? inAttachmentPath = null,
        Sender? inSender = null) =>
        SendEmailAsync(inRecipients.Select(Recipient.FromEmail), inMessage, inAttachmentPath, inSender);

    public Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, EmailAttachment inAttachment,
        Sender? inSender = null) =>
        SendEmailAsync(inRecipients.Select(Recipient.FromEmail), inMessage, inAttachment, inSender);
    
    public Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, string? inAttachmentPath = null,
        Sender? inSender = null) =>
        SendEmailAsync(
            new[] { inRecipient }, 
            [], 
            [], 
            inMessage,
            string.IsNullOrWhiteSpace(inAttachmentPath) ? Enumerable.Empty<string>() : new[] { inAttachmentPath },
            inSender);
    
    public Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, EmailAttachment inAttachment,
        Sender? inSender = null) =>
        SendEmailAsync(
            new[] { inRecipient }, 
            [], 
            [], 
            inMessage,
            new[] { inAttachment },
            inSender);
    
    public Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, string? inAttachmentPath = null,
        Sender? inSender = null) =>
        SendEmailAsync(
            [], 
            [], 
            inRecipients, 
            inMessage,
            string.IsNullOrWhiteSpace(inAttachmentPath) ? Enumerable.Empty<string>() : new[] { inAttachmentPath },
            inSender);
    
    public Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, EmailAttachment inAttachment,
        Sender? inSender = null) =>
        SendEmailAsync(
            [], 
            [], 
            inRecipients, 
            inMessage,
            new[] { inAttachment },
            inSender);

    public Task SendEmailAsync(string inRecipient, EmailMessage inMessage, Sender inSender) =>
        SendEmailAsync(
            new[] { Recipient.FromEmail(inRecipient) }, 
            [], 
            [], 
            inMessage,
            Enumerable.Empty<string>(),
            inSender);

    public Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, Sender inSender) =>
        SendEmailAsync(
            inRecipients.Select(Recipient.FromEmail),
            inMessage,
            inSender);

    public Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, Sender inSender) =>
        SendEmailAsync(
            new[] { inRecipient }, 
            [], 
            [], 
            inMessage,
            Enumerable.Empty<string>(),
            inSender);

    public Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, Sender inSender) =>
        SendEmailAsync(
            [], 
            [], 
            inRecipients, 
            inMessage,
            Enumerable.Empty<string>(),
            inSender);

    public Task SendEmailAsync(string inRecipient, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null) =>
        SendEmailAsync(
            new[] { Recipient.FromEmail(inRecipient) }, 
            [], 
            [], 
            inMessage,
            inAttachmentsPath,
            inSender);

    public Task SendEmailAsync(string inRecipient, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null) =>
        SendEmailAsync(
            new[] { Recipient.FromEmail(inRecipient) }, 
            [], 
            [], 
            inMessage,
            inAttachments,
            inSender);

    public Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null) =>
        SendEmailAsync(
            inRecipients.Select(Recipient.FromEmail), 
            inMessage,
            inAttachmentsPath,
            inSender);

    public Task SendEmailAsync(IEnumerable<string> inRecipients, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null) =>
        SendEmailAsync(
            inRecipients.Select(Recipient.FromEmail), 
            inMessage,
            inAttachments,
            inSender);

    public Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath,
        Sender? inSender = null) =>
        SendEmailAsync(
            new[] { inRecipient }, 
            [], 
            [], 
            inMessage,
            inAttachmentsPath,
            inSender);

    public Task SendEmailAsync(Recipient inRecipient, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments,
        Sender? inSender = null) =>
        SendEmailAsync(
            new[] { inRecipient }, 
            [], 
            [], 
            inMessage,
            inAttachments,
            inSender);

    public Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath,
        Sender? inSender = null)
    {
        inRecipients = inRecipients.ToList();

        return SendEmailAsync(
            inRecipients.Count() <= 1 ? inRecipients : [], 
            [], 
            inRecipients.Count() > 1 ? inRecipients : [],
            inMessage,
            inAttachmentsPath,
            inSender);
    }

    public Task SendEmailAsync(IEnumerable<Recipient> inRecipients, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments,
        Sender? inSender = null)
    {
        inRecipients = inRecipients.ToList();

        return SendEmailAsync(
            inRecipients.Count() <= 1 ? inRecipients : [], 
            [], 
            inRecipients.Count() > 1 ? inRecipients : [],
            inMessage,
            inAttachments,
            inSender);
    }

    public Task SendEmailAsync(IEnumerable<Recipient> inToRecipients, IEnumerable<Recipient> inCcRecipients,
        IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage) =>
        SendEmailAsync(inToRecipients, inCcRecipients, inBccRecipients, inMessage, Enumerable.Empty<string>());
    
    public Task SendEmailAsync(IEnumerable<Recipient> inToRecipients, IEnumerable<Recipient> inCcRecipients,
        IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage, Sender inSender) =>
        SendEmailAsync(inToRecipients, inCcRecipients, inBccRecipients, inMessage, Enumerable.Empty<string>(), inSender);
    
    public async Task SendEmailAsync(IEnumerable<Recipient> inToRecipients,IEnumerable<Recipient> inCcRecipients, IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage, IEnumerable<string> inAttachmentsPath, Sender? inSender = null)
    {
        inSender ??= new Sender(_emailConfiguration.DefaultSenderName, _emailConfiguration.DefaultSenderAddress);
        
        await ConnectSmtpServer();
        await CreateBodyWithAttachmentAsync(inMessage, inAttachmentsPath);
        var theEmail = CreateEmail(inToRecipients, inCcRecipients, inBccRecipients, inMessage, inSender, inMessage.BodyBuilder!);
        await _smtpClient.SendAsync(theEmail);

        await _smtpClient.DisconnectAsync(true);
    }
    
    public async Task SendEmailAsync(IEnumerable<Recipient> inToRecipients,IEnumerable<Recipient> inCcRecipients, IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments, Sender? inSender = null)
    {
        inSender ??= new Sender(_emailConfiguration.DefaultSenderName, _emailConfiguration.DefaultSenderAddress);
        
        await ConnectSmtpServer();
        CreateBodyWithAttachment(inMessage, inAttachments);
        var theEmail = CreateEmail(inToRecipients, inCcRecipients, inBccRecipients, inMessage, inSender, inMessage.BodyBuilder!);
        await _smtpClient.SendAsync(theEmail);

        await _smtpClient.DisconnectAsync(true);
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private static MimeMessage CreateEmail(IEnumerable<Recipient> inToRecipients, IEnumerable<Recipient> inCcRecipients,
        IEnumerable<Recipient> inBccRecipients, EmailMessage inMessage, Sender inSender, BodyBuilder inBody)
    {
        var theMessage = new MimeMessage
        {
            Sender = inSender.Adapt<MailboxAddress>(),
            Subject = inMessage.Subject,
            Body = inBody.ToMessageBody()
        };
        theMessage.From.Add(inSender.Adapt<MailboxAddress>());
#if DEBUG
        theMessage.Subject = $"{string.Join(';', inToRecipients.Select(inRecipient => inRecipient.Address))} - {string.Join(';', inCcRecipients.Select(inRecipient => inRecipient.Address))} - {string.Join(';', inBccRecipients.Select(inRecipient => inRecipient.Address))} - {theMessage.Subject}";
        theMessage.To.Add(new Recipient("Robin Kebaili", "robin.kebaili@gmail.com").Adapt<MailboxAddress>());
#else
        theMessage.To.AddRange(inToRecipients.Adapt<IEnumerable<MailboxAddress>>());
        theMessage.Cc.AddRange(inCcRecipients.Adapt<IEnumerable<MailboxAddress>>());
        theMessage.Bcc.AddRange(inBccRecipients.Adapt<IEnumerable<MailboxAddress>>());
#endif
        return theMessage;
    }

    private static Task CreateBodyWithAttachmentAsync(EmailMessage inMessage, IEnumerable<string> inAttachmentsPath)
    {
        inMessage.BodyBuilder ??= new BodyBuilder
        {
            TextBody = inMessage.BodyType == BodyType.Plain ? inMessage.Body : null,
            HtmlBody = inMessage.BodyType == BodyType.Html ? inMessage.Body : null
        };

        return AddAttachments(inAttachmentsPath, inMessage.BodyBuilder);
    }

    private static void CreateBodyWithAttachment(EmailMessage inMessage, IEnumerable<EmailAttachment> inAttachments)
    {
        inMessage.BodyBuilder ??= new BodyBuilder
        {
            TextBody = inMessage.BodyType == BodyType.Plain ? inMessage.Body : null,
            HtmlBody = inMessage.BodyType == BodyType.Html ? inMessage.Body : null
        };
        
        AddAttachments(inAttachments, inMessage.BodyBuilder);
    }

    private static async Task AddAttachments(IEnumerable<string> inAttachmentsPath, BodyBuilder inBody)
    {
        foreach (var theAttachmentPath in inAttachmentsPath)
        {
            await inBody.Attachments.AddAsync(theAttachmentPath);
        }
    }

    private static void AddAttachments(IEnumerable<EmailAttachment> inAttachments, BodyBuilder inBody)
    {
        foreach (var theAttachment in inAttachments)
        {
            inBody.Attachments.Add(theAttachment.Filename, theAttachment.Content, theAttachment.Type.ContentType);
        }
    }

    private async Task ConnectSmtpServer()
    {
        await _smtpClient.ConnectAsync(_emailConfiguration.SmtpHost, _emailConfiguration.SmtpPort);

        if (!string.IsNullOrWhiteSpace(_emailConfiguration.SmtpUsername))
        {
            await _smtpClient.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
        }
    }
}