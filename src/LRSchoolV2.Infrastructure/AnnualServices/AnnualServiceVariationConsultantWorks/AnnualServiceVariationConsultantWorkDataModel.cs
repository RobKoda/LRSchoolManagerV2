using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Consultants.Consultants;

// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks;

[Table(nameof(AnnualServiceVariationConsultantWork))]
public class AnnualServiceVariationConsultantWorkDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(AnnualServiceVariation))]
    public Guid AnnualServiceVariationId { get; set; }
    public AnnualServiceVariationDataModel? AnnualServiceVariation { get; set; }
    
    [ForeignKey(nameof(SchoolYear))]
    public Guid SchoolYearId { get; set; }
    public SchoolYearDataModel? SchoolYear { get; set; }
    
    [ForeignKey(nameof(Consultant))]
    public Guid ConsultantId { get; set; }
    public ConsultantDataModel? Consultant { get; set; }
    
    public decimal IndividualWorkHours { get; set; }
    
    [MaxLength(1024)]
    public string IndividualWorkHoursComment { get; set; } = string.Empty;
}