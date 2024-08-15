using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Common.Documents.SaveDocument;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.SaveConsultantInvoiceItem;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GetConsultantInvoices;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SaveConsultantInvoice;
using LRSchoolV2.Application.Consultants.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Reporting.Consultants.ConsultantInvoices;
using MediatR;
using Unit = LanguageExt.Unit;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GenerateConsultantInvoices;

public class GenerateConsultantInvoicesHandler(
    ISender inMediator,
    IValidator<SaveConsultantInvoiceRequest> inSaveConsultantInvoiceRequestValidation,
    IValidator<SaveConsultantInvoiceItemRequest> inSaveConsultantInvoiceItemRequestValidation,
    IConsultantsRepository inConsultantsRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository)
    : IRequestHandler<GenerateConsultantInvoicesQuery, GenerateConsultantInvoicesResponse>
{
    private Seq<string> _saveConsultantInvoiceValidationErrors;
    private Seq<string> _saveConsultantInvoiceItemValidationErrors;

    public async Task<GenerateConsultantInvoicesResponse> Handle(GenerateConsultantInvoicesQuery inRequest, CancellationToken inCancellationToken)
    {
        var consultantInvoiceables = inRequest.ConsultantInvoiceables.ToList();

        await GenerateAndIterateOnConsultantInvoicesAsync(consultantInvoiceables, SimulateGeneration);
        if (_saveConsultantInvoiceValidationErrors.Any() || _saveConsultantInvoiceItemValidationErrors.Any())
        {
            return new GenerateConsultantInvoicesResponse(Validation<string, Unit>.Fail(_saveConsultantInvoiceValidationErrors.Concat(_saveConsultantInvoiceItemValidationErrors)));
        }
        
        await GenerateAndIterateOnConsultantInvoicesAsync(consultantInvoiceables, Generate);

        await SetFullyBilledAsync(consultantInvoiceables);
        
        return new GenerateConsultantInvoicesResponse(Validation<string, Unit>.Success(Unit.Default));
    }

    private async Task<IEnumerable<ConsultantInvoice>> GetInvoicesAsync() =>
        (await inMediator.Send(new GetConsultantInvoicesQuery())).ConsultantInvoices;

    private async Task GenerateAndIterateOnConsultantInvoicesAsync(IEnumerable<ConsultantInvoiceable> inConsultantInvoiceables, Func<ConsultantInvoice, IList<ConsultantInvoiceItem>, Task> inFunction)
    {
        inConsultantInvoiceables = inConsultantInvoiceables.ToList();
        
        var consultants = inConsultantInvoiceables.Distinct().Select(inInvoiceable => inInvoiceable.Consultant);
        var nonBilledPersonServiceVariations = (await inPersonAnnualServiceVariationsRepository.GetConsultantNonBilledPersonAnnualServiceVariations()).ToList();
        var unpaidPersonServiceVariationBilledItems = (await inPersonAnnualServiceVariationsRepository.GetConsultantNonBilledPersonAnnualServiceVariationBilledItems()).ToList();
        
        foreach (var consultant in consultants)
        {
            var generationDate = DateTime.Now;
            var consultantInvoice = new ConsultantInvoice(
                Guid.NewGuid(),
                ConsultantInvoice.GetInvoiceNumber(generationDate, await GetInvoicesAsync(), consultant.Id),
                generationDate,
                consultant,
                consultant.GetFullName(),
                consultant.Address.GetFormattedAddress(),
                0,
                false
            );
            
            var items = new List<ConsultantInvoiceItem>();
            foreach (var consultantInvoiceable in inConsultantInvoiceables)
            {
                items.Add(new ConsultantInvoiceItem(
                    Guid.NewGuid(),
                    consultantInvoice,
                    consultantInvoiceable.SchoolYear,
                    consultantInvoiceable.ReferenceId,
                    1, // TODO
                    consultantInvoiceable.Denomination,
                    GetConsultantInvoiceItemPrice(consultantInvoiceable, nonBilledPersonServiceVariations, unpaidPersonServiceVariationBilledItems),
                    items.Count + 1
                ));
            }

            await inFunction(consultantInvoice, items);
        }
    }

    private static decimal GetConsultantInvoiceItemPrice(ConsultantInvoiceable inConsultantInvoiceable, IEnumerable<PersonAnnualServiceVariation> inNonBilledPersonServiceVariations, IEnumerable<ConsultantInvoiceItem> inUnpaidPersonServiceVariationBilledItems)
    {
        // TODO
        return 0m;
        /*
        // ReSharper disable once InvertIf - Nope.
        if (inConsultantInvoiceable.ConsultantInvoiceableReferenceType == ConsultantInvoiceableReferenceType.PersonAnnualServiceVariation)
        {
            var personServiceVariation = inNonBilledPersonServiceVariations.Single(inVariation => inVariation.Id == inConsultantInvoiceable.ReferenceId);
            return inConsultantInvoiceable.CompletePayment ? 
                inConsultantInvoiceable.Price - AnnualServiceVariation.GetConsultantAlreadyBilled(inUnpaidPersonServiceVariationBilledItems, personServiceVariation) : 
                Math.Round(inConsultantInvoiceable.Price / inConsultantInvoiceable.PaymentsCount, 2);
        }

        throw new InvalidOperationException("Reference type not found!");
        */
    }

    private async Task SimulateGeneration(ConsultantInvoice inConsultantInvoice, IEnumerable<ConsultantInvoiceItem> inItems)
    {
        await SimulateSaveConsultantInvoiceAsync(inConsultantInvoice);
        foreach (var consultantInvoiceItem in inItems)
        {
            await SimulateSaveConsultantInvoiceItemAsync(consultantInvoiceItem);
        }
    }

    private async Task SimulateSaveConsultantInvoiceAsync(ConsultantInvoice inConsultantInvoice)
    {
        var saveConsultantInvoiceRequest = new SaveConsultantInvoiceRequest(inConsultantInvoice);
        var validationResult = await inSaveConsultantInvoiceRequestValidation.ValidateAsync(saveConsultantInvoiceRequest);
        if (!validationResult.IsValid)
        {
            _saveConsultantInvoiceValidationErrors += new Seq<string>(validationResult.Errors.Skip(_saveConsultantInvoiceValidationErrors.Count()).Select(inError => inError.ErrorMessage));
        }
    }

    private async Task SimulateSaveConsultantInvoiceItemAsync(ConsultantInvoiceItem inConsultantInvoiceItem)
    {
        var saveConsultantInvoiceItemRequest = new SaveConsultantInvoiceItemRequest(inConsultantInvoiceItem);
        var validationResult = await inSaveConsultantInvoiceItemRequestValidation.ValidateAsync(saveConsultantInvoiceItemRequest);
        if (!validationResult.IsValid)
        {
            _saveConsultantInvoiceItemValidationErrors += new Seq<string>(validationResult.Errors
                .Select(inError => inError.ErrorMessage)
                .Where(inError => !string.Equals(inError, SaveConsultantInvoiceItemRequestValidationErrors.ConsultantInvoiceNotFound))
                .Skip(_saveConsultantInvoiceValidationErrors.Count()));
        }
    }

    private async Task Generate(ConsultantInvoice inConsultantInvoice, IList<ConsultantInvoiceItem> inItems)
    {
        await inMediator.Send(new SaveConsultantInvoiceCommand(inConsultantInvoice));
        foreach (var consultantInvoiceItem in inItems)
        {
            await inMediator.Send(new SaveConsultantInvoiceItemCommand(consultantInvoiceItem));
        }
        await GenerateDocument(inConsultantInvoice, inItems);
    }

    private async Task GenerateDocument(ConsultantInvoice inConsultantInvoice, IEnumerable<ConsultantInvoiceItem> inItems)
    {
        var documentContent = await inConsultantsRepository.GetConsultantInvoiceDocument(inConsultantInvoice.Consultant.Id);
        await documentContent.IfSomeAsync(async inSome =>
        {
            var report = new ConsultantInvoiceReport(inConsultantInvoice, inItems, inSome);
            var reportContent = (await report.GetReportMemoryStreamAsync()).ToArray();
            var document = new Document(Guid.NewGuid(), inConsultantInvoice.Id, report.ExportFileName, ConsultantInvoiceReport.ContentType, reportContent);
            await inMediator.Send(new SaveDocumentCommand(document));
        });
    }

    private async Task SetFullyBilledAsync(IEnumerable<ConsultantInvoiceable> inConsultantInvoiceables)
    {
        var fullyBilledConsultantInvoiceables = inConsultantInvoiceables.Where(inConsultantInvoiceable => inConsultantInvoiceable.CompletePayment).ToList();
        var fullyBilledPersonServiceVariations = fullyBilledConsultantInvoiceables.Where(inConsultantInvoiceable => inConsultantInvoiceable.ConsultantInvoiceableReferenceType == ConsultantInvoiceableReferenceType.PersonAnnualServiceVariation);

        await SetPersonServiceVariationsFullyBilled(fullyBilledPersonServiceVariations);
    }

    private Task SetPersonServiceVariationsFullyBilled(IEnumerable<ConsultantInvoiceable> inFullyBilledPersonServiceVariations) => 
        inPersonAnnualServiceVariationsRepository.SetConsultantFullyBilledAsync(inFullyBilledPersonServiceVariations.Select(inConsultantInvoiceable => inConsultantInvoiceable.ReferenceId));
}