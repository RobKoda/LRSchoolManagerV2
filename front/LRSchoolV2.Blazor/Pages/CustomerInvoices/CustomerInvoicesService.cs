using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.CancelCustomerInvoice;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GenerateCustomerInvoices;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GetCustomerInvoices;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SetCustomerInvoiceEmailSent;
using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices;

public class CustomerInvoicesService(ISender inMediator, IValidator<CancelCustomerInvoiceRequest> inCancelCustomerInvoiceRequestValidator, IValidator<SetCustomerInvoiceEmailSentRequest> inSetCustomerInvoiceEmailSentRequestValidator)
{
    public Task<GetCustomerInvoicesResponse> GetCustomerInvoicesAsync() => 
        inMediator.Send(new GetCustomerInvoicesQuery());

    public async Task<Validation<string, Unit>> GenerateCustomerInvoicesAsync(IEnumerable<Payable> inPayables) =>
        (await inMediator.Send(new GenerateCustomerInvoicesQuery(inPayables))).Validation;

    public Task<Validation<string, Unit>> SetCustomerInvoiceEmailSentAsync(CustomerInvoice inCustomerInvoice) => 
        inMediator.SendRequestWithValidation<SetCustomerInvoiceEmailSentRequest, SetCustomerInvoiceEmailSentCommand>(new SetCustomerInvoiceEmailSentRequest(inCustomerInvoice.Id), inSetCustomerInvoiceEmailSentRequestValidator);
    
    public Task<Validation<string, Unit>> CancelInvoiceAsync(CustomerInvoice inCustomerInvoice) => 
        inMediator.SendRequestWithValidation<CancelCustomerInvoiceRequest, CancelCustomerInvoiceCommand>(new CancelCustomerInvoiceRequest(inCustomerInvoice), inCancelCustomerInvoiceRequestValidator);
}