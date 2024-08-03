using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;

// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;

[Table(nameof(AnnualService))]
public class AnnualServiceDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = string.Empty;

    public ICollection<AnnualServiceVariationDataModel> Variations { get; set; } = new List<AnnualServiceVariationDataModel>();
    
    public ICollection<AnnualServiceConsultantWorkDataModel> ConsultantWorks { get; set; } = new List<AnnualServiceConsultantWorkDataModel>();
}