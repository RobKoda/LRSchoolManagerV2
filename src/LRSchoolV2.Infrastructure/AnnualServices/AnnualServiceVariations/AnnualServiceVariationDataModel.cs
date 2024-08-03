using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationYearlyPrices;
using LRSchoolV2.Infrastructure.Persons.PersonAnnualServiceVariations;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global - Implicit use
// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use
// ReSharper disable CollectionNeverUpdated.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;

[Table(nameof(AnnualServiceVariation))]
public class AnnualServiceVariationDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(AnnualService))]
    public Guid AnnualServiceId { get; set; }
    public AnnualServiceDataModel? AnnualService { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(256)]
    public string InvoiceName { get; set; } = string.Empty;

    public ICollection<AnnualServiceVariationYearlyPriceDataModel> YearlyPrices { get; set; } = new List<AnnualServiceVariationYearlyPriceDataModel>();
    
    public ICollection<AnnualServiceVariationConsultantWorkDataModel> ConsultantWorks { get; set; } = new List<AnnualServiceVariationConsultantWorkDataModel>();
    
    public ICollection<PersonAnnualServiceVariationDataModel> PersonAnnualServiceVariations { get; set; } = new List<PersonAnnualServiceVariationDataModel>();
}