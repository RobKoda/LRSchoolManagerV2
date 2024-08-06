using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.GetCustomerInvoiceItemsPerCustomerInvoice;
using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.CustomerInvoiceItems;

public class CustomerInvoiceItemsService
{
    private readonly ISender _mediator;
    private readonly IValidator<GetCustomerInvoiceItemsPerCustomerInvoiceRequest> _getCustomerInvoiceItemsPerCustomerInvoiceRequestValidator;

    public CustomerInvoiceItemsService(ISender inMediator, 
        IValidator<GetCustomerInvoiceItemsPerCustomerInvoiceRequest> inGetCustomerInvoiceItemsPerCustomerInvoiceRequestValidator)
    {
        _mediator = inMediator;
        _getCustomerInvoiceItemsPerCustomerInvoiceRequestValidator = inGetCustomerInvoiceItemsPerCustomerInvoiceRequestValidator;
    }

    public async Task<Validation<string, IEnumerable<CustomerInvoiceItem>>> GetCustomerInvoiceItemsPerCustomerInvoiceAsync(Guid inCustomerInvoiceId)
    {
        var request = new GetCustomerInvoiceItemsPerCustomerInvoiceRequest(inCustomerInvoiceId);
        var result = await _mediator.SendRequestWithValidation<GetCustomerInvoiceItemsPerCustomerInvoiceRequest, GetCustomerInvoiceItemsPerCustomerInvoiceQuery, GetCustomerInvoiceItemsPerCustomerInvoiceResponse>(request, _getCustomerInvoiceItemsPerCustomerInvoiceRequestValidator);
        return result.Map(inSuccess => inSuccess.CustomerInvoiceItems);
    }
}