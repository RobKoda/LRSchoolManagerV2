using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Consultants.Consultants;

// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks;

[Table(nameof(AnnualServiceConsultantWork))]
public class AnnualServiceConsultantWorkDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(AnnualService))]
    public Guid AnnualServiceId { get; set; }
    public AnnualServiceDataModel? AnnualService { get; set; }
    
    [ForeignKey(nameof(SchoolYear))]
    public Guid SchoolYearId { get; set; }
    public SchoolYearDataModel? SchoolYear { get; set; }
    
    [ForeignKey(nameof(Consultant))]
    public Guid ConsultantId { get; set; }
    public ConsultantDataModel? Consultant { get; set; }
    
    public decimal CommonWorkHours { get; set; }

    [MaxLength(1024)]
    public string CommonWorkHoursComment { get; set; } = string.Empty;
}