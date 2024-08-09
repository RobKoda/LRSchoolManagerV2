using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SaveCustomerInvoice;

public class SaveCustomerInvoiceHandler(ICustomerInvoicesRepository inCustomerInvoicesRepository) : IRequestHandler<SaveCustomerInvoiceCommand>
{
    public Task Handle(SaveCustomerInvoiceCommand inRequest, CancellationToken inCancellationToken) => 
        inCustomerInvoicesRepository.SaveCustomerInvoiceAsync(inRequest.CustomerInvoice);
}