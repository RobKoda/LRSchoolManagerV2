using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.CancelConsultantQuote;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.GetConsultantQuotes;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SaveConsultantQuote;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SetConsultantQuoteEmailSent;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.ConsultantQuotes;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.ConsultantQuotes;

public class ConsultantQuotesService(
    ISender inMediator, 
    IValidator<CancelConsultantQuoteRequest> inCancelConsultantQuoteRequestValidator, 
    IValidator<SetConsultantQuoteEmailSentRequest> inSetConsultantQuoteEmailSentRequestValidator,
    IValidator<SaveConsultantQuoteRequest> inSaveConsultantQuoteRequestValidator
    ) : IFrontDataService
{
    public Task<GetConsultantQuotesResponse> GetConsultantQuotesAsync() => 
        inMediator.Send(new GetConsultantQuotesQuery());

    public Task<Validation<string, Unit>> SetConsultantQuoteEmailSentAsync(ConsultantQuote inConsultantQuote) => 
        inMediator.SendRequestWithValidation<SetConsultantQuoteEmailSentRequest, SetConsultantQuoteEmailSentCommand>(new SetConsultantQuoteEmailSentRequest(inConsultantQuote.Id), inSetConsultantQuoteEmailSentRequestValidator);
    
    public Task<Validation<string, Unit>> CancelConsultantQuoteAsync(ConsultantQuote inConsultantQuote) => 
        inMediator.SendRequestWithValidation<CancelConsultantQuoteRequest, CancelConsultantQuoteCommand>(new CancelConsultantQuoteRequest(inConsultantQuote), inCancelConsultantQuoteRequestValidator);
    
    public Task<Validation<string, Unit>> SaveConsultantQuoteAsync(ConsultantQuote inConsultantQuote) =>
        inMediator.SendRequestWithValidation<SaveConsultantQuoteRequest, SaveConsultantQuoteCommand>(new SaveConsultantQuoteRequest(inConsultantQuote), inSaveConsultantQuoteRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveConsultantQuoteAsync(ConsultantQuote inConsultantQuote)
    {
        var validationResult = await inSaveConsultantQuoteRequestValidator.ValidateAsync(new SaveConsultantQuoteRequest(inConsultantQuote));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : 
            Validation<string, Unit>.Success(default!);
    }
}