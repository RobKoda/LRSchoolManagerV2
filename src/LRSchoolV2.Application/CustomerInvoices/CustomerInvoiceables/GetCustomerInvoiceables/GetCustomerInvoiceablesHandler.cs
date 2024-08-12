using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceables.GetCustomerInvoiceables;

public class GetCustomerInvoiceablesHandler(
    IPersonRegistrationsRepository inPersonRegistrationsRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository,
    IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository
    ) : IRequestHandler<GetCustomerInvoiceablesQuery, GetCustomerInvoiceablesResponse>
{
    public async Task<GetCustomerInvoiceablesResponse> Handle(GetCustomerInvoiceablesQuery inRequest, CancellationToken inCancellationToken)
    {
        var result = new List<CustomerInvoiceable>();

        await GetPersonRegistrationsCustomerInvoiceables(result);
        await GetPersonServiceVariationsCustomerInvoiceables(result);

        return new GetCustomerInvoiceablesResponse(result);
    }

    private async Task GetPersonRegistrationsCustomerInvoiceables(List<CustomerInvoiceable> inResult)
    {
        var personRegistrations = await inPersonRegistrationsRepository.GetNonBilledPersonRegistrations();
        inResult.AddRange(personRegistrations.Select(inPersonRegistration =>
            new CustomerInvoiceable(
                CustomerInvoiceableReferenceType.PersonRegistration,
                inPersonRegistration.Id,
                GetPersonRegistrationBilledPerson(inPersonRegistration),
                $"Cotisation annuelle - {inPersonRegistration.SchoolYear.GetPeriodDisplay()} - {inPersonRegistration.Person.GetFullName()} ",
                inPersonRegistration.SchoolYear.MembershipFee,
                0,
                0,
                1,
                true)));
    }

    private static Person GetPersonRegistrationBilledPerson(PersonRegistration inPersonRegistration) =>
        inPersonRegistration.BilledPerson ??
        (inPersonRegistration.Person.BillingToContactPerson1 ? inPersonRegistration.Person.ContactPerson1! : inPersonRegistration.Person);

    private async Task GetPersonServiceVariationsCustomerInvoiceables(List<CustomerInvoiceable> inResult)
    {
        var serviceVariationYearlyPrices = await inAnnualServiceVariationYearlyPricesRepository.GetAnnualServiceVariationYearlyPricesAsync();
        var nonBilledPersonServiceVariations = await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariations();
        var nonBilledPersonServiceVariationPayments = (await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariationBilledItems()).ToList();

        inResult.AddRange(nonBilledPersonServiceVariations.Select(inPersonServiceVariation =>
            new CustomerInvoiceable(
                CustomerInvoiceableReferenceType.PersonAnnualServiceVariation,
                inPersonServiceVariation.Id,
                GetPersonServiceVariationBilledPerson(inPersonServiceVariation),
                GetPersonServiceVariationDenomination(nonBilledPersonServiceVariationPayments, inPersonServiceVariation),
                serviceVariationYearlyPrices.Single(inYearlyPrice =>
                    inYearlyPrice.AnnualServiceVariationId == inPersonServiceVariation.AnnualServiceVariation.Id &&
                    inYearlyPrice.SchoolYear.Id == inPersonServiceVariation.SchoolYear.Id).Price,
                PersonAnnualServiceVariation.GetAlreadyBilled(nonBilledPersonServiceVariationPayments, inPersonServiceVariation),
                PersonAnnualServiceVariation.GetBilledPaymentsCount(nonBilledPersonServiceVariationPayments, inPersonServiceVariation),
                inPersonServiceVariation.PaymentsCount,
                PersonAnnualServiceVariation.GetBilledPaymentsCount(nonBilledPersonServiceVariationPayments, inPersonServiceVariation) == inPersonServiceVariation.PaymentsCount - 1)));
    }

    private static Person GetPersonServiceVariationBilledPerson(PersonAnnualServiceVariation inPersonAnnualServiceVariation) =>
        inPersonAnnualServiceVariation.BilledPerson ??
        (inPersonAnnualServiceVariation.Person.BillingToContactPerson1 ? inPersonAnnualServiceVariation.Person.ContactPerson1! : inPersonAnnualServiceVariation.Person);

    private static string GetPersonServiceVariationDenomination(IEnumerable<CustomerInvoiceItem> inNonBilledPersonServiceVariationPayments, PersonAnnualServiceVariation inPersonAnnualServiceVariation)
    {
        var denomination = $"{inPersonAnnualServiceVariation.AnnualServiceVariation.InvoiceName} - {inPersonAnnualServiceVariation.Person.GetFullName()}";

        if (inPersonAnnualServiceVariation.PaymentsCount > 1)
        {
            denomination += $" - {PersonAnnualServiceVariation.GetBilledPaymentsCount(inNonBilledPersonServiceVariationPayments, inPersonAnnualServiceVariation) + 1}/{inPersonAnnualServiceVariation.PaymentsCount}";
        }

        return denomination;
    }
}