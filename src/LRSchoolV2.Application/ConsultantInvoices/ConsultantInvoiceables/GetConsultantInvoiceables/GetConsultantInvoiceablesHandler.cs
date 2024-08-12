using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceables.GetConsultantInvoiceables;

public class GetConsultantInvoiceablesHandler(
    IAnnualServiceConsultantWorksRepository inAnnualServiceConsultantWorksRepository,
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository,
    IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository
    ) : IRequestHandler<GetConsultantInvoiceablesQuery, GetConsultantInvoiceablesResponse>
{
    public async Task<GetConsultantInvoiceablesResponse> Handle(GetConsultantInvoiceablesQuery inRequest, CancellationToken inCancellationToken)
    {
        var result = new List<ConsultantInvoiceable>();

        // TODO
        //await GetPersonServiceVariationsConsultantInvoiceables(result);

        return new GetConsultantInvoiceablesResponse(result);
    }

    /*
    private async Task GetPersonServiceVariationsConsultantInvoiceables(List<ConsultantInvoiceable> inResult)
    {
        var serviceVariationYearlyPrices = await inAnnualServiceVariationYearlyPricesRepository.GetAnnualServiceVariationYearlyPricesAsync();
        var nonBilledPersonServiceVariations = await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariations();
        var nonBilledPersonServiceVariationPayments = (await inPersonAnnualServiceVariationsRepository.GetNonBilledPersonAnnualServiceVariationBilledItems()).ToList();

        inResult.AddRange(nonBilledPersonServiceVariations.Select(inPersonServiceVariation =>
            new ConsultantInvoiceable(
                ConsultantInvoiceableReferenceType.PersonAnnualServiceVariation,
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

    private static string GetPersonServiceVariationDenomination(IEnumerable<ConsultantInvoiceItem> inNonBilledPersonServiceVariationPayments, PersonAnnualServiceVariation inPersonAnnualServiceVariation)
    {
        var denomination = $"{inPersonAnnualServiceVariation.AnnualServiceVariation.InvoiceName} - {inPersonAnnualServiceVariation.Person.GetFullName()}";

        if (inPersonAnnualServiceVariation.PaymentsCount > 1)
        {
            denomination += $" - {PersonAnnualServiceVariation.GetBilledPaymentsCount(inNonBilledPersonServiceVariationPayments, inPersonAnnualServiceVariation) + 1}/{inPersonAnnualServiceVariation.PaymentsCount}";
        }

        return denomination;
    }*/
}