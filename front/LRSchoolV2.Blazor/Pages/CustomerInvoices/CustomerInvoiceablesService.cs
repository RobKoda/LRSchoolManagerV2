using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceables.GetCustomerInvoiceables;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices;

public class CustomerInvoiceablesService(ISender inMediator) : IFrontDataService
{
    public async Task<IEnumerable<CustomerInvoiceable>> GetCustomerInvoiceablesAsync() => 
        (await inMediator.Send(new GetCustomerInvoiceablesQuery())).CustomerInvoiceables;
}