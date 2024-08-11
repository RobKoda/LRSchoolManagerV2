using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.SaveConsultantQuoteItem;

public class SaveConsultantQuoteItemHandler(
    IConsultantQuoteItemsRepository inConsultantQuoteItemsRepository
) : IRequestHandler<SaveConsultantQuoteItemCommand>
{
    public Task Handle(SaveConsultantQuoteItemCommand inRequest, CancellationToken inCancellationToken) => 
        inConsultantQuoteItemsRepository.SaveConsultantQuoteItemAsync(inRequest.ConsultantQuoteItem);
}