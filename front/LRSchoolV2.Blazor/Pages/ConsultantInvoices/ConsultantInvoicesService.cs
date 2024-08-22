using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.CancelConsultantInvoice;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GenerateConsultantInvoices;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GetConsultantInvoices;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SaveConsultantInvoice;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SetConsultantInvoiceEmailSent;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.ConsultantInvoices;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.ConsultantInvoices;

public class ConsultantInvoicesService(
    ISender inMediator, 
    IValidator<CancelConsultantInvoiceRequest> inCancelConsultantInvoiceRequestValidator, 
    IValidator<SetConsultantInvoiceEmailSentRequest> inSetConsultantInvoiceEmailSentRequestValidator,
    IValidator<SaveConsultantInvoiceRequest> inSaveConsultantInvoiceRequestValidator,
    IValidator<GenerateConsultantInvoicesRequest> inGenerateConsultantInvoiceRequestValidator
    ) : IFrontDataService
{
    public Task<GetConsultantInvoicesResponse> GetConsultantInvoicesAsync() => 
        inMediator.Send(new GetConsultantInvoicesQuery());

    public Task<Validation<string, Unit>> SetConsultantInvoiceEmailSentAsync(ConsultantInvoice inConsultantInvoice) => 
        inMediator.SendRequestWithValidation<SetConsultantInvoiceEmailSentRequest, SetConsultantInvoiceEmailSentCommand>(new SetConsultantInvoiceEmailSentRequest(inConsultantInvoice.Id), inSetConsultantInvoiceEmailSentRequestValidator);
    
    public Task<Validation<string, Unit>> CancelConsultantInvoiceAsync(ConsultantInvoice inConsultantInvoice) => 
        inMediator.SendRequestWithValidation<CancelConsultantInvoiceRequest, CancelConsultantInvoiceCommand>(new CancelConsultantInvoiceRequest(inConsultantInvoice), inCancelConsultantInvoiceRequestValidator);
    
    public Task<Validation<string, Unit>> SaveConsultantInvoiceAsync(ConsultantInvoice inConsultantInvoice) =>
        inMediator.SendRequestWithValidation<SaveConsultantInvoiceRequest, SaveConsultantInvoiceCommand>(new SaveConsultantInvoiceRequest(inConsultantInvoice), inSaveConsultantInvoiceRequestValidator);
    
    public async Task<Validation<string, Unit>> SimulateSaveConsultantInvoiceAsync(ConsultantInvoice inConsultantInvoice)
    {
        var validationResult = await inSaveConsultantInvoiceRequestValidator.ValidateAsync(new SaveConsultantInvoiceRequest(inConsultantInvoice));
        return !validationResult.IsValid ? 
            Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage))) : 
            Validation<string, Unit>.Success(default!);
    }
    
    public Task<Validation<string, Unit>> GenerateConsultantInvoicesAsync(IEnumerable<ConsultantInvoiceable> inConsultantInvoiceables) =>
        inMediator.SendRequestWithValidation<GenerateConsultantInvoicesRequest, GenerateConsultantInvoicesQuery>(new GenerateConsultantInvoicesRequest(inConsultantInvoiceables), inGenerateConsultantInvoiceRequestValidator);
}