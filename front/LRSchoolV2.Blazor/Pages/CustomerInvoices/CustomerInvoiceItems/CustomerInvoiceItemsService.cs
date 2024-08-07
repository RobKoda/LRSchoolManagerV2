using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.GetCustomerInvoiceItemsPerCustomerInvoice;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.CustomerInvoiceItems;

public class CustomerInvoiceItemsService(
    ISender inMediator,
    IValidator<GetCustomerInvoiceItemsPerCustomerInvoiceRequest> inGetCustomerInvoiceItemsPerCustomerInvoiceRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<CustomerInvoiceItem>>> GetCustomerInvoiceItemsPerCustomerInvoiceAsync(Guid inCustomerInvoiceId)
    {
        var request = new GetCustomerInvoiceItemsPerCustomerInvoiceRequest(inCustomerInvoiceId);
        var result = await inMediator.SendRequestWithValidation<GetCustomerInvoiceItemsPerCustomerInvoiceRequest, GetCustomerInvoiceItemsPerCustomerInvoiceQuery, GetCustomerInvoiceItemsPerCustomerInvoiceResponse>(request, inGetCustomerInvoiceItemsPerCustomerInvoiceRequestValidator);
        return result.Map(inSuccess => inSuccess.CustomerInvoiceItems);
    }
}