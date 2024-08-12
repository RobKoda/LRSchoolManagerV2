using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Persons.Persons;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Implicit use
// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.Persons.PersonAnnualServiceVariations;

[Table(nameof(PersonAnnualServiceVariation))]
public class PersonAnnualServiceVariationDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Person))]
    public Guid PersonId { get; set; }
    public PersonDataModel? Person { get; set; }
    
    [ForeignKey(nameof(SchoolYear))]
    public Guid SchoolYearId { get; set; }
    public SchoolYearDataModel? SchoolYear { get; set; }
    
    [ForeignKey(nameof(AnnualServiceVariation))]
    public Guid AnnualServiceVariationId { get; set; }
    public AnnualServiceVariationDataModel? AnnualServiceVariation { get; set; }
    
    public int PaymentsCount { get; set; }
    
    public bool IsFullyBilled { get; set; }
    
    [ForeignKey(nameof(BilledPerson))]
    public Guid? BilledPersonId { get; set; }
    public PersonDataModel? BilledPerson { get; set; }
    
    public int ConsultantPaymentsCount { get; set; }
    
    public bool ConsultantIsFullyBilled { get; set; }
}