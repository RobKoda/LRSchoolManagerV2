using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.CancelCustomerInvoice;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GenerateCustomerInvoices;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GetCustomerInvoices;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SaveCustomerInvoice;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SetCustomerInvoiceEmailSent;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices;

public class CustomerInvoicesService(
    ISender inMediator, 
    IValidator<CancelCustomerInvoiceRequest> inCancelCustomerInvoiceRequestValidator, 
    IValidator<SetCustomerInvoiceEmailSentRequest> inSetCustomerInvoiceEmailSentRequestValidator,
    IValidator<SaveCustomerInvoiceRequest> inSaveCustomerInvoiceRequestValidator
    ) : IFrontDataService
{
    public Task<GetCustomerInvoicesResponse> GetCustomerInvoicesAsync() => 
        inMediator.Send(new GetCustomerInvoicesQuery());

    public async Task<Validation<string, Unit>> GenerateCustomerInvoicesAsync(IEnumerable<Payable> inPayables) =>
        (await inMediator.Send(new GenerateCustomerInvoicesQuery(inPayables))).Validation;

    public Task<Validation<string, Unit>> SetCustomerInvoiceEmailSentAsync(CustomerInvoice inCustomerInvoice) => 
        inMediator.SendRequestWithValidation<SetCustomerInvoiceEmailSentRequest, SetCustomerInvoiceEmailSentCommand>(new SetCustomerInvoiceEmailSentRequest(inCustomerInvoice.Id), inSetCustomerInvoiceEmailSentRequestValidator);
    
    public Task<Validation<string, Unit>> CancelCustomerInvoiceAsync(CustomerInvoice inCustomerInvoice) => 
        inMediator.SendRequestWithValidation<CancelCustomerInvoiceRequest, CancelCustomerInvoiceCommand>(new CancelCustomerInvoiceRequest(inCustomerInvoice), inCancelCustomerInvoiceRequestValidator);
    
    public Task<Validation<string, Unit>> SaveCustomerInvoiceAsync(CustomerInvoice inCustomerInvoice) =>
        inMediator.SendRequestWithValidation<SaveCustomerInvoiceRequest, SaveCustomerInvoiceCommand>(new SaveCustomerInvoiceRequest(inCustomerInvoice), inSaveCustomerInvoiceRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveCustomerInvoiceAsync(CustomerInvoice inCustomerInvoice)
    {
        var validationResult = await inSaveCustomerInvoiceRequestValidator.ValidateAsync(new SaveCustomerInvoiceRequest(inCustomerInvoice));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : 
            Validation<string, Unit>.Success(default!);
    }
}