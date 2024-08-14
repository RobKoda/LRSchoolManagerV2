using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Consultants;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceables.GetConsultantInvoiceables;

public class GetConsultantInvoiceablesHandler(
    IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository,
    IAnnualServiceConsultantWorksRepository inAnnualServiceConsultantWorksRepository,
    IAnnualServiceVariationConsultantWorksRepository inAnnualServiceVariationConsultantWorksRepository
    ) : IRequestHandler<GetConsultantInvoiceablesQuery, GetConsultantInvoiceablesResponse>
{
    private IEnumerable<AnnualServiceConsultantWork> _annualServiceConsultantWorks = [];
    private IEnumerable<AnnualServiceVariationConsultantWork> _annualServiceVariationConsultantWorks = [];
    
    public async Task<GetConsultantInvoiceablesResponse> Handle(GetConsultantInvoiceablesQuery inRequest, CancellationToken inCancellationToken)
    {
        var result = new List<ConsultantInvoiceable>();
        
        await GetPersonAnnualServiceVariationsInvoiceables(result);
        
        return new GetConsultantInvoiceablesResponse(result);
    }

    private async Task GetPersonAnnualServiceVariationsInvoiceables(ICollection<ConsultantInvoiceable> inResult)
    {
        _annualServiceConsultantWorks = await inAnnualServiceConsultantWorksRepository.GetAnnualServiceConsultantWorksAsync();
        _annualServiceVariationConsultantWorks = await inAnnualServiceVariationConsultantWorksRepository.GetAnnualServiceVariationConsultantWorksAsync();
        
        var nonBilledPersonServiceVariations = await inPersonAnnualServiceVariationsRepository.GetConsultantNonBilledPersonAnnualServiceVariations();
        var variationsGrouping = nonBilledPersonServiceVariations
            .GroupBy(inVariation => new {inVariation.AnnualServiceVariation, inVariation.SchoolYear});
        foreach (var variationGroup in variationsGrouping)
        {
            var consultants = GetConsultantsNonBilledVariation(variationGroup.Key.SchoolYear, variationGroup.Key.AnnualServiceVariation);
            foreach (var consultant in consultants)
            {
                inResult.Add(new ConsultantInvoiceable(
                    ConsultantInvoiceableReferenceType.PersonAnnualServiceVariation,
                    variationGroup.Key.SchoolYear,
                    consultant,
                    variationGroup.Key.AnnualServiceVariation.Id,
                    variationGroup.Key.AnnualServiceVariation.GetFullName(),
                    0,
                    0,
                    0,
                    0,
                    false
                ));
            }
        }
    }
    
    private IEnumerable<Consultant> GetConsultantsNonBilledVariation(SchoolYear inSchoolYear, AnnualServiceVariation inAnnualServiceVariation) =>
        _annualServiceConsultantWorks
            .Where(inWork => inWork.SchoolYear.Id == inSchoolYear.Id && inWork.AnnualService.Id == inAnnualServiceVariation.AnnualService.Id)
            .Select(inWork => inWork.Consultant)
            .Append(_annualServiceVariationConsultantWorks
                .Where(inWork => inWork.SchoolYear.Id == inSchoolYear.Id && inWork.AnnualServiceVariationId == inAnnualServiceVariation.Id)
                .Select(inWork => inWork.Consultant))
            .Distinct();
}