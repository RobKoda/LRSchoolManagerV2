using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.SaveCustomerInvoiceItem;

public class SaveCustomerInvoiceItemHandler : IRequestHandler<SaveCustomerInvoiceItemCommand>
{
    private readonly ICustomerInvoiceItemsRepository _customerInvoiceItemsRepository;

    public SaveCustomerInvoiceItemHandler(ICustomerInvoiceItemsRepository inCustomerInvoiceItemsRepository)
    {
        _customerInvoiceItemsRepository = inCustomerInvoiceItemsRepository;
    }

    public Task Handle(SaveCustomerInvoiceItemCommand inRequest, CancellationToken inCancellationToken) => 
        _customerInvoiceItemsRepository.SaveCustomerInvoiceItemAsync(inRequest.CustomerInvoiceItem);
}