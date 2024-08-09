using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.GetCustomerInvoiceItemsPerCustomerInvoice;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.SaveCustomerInvoiceItem;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;
using Unit=LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.CustomerInvoiceItems;

public class CustomerInvoiceItemsService(
    ISender inMediator,
    IValidator<GetCustomerInvoiceItemsPerCustomerInvoiceRequest> inGetCustomerInvoiceItemsPerCustomerInvoiceRequestValidator,
    IValidator<SaveCustomerInvoiceItemRequest> inSaveCustomerInvoiceItemRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<CustomerInvoiceItem>>> GetCustomerInvoiceItemsPerCustomerInvoiceAsync(Guid inCustomerInvoiceId) => 
        (await inMediator.SendRequestWithValidation<GetCustomerInvoiceItemsPerCustomerInvoiceRequest, GetCustomerInvoiceItemsPerCustomerInvoiceQuery, GetCustomerInvoiceItemsPerCustomerInvoiceResponse>(new GetCustomerInvoiceItemsPerCustomerInvoiceRequest(inCustomerInvoiceId), inGetCustomerInvoiceItemsPerCustomerInvoiceRequestValidator))
        .Map(inSuccess => inSuccess.CustomerInvoiceItems);
    
    public Task<Validation<string, Unit>> SaveCustomerInvoiceItemAsync(CustomerInvoiceItem inCustomerInvoiceItem) =>
        inMediator.SendRequestWithValidation<SaveCustomerInvoiceItemRequest, SaveCustomerInvoiceItemCommand>(new SaveCustomerInvoiceItemRequest(inCustomerInvoiceItem), inSaveCustomerInvoiceItemRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveCustomerInvoiceItemAsync(CustomerInvoiceItem inCustomerInvoiceItem)
    {
        var validationResult = await inSaveCustomerInvoiceItemRequestValidator.ValidateAsync(new SaveCustomerInvoiceItemRequest(inCustomerInvoiceItem));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : Validation<string, Unit>.Success(default!);
    }
}