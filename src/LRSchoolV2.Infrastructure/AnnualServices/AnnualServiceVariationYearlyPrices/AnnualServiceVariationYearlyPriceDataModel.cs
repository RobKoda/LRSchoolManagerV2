using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using LRSchoolV2.Infrastructure.Common.SchoolYears;

// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationYearlyPrices;

[Table(nameof(AnnualServiceVariationYearlyPrice))]
public class AnnualServiceVariationYearlyPriceDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(AnnualService))]
    public Guid AnnualServiceVariationId { get; set; }
    public AnnualServiceVariationDataModel? AnnualServiceVariation { get; set; }
    
    [ForeignKey(nameof(SchoolYear))]
    public Guid SchoolYearId { get; set; }
    public SchoolYearDataModel? SchoolYear { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Margin { get; set; }
}