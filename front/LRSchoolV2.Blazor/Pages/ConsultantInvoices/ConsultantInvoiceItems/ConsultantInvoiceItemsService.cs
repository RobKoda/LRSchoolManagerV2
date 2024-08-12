using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.GetConsultantInvoiceItemsPerConsultantInvoice;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.SaveConsultantInvoiceItem;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.ConsultantInvoices;
using MediatR;
using Unit=LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.ConsultantInvoices.ConsultantInvoiceItems;

public class ConsultantInvoiceItemsService(
    ISender inMediator,
    IValidator<GetConsultantInvoiceItemsPerConsultantInvoiceRequest> inGetConsultantInvoiceItemsPerConsultantInvoiceRequestValidator,
    IValidator<SaveConsultantInvoiceItemRequest> inSaveConsultantInvoiceItemRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<ConsultantInvoiceItem>>> GetConsultantInvoiceItemsPerConsultantInvoiceAsync(Guid inConsultantInvoiceId) => 
        (await inMediator.SendRequestWithValidation<GetConsultantInvoiceItemsPerConsultantInvoiceRequest, GetConsultantInvoiceItemsPerConsultantInvoiceQuery, GetConsultantInvoiceItemsPerConsultantInvoiceResponse>(new GetConsultantInvoiceItemsPerConsultantInvoiceRequest(inConsultantInvoiceId), inGetConsultantInvoiceItemsPerConsultantInvoiceRequestValidator))
        .Map(inSuccess => inSuccess.ConsultantInvoiceItems);
    
    public Task<Validation<string, Unit>> SaveConsultantInvoiceItemAsync(ConsultantInvoiceItem inConsultantInvoiceItem) =>
        inMediator.SendRequestWithValidation<SaveConsultantInvoiceItemRequest, SaveConsultantInvoiceItemCommand>(new SaveConsultantInvoiceItemRequest(inConsultantInvoiceItem), inSaveConsultantInvoiceItemRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveConsultantInvoiceItemAsync(ConsultantInvoiceItem inConsultantInvoiceItem)
    {
        var validationResult = await inSaveConsultantInvoiceItemRequestValidator.ValidateAsync(new SaveConsultantInvoiceItemRequest(inConsultantInvoiceItem));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : Validation<string, Unit>.Success(default!);
    }
}