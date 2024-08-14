using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceables.GetConsultantInvoiceables;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.ConsultantInvoices;
using MediatR;

namespace LRSchoolV2.Blazor.Pages.ConsultantInvoices;

public class ConsultantInvoiceablesService(ISender inMediator) : IFrontDataService
{
    public async Task<IEnumerable<ConsultantInvoiceable>> GetConsultantInvoiceablesAsync() => 
        (await inMediator.Send(new GetConsultantInvoiceablesQuery())).ConsultantInvoiceables;
}