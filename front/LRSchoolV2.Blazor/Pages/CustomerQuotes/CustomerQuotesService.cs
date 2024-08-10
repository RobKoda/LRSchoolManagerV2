using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.CancelCustomerQuote;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.GetCustomerQuotes;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SaveCustomerQuote;
using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SetCustomerQuoteEmailSent;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CustomerQuotes;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CustomerQuotes;

public class CustomerQuotesService(
    ISender inMediator, 
    IValidator<CancelCustomerQuoteRequest> inCancelCustomerQuoteRequestValidator, 
    IValidator<SetCustomerQuoteEmailSentRequest> inSetCustomerQuoteEmailSentRequestValidator,
    IValidator<SaveCustomerQuoteRequest> inSaveCustomerQuoteRequestValidator
    ) : IFrontDataService
{
    public Task<GetCustomerQuotesResponse> GetCustomerQuotesAsync() => 
        inMediator.Send(new GetCustomerQuotesQuery());

    public Task<Validation<string, Unit>> SetCustomerQuoteEmailSentAsync(CustomerQuote inCustomerQuote) => 
        inMediator.SendRequestWithValidation<SetCustomerQuoteEmailSentRequest, SetCustomerQuoteEmailSentCommand>(new SetCustomerQuoteEmailSentRequest(inCustomerQuote.Id), inSetCustomerQuoteEmailSentRequestValidator);
    
    public Task<Validation<string, Unit>> CancelCustomerQuoteAsync(CustomerQuote inCustomerQuote) => 
        inMediator.SendRequestWithValidation<CancelCustomerQuoteRequest, CancelCustomerQuoteCommand>(new CancelCustomerQuoteRequest(inCustomerQuote), inCancelCustomerQuoteRequestValidator);
    
    public Task<Validation<string, Unit>> SaveCustomerQuoteAsync(CustomerQuote inCustomerQuote) =>
        inMediator.SendRequestWithValidation<SaveCustomerQuoteRequest, SaveCustomerQuoteCommand>(new SaveCustomerQuoteRequest(inCustomerQuote), inSaveCustomerQuoteRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveCustomerQuoteAsync(CustomerQuote inCustomerQuote)
    {
        var validationResult = await inSaveCustomerQuoteRequestValidator.ValidateAsync(new SaveCustomerQuoteRequest(inCustomerQuote));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : 
            Validation<string, Unit>.Success(default!);
    }
}