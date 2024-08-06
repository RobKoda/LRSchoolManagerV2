using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GetCustomerInvoices;

public class GetCustomerInvoicesHandler(ICustomerInvoicesRepository inCustomerInvoicesRepository) : IRequestHandler<GetCustomerInvoicesQuery, GetCustomerInvoicesResponse>
{
    public async Task<GetCustomerInvoicesResponse> Handle(GetCustomerInvoicesQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inCustomerInvoicesRepository.GetCustomerInvoicesAsync());
}