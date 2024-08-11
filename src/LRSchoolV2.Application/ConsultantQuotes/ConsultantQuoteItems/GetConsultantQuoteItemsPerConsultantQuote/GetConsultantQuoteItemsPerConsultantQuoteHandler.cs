using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.GetConsultantQuoteItemsPerConsultantQuote;

public class GetConsultantQuoteItemsPerConsultantQuoteHandler(
    IConsultantQuoteItemsRepository inConsultantQuoteItemsRepository
) : IRequestHandler<GetConsultantQuoteItemsPerConsultantQuoteQuery, GetConsultantQuoteItemsPerConsultantQuoteResponse>
{
    public async Task<GetConsultantQuoteItemsPerConsultantQuoteResponse> Handle(GetConsultantQuoteItemsPerConsultantQuoteQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inConsultantQuoteItemsRepository.GetConsultantQuoteItemsPerConsultantQuoteAsync(inRequest.ConsultantQuoteId));
}