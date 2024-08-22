using LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.Persistence;
using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.Persistence;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Consultants;
using LRSchoolV2.Domain.Persons;
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
        
        var nonBilledPersonServiceVariations = (await inPersonAnnualServiceVariationsRepository.GetConsultantNonBilledPersonAnnualServiceVariations()).ToList();
        var variationsGrouping = nonBilledPersonServiceVariations
            .GroupBy(inVariation => new { inVariation.AnnualServiceVariation.AnnualService, inVariation.SchoolYear });
        
        foreach (var variationGroup in variationsGrouping)
        {
            var filteredDonBilledPersonServiceVariations = nonBilledPersonServiceVariations
                .Where(inNonBilled => inNonBilled.AnnualServiceVariation.AnnualService.Id == variationGroup.Key.AnnualService.Id && 
                                                              inNonBilled.SchoolYear.Id == variationGroup.Key.SchoolYear.Id)
                .ToList();
            
            var consultants = GetConsultantsNonBilledAnnualService(variationGroup.Key.SchoolYear, variationGroup.Key.AnnualService);
            
            var totalCommonHours = _annualServiceConsultantWorks
                .Where(inWork => inWork.SchoolYear.Id == variationGroup.Key.SchoolYear.Id &&
                                 inWork.AnnualService.Id == variationGroup.Key.AnnualService.Id)
                .Sum(inWork => inWork.CommonWorkHours);
            var totalIndividualWorkHours = GetTotalIndividualWorkHours(variationGroup.Key.AnnualService, variationGroup.Key.SchoolYear, filteredDonBilledPersonServiceVariations);
            var totalHours = totalCommonHours + totalIndividualWorkHours;
            
            foreach (var consultant in consultants)
            {
                var consultantCommonHours = _annualServiceConsultantWorks
                    .Where(inWork => inWork.SchoolYear.Id == variationGroup.Key.SchoolYear.Id &&
                                     inWork.AnnualService.Id == variationGroup.Key.AnnualService.Id &&
                                     inWork.Consultant.Id == consultant.Id)
                    .Sum(inWork => inWork.CommonWorkHours);
                var consultantIndividualWorkHours = GetConsultantIndividualWorkHours(variationGroup.Key.AnnualService, variationGroup.Key.SchoolYear, consultant, filteredDonBilledPersonServiceVariations);
                var consultantHours = consultantCommonHours + consultantIndividualWorkHours;
                var consultantRatioTime = consultantHours / totalHours;
                
                inResult.Add(new ConsultantInvoiceable(
                    ConsultantInvoiceableReferenceType.AnnualService,
                    variationGroup.Key.SchoolYear,
                    consultant,
                    variationGroup.Key.AnnualService,
                    variationGroup.Key.AnnualService.Id,
                    variationGroup.Key.AnnualService.Name,
                    GetAnnualServiceTotalToPay(variationGroup.Key.AnnualService, variationGroup.Key.SchoolYear, filteredDonBilledPersonServiceVariations) * consultantRatioTime,
                    AnnualService.GetConsultantAlreadyBilled(consultantInvoiceItems, variationGroup.Key.AnnualService, consultant, variationGroup.Key.SchoolYear),
                    AnnualService.GetConsultantBilledPaymentsCount(consultantInvoiceItems, variationGroup.Key.AnnualService, consultant, variationGroup.Key.SchoolYear),
                    variationGroup.Max(inGroup => inGroup.ConsultantPaymentsCount),
                    false
                ));
            }
        }
    }
    
    private decimal GetTotalIndividualWorkHours(AnnualService inAnnualService, SchoolYear inSchoolYear, IEnumerable<PersonAnnualServiceVariation> inPersonAnnualServiceVariations) =>
        inPersonAnnualServiceVariations.Where(inPersonVariation => inPersonVariation.AnnualServiceVariation.AnnualService.Id == inAnnualService.Id)
            .GroupBy(inPersonVariation => inPersonVariation.AnnualServiceVariation)
            .Sum(inVariation => _annualServiceVariationConsultantWorks
                .Where(inWork => inWork.AnnualServiceVariation.Id == inVariation.Key.Id && inWork.SchoolYear.Id == inSchoolYear.Id)
                .Sum(inWork => inWork.IndividualWorkHours) * inVariation.Count());
    
    private decimal GetConsultantIndividualWorkHours(AnnualService inAnnualService, SchoolYear inSchoolYear, Consultant inConsultant, IEnumerable<PersonAnnualServiceVariation> inPersonAnnualServiceVariations) =>
        inPersonAnnualServiceVariations.Where(inPersonVariation => inPersonVariation.AnnualServiceVariation.AnnualService.Id == inAnnualService.Id)
            .GroupBy(inPersonVariation => inPersonVariation.AnnualServiceVariation)
            .Sum(inVariation => _annualServiceVariationConsultantWorks
                .Where(inWork => inWork.AnnualServiceVariation.Id == inVariation.Key.Id && inWork.SchoolYear.Id == inSchoolYear.Id && inWork.Consultant.Id == inConsultant.Id)
                .Sum(inWork => inWork.IndividualWorkHours) * inVariation.Count());
    
    private decimal GetAnnualServiceTotalToPay(AnnualService inAnnualService, SchoolYear inSchoolYear, IEnumerable<PersonAnnualServiceVariation> inPersonAnnualServiceVariations) =>
        inPersonAnnualServiceVariations.Where(inPersonVariation => inPersonVariation.AnnualServiceVariation.AnnualService.Id == inAnnualService.Id)
            .GroupBy(inPersonVariation => inPersonVariation.AnnualServiceVariation)
            .Sum(inVariation => _annualServiceVariationYearlyPrices
                .Where(inPrice => inPrice.AnnualServiceVariation.Id == inVariation.Key.Id && inPrice.SchoolYear.Id == inSchoolYear.Id)
                .Sum(inPrice => inPrice.Price - inPrice.Margin) * inVariation.Count());
    
    private IEnumerable<Consultant> GetConsultantsNonBilledAnnualService(SchoolYear inSchoolYear, AnnualService inAnnualService) =>
        _annualServiceConsultantWorks
            .Where(inWork => inWork.SchoolYear.Id == inSchoolYear.Id && inWork.AnnualService.Id == inAnnualService.Id)
            .Select(inWork => inWork.Consultant)
            .Append(_annualServiceVariationConsultantWorks
                .Where(inWork => inWork.SchoolYear.Id == inSchoolYear.Id && inWork.AnnualServiceVariation.AnnualService.Id == inAnnualService.Id)
                .Select(inWork => inWork.Consultant))
            .Distinct();
}