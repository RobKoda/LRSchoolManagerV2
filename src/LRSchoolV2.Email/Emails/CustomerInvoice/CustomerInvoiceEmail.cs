using LRSchoolV2.Email.Models;
using MimeKit;
using MimeKit.Utils;

namespace LRSchoolV2.Email.Emails.CustomerInvoice;

public class CustomerInvoiceEmail : EmailMessage
{
    public CustomerInvoiceEmail(string inInvoiceNumber) // Formerly Domain.CustomerInvoices.CustomerInvoice inCustomerInvoice
    {
        BodyBuilder = new BodyBuilder();
        var logo = GetType().Assembly.GetManifestResourceStream("LRSchoolV2.Email.Emails.logo.png");
        var image = BodyBuilder.LinkedResources.Add("LR School Logo.png", logo);
        image.ContentId = MimeUtils.GenerateMessageId();

        BodyType = BodyType.Html;
        Subject = $"Facture {inInvoiceNumber}";
        BodyBuilder.HtmlBody =
            "<p>Bonjour,</p>" +
            $"<p>Veuillez trouver ci-joint la facture {inInvoiceNumber}.</p>" +
            "<p>Cordialement,<br/>" +
            "L'équipe LR School</p>" +
            $"""<img style="width: 200px" src="cid:{image.ContentId}"><br/>""" +
            "<p><i>Cet email a été généré automatiquement. Pour toute question, contactez contact@lrschool.com</i></p>";
    }
}