using LRSchoolV2.Application.CustomerInvoices.Payables.GetPayables;
using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices;

public class PayablesService(ISender inMediator)
{
    public async Task<IEnumerable<Payable>> GetPayablesAsync() => 
        (await inMediator.Send(new GetPayablesQuery())).Payables;
}