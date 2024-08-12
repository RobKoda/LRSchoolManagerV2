using LRSchoolV2.Application.Common.Documents.Persistence;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.Persistence;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.CancelConsultantInvoice;

public class CancelConsultantInvoiceHandler(
    IConsultantInvoicesRepository inConsultantInvoicesRepository,
    IConsultantInvoiceItemsRepository inConsultantInvoiceItemsRepository,
    IDocumentsRepository inDocumentsRepository
    ) : IRequestHandler<CancelConsultantInvoiceCommand>
{
    public async Task Handle(CancelConsultantInvoiceCommand inRequest, CancellationToken inCancellationToken)
    {
        await inDocumentsRepository.DeleteDocumentPerReferenceIdAsync(inRequest.ConsultantInvoice.Id);
        
        var items = await inConsultantInvoiceItemsRepository.GetConsultantInvoiceItemsPerConsultantInvoiceAsync(inRequest.ConsultantInvoice.Id);
        foreach (var item in items)
        {
            await inConsultantInvoiceItemsRepository.DeleteConsultantInvoiceItemAsync(item.Id);
        }
        await inConsultantInvoicesRepository.DeleteConsultantInvoiceAsync(inRequest.ConsultantInvoice.Id);
    }
}