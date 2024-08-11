using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.GetConsultantQuoteItemsPerConsultantQuote;
using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.SaveConsultantQuoteItem;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.ConsultantQuotes;
using MediatR;
using Unit=LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.ConsultantQuotes.ConsultantQuoteItems;

public class ConsultantQuoteItemsService(
    ISender inMediator,
    IValidator<GetConsultantQuoteItemsPerConsultantQuoteRequest> inGetConsultantQuoteItemsPerConsultantQuoteRequestValidator,
    IValidator<SaveConsultantQuoteItemRequest> inSaveConsultantQuoteItemRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<ConsultantQuoteItem>>> GetConsultantQuoteItemsPerConsultantQuoteAsync(Guid inConsultantQuoteId) => 
        (await inMediator.SendRequestWithValidation<GetConsultantQuoteItemsPerConsultantQuoteRequest, GetConsultantQuoteItemsPerConsultantQuoteQuery, GetConsultantQuoteItemsPerConsultantQuoteResponse>(new GetConsultantQuoteItemsPerConsultantQuoteRequest(inConsultantQuoteId), inGetConsultantQuoteItemsPerConsultantQuoteRequestValidator))
        .Map(inSuccess => inSuccess.ConsultantQuoteItems);
    
    public Task<Validation<string, Unit>> SaveConsultantQuoteItemAsync(ConsultantQuoteItem inConsultantQuoteItem) =>
        inMediator.SendRequestWithValidation<SaveConsultantQuoteItemRequest, SaveConsultantQuoteItemCommand>(new SaveConsultantQuoteItemRequest(inConsultantQuoteItem), inSaveConsultantQuoteItemRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveConsultantQuoteItemAsync(ConsultantQuoteItem inConsultantQuoteItem)
    {
        var validationResult = await inSaveConsultantQuoteItemRequestValidator.ValidateAsync(new SaveConsultantQuoteItemRequest(inConsultantQuoteItem));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : Validation<string, Unit>.Success(default!);
    }
}