using LRSchoolV2.Application.Common.Documents.Persistence;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.Persistence;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.CancelCustomerInvoice;

public class CancelCustomerInvoiceHandler(
    ICustomerInvoicesRepository inCustomerInvoicesRepository,
    ICustomerInvoiceItemsRepository inCustomerInvoiceItemsRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository,
    IPersonRegistrationsRepository inPersonRegistrationsRepository,
    IDocumentsRepository inDocumentsRepository
    ) : IRequestHandler<CancelCustomerInvoiceCommand>
{
    public async Task Handle(CancelCustomerInvoiceCommand inRequest, CancellationToken inCancellationToken)
    {
        await inDocumentsRepository.DeleteDocumentPerReferenceIdAsync(inRequest.CustomerInvoice.Id);
        
        var items = await inCustomerInvoiceItemsRepository.GetCustomerInvoiceItemsPerCustomerInvoiceAsync(inRequest.CustomerInvoice.Id);
        foreach (var item in items)
        {
            await item.ReferenceId.IfSomeAsync(async inReferenceId =>
            {
                await inPersonRegistrationsRepository.SetFullyBilledAsync(new[] { inReferenceId }, false);
                await inPersonAnnualServiceVariationsRepository.SetFullyBilledAsync(new[] { inReferenceId }, false);
            });
            
            await inCustomerInvoiceItemsRepository.DeleteCustomerInvoiceItemAsync(item.Id);
        }
        await inCustomerInvoicesRepository.DeleteCustomerInvoiceAsync(inRequest.CustomerInvoice.Id);
    }
}