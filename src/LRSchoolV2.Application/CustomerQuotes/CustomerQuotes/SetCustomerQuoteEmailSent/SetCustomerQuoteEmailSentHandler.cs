using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SetCustomerQuoteEmailSent;

public class SetCustomerQuoteEmailSentHandler(ICustomerQuotesRepository inCustomerQuotesRepository) : IRequestHandler<SetCustomerQuoteEmailSentCommand>
{
    public async Task Handle(SetCustomerQuoteEmailSentCommand inRequest, CancellationToken inCancellationToken) =>
        await (await inCustomerQuotesRepository.GetCustomerQuoteAsync(inRequest.CustomerQuoteId))
            .IfSomeAsync(async inQuote =>
            {
                inQuote = inQuote with { EmailSent = true };
                await inCustomerQuotesRepository.SaveCustomerQuoteAsync(inQuote);
            });
}