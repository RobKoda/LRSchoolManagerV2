using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.GetCustomerInvoiceItemsPerCustomerInvoice;

public class GetCustomerInvoiceItemsPerCustomerInvoiceHandler : IRequestHandler<GetCustomerInvoiceItemsPerCustomerInvoiceQuery, GetCustomerInvoiceItemsPerCustomerInvoiceResponse>
{
    private readonly ICustomerInvoiceItemsRepository _customerInvoiceItemsRepository;
    
    public GetCustomerInvoiceItemsPerCustomerInvoiceHandler(ICustomerInvoiceItemsRepository inCustomerInvoiceItemsRepository) 
    {
        _customerInvoiceItemsRepository = inCustomerInvoiceItemsRepository;
    }

    public async Task<GetCustomerInvoiceItemsPerCustomerInvoiceResponse> Handle(GetCustomerInvoiceItemsPerCustomerInvoiceQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _customerInvoiceItemsRepository.GetCustomerInvoiceItemsPerCustomerInvoiceAsync(inRequest.CustomerInvoiceId));
}