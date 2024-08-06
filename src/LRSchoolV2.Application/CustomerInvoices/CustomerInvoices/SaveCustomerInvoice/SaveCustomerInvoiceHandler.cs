using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SaveCustomerInvoice;

public class SaveCustomerInvoiceHandler : IRequestHandler<SaveCustomerInvoiceCommand>
{
    private readonly ICustomerInvoicesRepository _customerInvoicesRepository;

    public SaveCustomerInvoiceHandler(ICustomerInvoicesRepository inCustomerInvoicesRepository)
    {
        _customerInvoicesRepository = inCustomerInvoicesRepository;
    }

    public Task Handle(SaveCustomerInvoiceCommand inRequest, CancellationToken inCancellationToken) => 
        _customerInvoicesRepository.SaveCustomerInvoiceAsync(inRequest.CustomerInvoice);
}