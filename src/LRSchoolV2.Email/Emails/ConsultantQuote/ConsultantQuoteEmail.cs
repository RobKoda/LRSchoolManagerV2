using LRSchoolV2.Email.Models;
using MimeKit;
using MimeKit.Utils;

namespace LRSchoolV2.Email.Emails.ConsultantQuote;

public class ConsultantQuoteEmail : EmailMessage
{
    public ConsultantQuoteEmail(string inQuoteNumber)
    {
        BodyBuilder = new BodyBuilder();
        var logo = GetType().Assembly.GetManifestResourceStream("LRSchoolV2.Email.Emails.logo.png");
        var image = BodyBuilder.LinkedResources.Add("LR School Logo.png", logo);
        image.ContentId = MimeUtils.GenerateMessageId();

        BodyType = BodyType.Html;
        Subject = $"Devis {inQuoteNumber}";
        BodyBuilder.HtmlBody =
            "<p>Bonjour,</p>" +
            $"<p>Veuillez trouver ci-joint le devis {inQuoteNumber}.</p>" +
            "<p>Cordialement,<br/>" +
            "L'équipe LR School</p>" +
            $"""<img style="width: 200px" src="cid:{image.ContentId}"><br/>""" +
            "<p><i>Cet email a été généré automatiquement. Pour toute question, contactez contact@lrschool.com</i></p>";
    }
}