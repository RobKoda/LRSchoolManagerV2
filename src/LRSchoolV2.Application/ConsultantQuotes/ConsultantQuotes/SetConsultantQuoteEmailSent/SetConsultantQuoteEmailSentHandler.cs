using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SetConsultantQuoteEmailSent;

public class SetConsultantQuoteEmailSentHandler(IConsultantQuotesRepository inConsultantQuotesRepository) : IRequestHandler<SetConsultantQuoteEmailSentCommand>
{
    public async Task Handle(SetConsultantQuoteEmailSentCommand inRequest, CancellationToken inCancellationToken) =>
        await (await inConsultantQuotesRepository.GetConsultantQuoteAsync(inRequest.ConsultantQuoteId))
            .IfSomeAsync(async inQuote =>
            {
                inQuote = inQuote with { EmailSent = true };
                await inConsultantQuotesRepository.SaveConsultantQuoteAsync(inQuote);
            });
}