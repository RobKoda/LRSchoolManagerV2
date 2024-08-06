using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.Payables.GetPayables;

public class GetPayablesHandler(
    IPersonRegistrationsRepository inPersonRegistrationsRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository,
    IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository
    ) : IRequestHandler<GetPayablesQuery, GetPayablesResponse>
{
    public async Task<GetPayablesResponse> Handle(GetPayablesQuery inRequest, CancellationToken inCancellationToken)
    {
        var result = new List<Payable>();

        await GetPersonRegistrationsPayables(result);
        await GetPersonServiceVariationsPayables(result);

        return new GetPayablesResponse(result);
    }

    private async Task GetPersonRegistrationsPayables(List<Payable> inResult)
    {
        var personRegistrations = await inPersonRegistrationsRepository.GetNonBilledPersonRegistrations();
        inResult.AddRange(personRegistrations.Select(inPersonRegistration =>
            new Payable(PayableReferenceType.PersonRegistration,
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

    private async Task GetPersonServiceVariationsPayables(List<Payable> inResult)
    {
        var serviceVariationYearlyPrices = await inAnnualServiceVariationYearlyPricesRepository.GetAnnualServiceVariationYearlyPricesAsync();
        var nonBilledPersonServiceVariations = await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariations();
        var nonBilledPersonServiceVariationPayments = (await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariationBilledItems()).ToList();

        inResult.AddRange(nonBilledPersonServiceVariations.Select(inPersonServiceVariation =>
            new Payable(PayableReferenceType.PersonServiceVariation,
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