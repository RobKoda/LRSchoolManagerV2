using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.Persistence;
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
    IAnnualServiceVariationConsultantWorksRepository inAnnualServiceVariationConsultantWorksRepository,
    IAnnualServiceVariationYearlyPricesRepository inAnnualServiceVariationYearlyPricesRepository,
    IConsultantInvoiceItemsRepository inConsultantInvoiceItemsRepository
) : IRequestHandler<GetConsultantInvoiceablesQuery, GetConsultantInvoiceablesResponse>
{
    private IEnumerable<AnnualServiceConsultantWork> _annualServiceConsultantWorks = [];
    private IEnumerable<AnnualServiceVariationConsultantWork> _annualServiceVariationConsultantWorks = [];
    private IEnumerable<AnnualServiceVariationYearlyPrice> _annualServiceVariationYearlyPrices = [];
    
    public async Task<GetConsultantInvoiceablesResponse> Handle(GetConsultantInvoiceablesQuery inRequest, CancellationToken inCancellationToken)
    {
        var result = new List<ConsultantInvoiceable>();
        
        await GetPersonAnnualServiceVariationsInvoiceables(result);
        
        return new GetConsultantInvoiceablesResponse(result);
    }
    
    private async Task GetPersonAnnualServiceVariationsInvoiceables(ICollection<ConsultantInvoiceable> inResult)
    {
        _annualServiceConsultantWorks = (await inAnnualServiceConsultantWorksRepository.GetAnnualServiceConsultantWorksAsync()).ToList();
        _annualServiceVariationConsultantWorks = (await inAnnualServiceVariationConsultantWorksRepository.GetAnnualServiceVariationConsultantWorksAsync()).ToList();
        _annualServiceVariationYearlyPrices = await inAnnualServiceVariationYearlyPricesRepository.GetAnnualServiceVariationYearlyPricesAsync();
        
        var consultantInvoiceItems = (await inConsultantInvoiceItemsRepository.GetConsultantInvoiceItemsAsync()).ToList();
        
        var nonBilledPersonServiceVariations = await inPersonAnnualServiceVariationsRepository.GetConsultantNonBilledPersonAnnualServiceVariations();
        var variationsGrouping = nonBilledPersonServiceVariations
            .GroupBy(inVariation => new { inVariation.AnnualServiceVariation, inVariation.SchoolYear });
        foreach (var variationGroup in variationsGrouping)
        {
            var consultants = GetConsultantsNonBilledVariation(variationGroup.Key.SchoolYear, variationGroup.Key.AnnualServiceVariation);
            
            var totalCommonHours = _annualServiceConsultantWorks
                .Where(inWork => inWork.SchoolYear.Id == variationGroup.Key.SchoolYear.Id &&
                                 inWork.AnnualService.Id == variationGroup.Key.AnnualServiceVariation.AnnualService.Id)
                .Sum(inWork => inWork.CommonWorkHours);
            var totalIndividualWorkHours = _annualServiceVariationConsultantWorks
                .Where(inWork => inWork.SchoolYear.Id == variationGroup.Key.SchoolYear.Id &&
                                 inWork.AnnualServiceVariationId == variationGroup.Key.AnnualServiceVariation.Id)
                .Sum(inWork => inWork.IndividualWorkHours) * variationGroup.Count();
            var totalHours = totalCommonHours + totalIndividualWorkHours;
            
            foreach (var consultant in consultants)
            {
                var consultantCommonHours = _annualServiceConsultantWorks
                    .Where(inWork => inWork.SchoolYear.Id == variationGroup.Key.SchoolYear.Id &&
                                     inWork.AnnualService.Id == variationGroup.Key.AnnualServiceVariation.AnnualService.Id &&
                                     inWork.Consultant.Id == consultant.Id)
                    .Sum(inWork => inWork.CommonWorkHours);
                var consultantIndividualWorkHours = _annualServiceVariationConsultantWorks
                    .Where(inWork => inWork.SchoolYear.Id == variationGroup.Key.SchoolYear.Id &&
                                     inWork.AnnualServiceVariationId == variationGroup.Key.AnnualServiceVariation.Id &&
                                     inWork.Consultant.Id == consultant.Id)
                    .Sum(inWork => inWork.IndividualWorkHours) * variationGroup.Count();
                var consultantHours = consultantCommonHours + consultantIndividualWorkHours;
                var consultantRatioTime = consultantHours / totalHours;
                
                inResult.Add(new ConsultantInvoiceable(
                    ConsultantInvoiceableReferenceType.PersonAnnualServiceVariation,
                    variationGroup.Key.SchoolYear,
                    consultant,
                    variationGroup.Key.AnnualServiceVariation.Id,
                    variationGroup.Key.AnnualServiceVariation.GetFullName(),
                    variationGroup.Count() * GetAnnualServiceVariationPrice(variationGroup.Key.AnnualServiceVariation, variationGroup.Key.SchoolYear) * consultantRatioTime,
                    AnnualServiceVariation.GetConsultantAlreadyBilled(consultantInvoiceItems, variationGroup.Key.AnnualServiceVariation, consultant, variationGroup.Key.SchoolYear),
                    0, // Already billed count
                    variationGroup.Max(inGroup => inGroup.ConsultantPaymentsCount),
                    false
                ));
            }
        }
    }
    
    private decimal GetAnnualServiceVariationPrice(AnnualServiceVariation inAnnualServiceVariation, SchoolYear inSchoolYear)
    {
        var annualServiceVariationYearlyPrice = _annualServiceVariationYearlyPrices
            .SingleOrDefault(inPrice => inPrice.AnnualServiceVariationId == inAnnualServiceVariation.Id && inPrice.SchoolYear.Id == inSchoolYear.Id);
        return annualServiceVariationYearlyPrice?.Price ?? 0m;
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