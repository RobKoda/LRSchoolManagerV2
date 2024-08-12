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
using LRSchoolV2.Reporting.Customers.CustomerInvoices;
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
        var customerInvoiceables = inRequest.CustomerInvoiceables.ToList();

        await GenerateAndIterateOnCustomerInvoicesAsync(customerInvoiceables, SimulateGeneration);
        if (_saveCustomerInvoiceValidationErrors.Any() || _saveCustomerInvoiceItemValidationErrors.Any())
        {
            return new GenerateCustomerInvoicesResponse(Validation<string, Unit>.Fail(_saveCustomerInvoiceValidationErrors.Concat(_saveCustomerInvoiceItemValidationErrors)));
        }
        
        await GenerateAndIterateOnCustomerInvoicesAsync(customerInvoiceables, Generate);

        await SetFullyBilledAsync(customerInvoiceables);
        
        return new GenerateCustomerInvoicesResponse(Validation<string, Unit>.Success(Unit.Default));
    }

    private async Task<IEnumerable<CustomerInvoice>> GetInvoicesAsync() =>
        (await inMediator.Send(new GetCustomerInvoicesQuery())).CustomerInvoices;

    private async Task GenerateAndIterateOnCustomerInvoicesAsync(IList<CustomerInvoiceable> inCustomerInvoiceables, Func<CustomerInvoice, IList<CustomerInvoiceItem>, Task> inFunction)
    {
        var customers = inCustomerInvoiceables.Select(inCustomerInvoiceable => inCustomerInvoiceable.Person)
            .OrderBy(inPerson => inPerson.GetFullName())
            .Distinct();
        var nonBilledPersonServiceVariations = (await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariations()).ToList();
        var unpaidPersonServiceVariationBilledItems = (await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariationBilledItems()).ToList();
        
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
            
            var items = new List<CustomerInvoiceItem>();
            foreach (var customerInvoiceable in inCustomerInvoiceables)
            {
                items.Add(new CustomerInvoiceItem(
                    Guid.NewGuid(),
                    customerInvoice,
                    customerInvoiceable.ReferenceId,
                    1,
                    customerInvoiceable.Denomination,
                    GetCustomerInvoiceItemPrice(customerInvoiceable, nonBilledPersonServiceVariations, unpaidPersonServiceVariationBilledItems),
                    items.Count + 1
                ));
            }

            await inFunction(customerInvoice, items);
        }
    }

    private static decimal GetCustomerInvoiceItemPrice(CustomerInvoiceable inCustomerInvoiceable, IEnumerable<PersonAnnualServiceVariation> inNonBilledPersonServiceVariations, IEnumerable<CustomerInvoiceItem> inUnpaidPersonServiceVariationBilledItems)
    {
        if (inCustomerInvoiceable.CustomerInvoiceableReferenceType == CustomerInvoiceableReferenceType.PersonRegistration)
        {
            return inCustomerInvoiceable.Price;
        }

        // ReSharper disable once InvertIf - Nope.
        if (inCustomerInvoiceable.CustomerInvoiceableReferenceType == CustomerInvoiceableReferenceType.PersonAnnualServiceVariation)
        {
            var personServiceVariation = inNonBilledPersonServiceVariations.Single(inVariation => inVariation.Id == inCustomerInvoiceable.ReferenceId);
            return inCustomerInvoiceable.CompletePayment ? 
                inCustomerInvoiceable.Price - PersonAnnualServiceVariation.GetAlreadyBilled(inUnpaidPersonServiceVariationBilledItems, personServiceVariation) : 
                Math.Round(inCustomerInvoiceable.Price / inCustomerInvoiceable.PaymentsCount, 2);
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

    private async Task SetFullyBilledAsync(IEnumerable<CustomerInvoiceable> inCustomerInvoiceables)
    {
        var fullyBilledCustomerInvoiceables = inCustomerInvoiceables.Where(inCustomerInvoiceable => inCustomerInvoiceable.CompletePayment).ToList();
        var fullyBilledPersonRegistrations = fullyBilledCustomerInvoiceables.Where(inCustomerInvoiceable => inCustomerInvoiceable.CustomerInvoiceableReferenceType == CustomerInvoiceableReferenceType.PersonRegistration);
        var fullyBilledPersonServiceVariations = fullyBilledCustomerInvoiceables.Where(inCustomerInvoiceable => inCustomerInvoiceable.CustomerInvoiceableReferenceType == CustomerInvoiceableReferenceType.PersonAnnualServiceVariation);

        await SetPersonRegistrationsFullyBilled(fullyBilledPersonRegistrations);
        await SetPersonServiceVariationsFullyBilled(fullyBilledPersonServiceVariations);
    }

    private Task SetPersonRegistrationsFullyBilled(IEnumerable<CustomerInvoiceable> inFullyBilledPersonRegistrations) => 
        inPersonRegistrationsRepository.SetFullyBilledAsync(inFullyBilledPersonRegistrations.Select(inCustomerInvoiceable => inCustomerInvoiceable.ReferenceId));

    private Task SetPersonServiceVariationsFullyBilled(IEnumerable<CustomerInvoiceable> inFullyBilledPersonServiceVariations) => 
        inPersonAnnualServiceVariationsRepository.SetFullyBilledAsync(inFullyBilledPersonServiceVariations.Select(inCustomerInvoiceable => inCustomerInvoiceable.ReferenceId));
}