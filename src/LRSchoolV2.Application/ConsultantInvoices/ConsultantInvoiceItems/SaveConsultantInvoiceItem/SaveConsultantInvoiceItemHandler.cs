using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.SaveConsultantInvoiceItem;

public class SaveConsultantInvoiceItemHandler(
    IConsultantInvoiceItemsRepository inConsultantInvoiceItemsRepository
) : IRequestHandler<SaveConsultantInvoiceItemCommand>
{
    public Task Handle(SaveConsultantInvoiceItemCommand inRequest, CancellationToken inCancellationToken) => 
        inConsultantInvoiceItemsRepository.SaveConsultantInvoiceItemAsync(inRequest.ConsultantInvoiceItem);
}