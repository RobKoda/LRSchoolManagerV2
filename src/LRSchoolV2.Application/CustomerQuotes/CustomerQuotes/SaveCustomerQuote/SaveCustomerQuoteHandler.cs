using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SaveCustomerQuote;

public class SaveCustomerQuoteHandler(ICustomerQuotesRepository inCustomerQuotesRepository) : IRequestHandler<SaveCustomerQuoteCommand>
{
    public Task Handle(SaveCustomerQuoteCommand inRequest, CancellationToken inCancellationToken) => 
        inCustomerQuotesRepository.SaveCustomerQuoteAsync(inRequest.CustomerQuote);
}