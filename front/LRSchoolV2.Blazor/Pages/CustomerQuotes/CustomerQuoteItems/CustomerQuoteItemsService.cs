using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.GetCustomerQuoteItemsPerCustomerQuote;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.SaveCustomerQuoteItem;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CustomerQuotes;
using MediatR;
using Unit=LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CustomerQuotes.CustomerQuoteItems;

public class CustomerQuoteItemsService(
    ISender inMediator,
    IValidator<GetCustomerQuoteItemsPerCustomerQuoteRequest> inGetCustomerQuoteItemsPerCustomerQuoteRequestValidator,
    IValidator<SaveCustomerQuoteItemRequest> inSaveCustomerQuoteItemRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<CustomerQuoteItem>>> GetCustomerQuoteItemsPerCustomerQuoteAsync(Guid inCustomerQuoteId) => 
        (await inMediator.SendRequestWithValidation<GetCustomerQuoteItemsPerCustomerQuoteRequest, GetCustomerQuoteItemsPerCustomerQuoteQuery, GetCustomerQuoteItemsPerCustomerQuoteResponse>(new GetCustomerQuoteItemsPerCustomerQuoteRequest(inCustomerQuoteId), inGetCustomerQuoteItemsPerCustomerQuoteRequestValidator))
        .Map(inSuccess => inSuccess.CustomerQuoteItems);
    
    public Task<Validation<string, Unit>> SaveCustomerQuoteItemAsync(CustomerQuoteItem inCustomerQuoteItem) =>
        inMediator.SendRequestWithValidation<SaveCustomerQuoteItemRequest, SaveCustomerQuoteItemCommand>(new SaveCustomerQuoteItemRequest(inCustomerQuoteItem), inSaveCustomerQuoteItemRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveCustomerQuoteItemAsync(CustomerQuoteItem inCustomerQuoteItem)
    {
        var validationResult = await inSaveCustomerQuoteItemRequestValidator.ValidateAsync(new SaveCustomerQuoteItemRequest(inCustomerQuoteItem));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : Validation<string, Unit>.Success(default!);
    }
}