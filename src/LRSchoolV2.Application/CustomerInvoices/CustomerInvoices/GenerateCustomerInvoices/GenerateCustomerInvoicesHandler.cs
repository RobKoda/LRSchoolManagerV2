using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Common.Documents.SaveDocument;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.SaveCustomerInvoiceItem;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GetCustomerInvoices;
using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SaveCustomerInvoice;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Reporting.CustomerInvoices;
using MediatR;
using Unit = LanguageExt.Unit;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GenerateCustomerInvoices;

public class GenerateCustomerInvoicesHandler(
    ISender inMediator,
    IValidator<SaveCustomerInvoiceRequest> inSaveCustomerInvoiceRequestValidation,
    IValidator<SaveCustomerInvoiceItemRequest> inSaveCustomerInvoiceItemRequestValidation,
    IPersonRegistrationsRepository inPersonRegistrationsRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository)
    : IRequestHandler<GenerateCustomerInvoicesQuery, GenerateCustomerInvoicesResponse>
{
    private Seq<string> _saveCustomerInvoiceValidationErrors;
    private Seq<string> _saveCustomerInvoiceItemValidationErrors;

    public async Task<GenerateCustomerInvoicesResponse> Handle(GenerateCustomerInvoicesQuery inRequest, CancellationToken inCancellationToken)
    {
        var payables = inRequest.Payables.ToList();

        await GenerateAndIterateOnCustomerInvoicesAsync(payables, SimulateGeneration);
        if (_saveCustomerInvoiceValidationErrors.Any() || _saveCustomerInvoiceItemValidationErrors.Any())
        {
            return new GenerateCustomerInvoicesResponse(Validation<string, Unit>.Fail(_saveCustomerInvoiceValidationErrors.Concat(_saveCustomerInvoiceItemValidationErrors)));
        }
        
        await GenerateAndIterateOnCustomerInvoicesAsync(payables, Generate);

        await SetFullyBilledAsync(payables);
        
        return new GenerateCustomerInvoicesResponse(Validation<string, Unit>.Success(Unit.Default));
    }

    private async Task<IEnumerable<CustomerInvoice>> GetInvoicesAsync() =>
        (await inMediator.Send(new GetCustomerInvoicesQuery())).CustomerInvoices;

    private async Task GenerateAndIterateOnCustomerInvoicesAsync(IList<Payable> inPayables, Func<CustomerInvoice, IList<CustomerInvoiceItem>, Task> inFunction)
    {
        var customers = inPayables.Select(inPayable => inPayable.Person)
            .OrderBy(inPerson => inPerson.GetFullName())
            .Distinct();
        var nonBilledPersonServiceVariations = await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariations();
        var unpaidPersonServiceVariationBilledItems = await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariationBilledItems();
        
        foreach (var customer in customers)
        {
            var generationDate = DateTime.Now;
            var customerInvoice = new CustomerInvoice(
                Guid.NewGuid(),
                CustomerInvoice.GetInvoiceNumber(generationDate, await GetInvoicesAsync()),
                generationDate,
                customer,
                customer.GetFullName(),
                customer.Address.GetFormattedAddress(),
                0,
                false
            );

            var items = inPayables.Where(inPayable => inPayable.Person == customer)
                .Select(inPayable => new CustomerInvoiceItem(
                    Guid.NewGuid(), 
                    customerInvoice,
                    inPayable.ReferenceId,
                    1, 
                    inPayable.Denomination, 
                    GetCustomerInvoiceItemPrice(inPayable, nonBilledPersonServiceVariations, unpaidPersonServiceVariationBilledItems)))
                .ToList();

            await inFunction(customerInvoice, items);
        }
    }

    private static decimal GetCustomerInvoiceItemPrice(Payable inPayable, IEnumerable<PersonAnnualServiceVariation> inNonBilledPersonServiceVariations, IEnumerable<CustomerInvoiceItem> inUnpaidPersonServiceVariationBilledItems)
    {
        if (inPayable.PayableReferenceType == PayableReferenceType.PersonRegistration)
        {
            return inPayable.Price;
        }

        // ReSharper disable once InvertIf - Nope.
        if (inPayable.PayableReferenceType == PayableReferenceType.PersonServiceVariation)
        {
            var personServiceVariation = inNonBilledPersonServiceVariations.Single(inVariation => inVariation.Id == inPayable.ReferenceId);
            return inPayable.CompletePayment ? 
                inPayable.Price - PersonAnnualServiceVariation.GetAlreadyBilled(inUnpaidPersonServiceVariationBilledItems, personServiceVariation) : 
                Math.Round(inPayable.Price / inPayable.PaymentsCount, 2);
        }

        throw new InvalidOperationException("Reference type not found!");
    }

    private async Task SimulateGeneration(CustomerInvoice inCustomerInvoice, IEnumerable<CustomerInvoiceItem> inItems)
    {
        await SimulateSaveCustomerInvoiceAsync(inCustomerInvoice);
        foreach (var customerInvoiceItem in inItems)
        {
            await SimulateSaveCustomerInvoiceItemAsync(customerInvoiceItem);
        }
    }

    private async Task SimulateSaveCustomerInvoiceAsync(CustomerInvoice inCustomerInvoice)
    {
        var saveCustomerInvoiceRequest = new SaveCustomerInvoiceRequest(inCustomerInvoice);
        var validationResult = await inSaveCustomerInvoiceRequestValidation.ValidateAsync(saveCustomerInvoiceRequest);
        if (!validationResult.IsValid)
        {
            _saveCustomerInvoiceValidationErrors += new Seq<string>(validationResult.Errors.Skip(_saveCustomerInvoiceValidationErrors.Count()).Select(inError => inError.ErrorMessage));
        }
    }

    private async Task SimulateSaveCustomerInvoiceItemAsync(CustomerInvoiceItem inCustomerInvoiceItem)
    {
        var saveCustomerInvoiceItemRequest = new SaveCustomerInvoiceItemRequest(inCustomerInvoiceItem);
        var validationResult = await inSaveCustomerInvoiceItemRequestValidation.ValidateAsync(saveCustomerInvoiceItemRequest);
        if (!validationResult.IsValid)
        {
            _saveCustomerInvoiceItemValidationErrors += new Seq<string>(validationResult.Errors
                .Select(inError => inError.ErrorMessage)
                .Where(inError => !string.Equals(inError, SaveCustomerInvoiceItemRequestValidationErrors.CustomerInvoiceNotFound))
                .Skip(_saveCustomerInvoiceValidationErrors.Count()));
        }
    }

    private async Task Generate(CustomerInvoice inCustomerInvoice, IList<CustomerInvoiceItem> inItems)
    {
        await inMediator.Send(new SaveCustomerInvoiceCommand(inCustomerInvoice));
        foreach (var customerInvoiceItem in inItems)
        {
            await inMediator.Send(new SaveCustomerInvoiceItemCommand(customerInvoiceItem));
        }
        await GenerateDocument(inCustomerInvoice, inItems);
    }

    private async Task GenerateDocument(CustomerInvoice inCustomerInvoice, IEnumerable<CustomerInvoiceItem> inItems)
    {
        var report = new CustomerInvoiceReport(inCustomerInvoice, inItems);
        var reportContent = (await report.GetReportMemoryStreamAsync()).ToArray();
        var document = new Document(Guid.NewGuid(), inCustomerInvoice.Id, report.ExportFileName, CustomerInvoiceReport.ContentType, reportContent);
        await inMediator.Send(new SaveDocumentCommand(document));
    }

    private async Task SetFullyBilledAsync(IEnumerable<Payable> inPayables)
    {
        var fullyBilledPayables = inPayables.Where(inPayable => inPayable.CompletePayment).ToList();
        var fullyBilledPersonRegistrations = fullyBilledPayables.Where(inPayable => inPayable.PayableReferenceType == PayableReferenceType.PersonRegistration);
        var fullyBilledPersonServiceVariations = fullyBilledPayables.Where(inPayable => inPayable.PayableReferenceType == PayableReferenceType.PersonServiceVariation);

        await SetPersonRegistrationsFullyBilled(fullyBilledPersonRegistrations);
        await SetPersonServiceVariationsFullyBilled(fullyBilledPersonServiceVariations);
    }

    private Task SetPersonRegistrationsFullyBilled(IEnumerable<Payable> inFullyBilledPersonRegistrations) => 
        inPersonRegistrationsRepository.SetFullyBilledAsync(inFullyBilledPersonRegistrations.Select(inPayable => inPayable.ReferenceId));

    private Task SetPersonServiceVariationsFullyBilled(IEnumerable<Payable> inFullyBilledPersonServiceVariations) => 
        inPersonAnnualServiceVariationsRepository.SetFullyBilledAsync(inFullyBilledPersonServiceVariations.Select(inPayable => inPayable.ReferenceId));
}