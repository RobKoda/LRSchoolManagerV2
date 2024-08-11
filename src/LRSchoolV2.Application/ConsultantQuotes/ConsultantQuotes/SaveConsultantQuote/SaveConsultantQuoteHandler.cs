using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SaveConsultantQuote;

public class SaveConsultantQuoteHandler(IConsultantQuotesRepository inConsultantQuotesRepository) : IRequestHandler<SaveConsultantQuoteCommand>
{
    public Task Handle(SaveConsultantQuoteCommand inRequest, CancellationToken inCancellationToken) => 
        inConsultantQuotesRepository.SaveConsultantQuoteAsync(inRequest.ConsultantQuote);
}