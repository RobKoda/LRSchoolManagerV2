using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;

public interface IPersonAnnualServiceVariationsRepository : IRepository
{
    Task<IEnumerable<PersonAnnualServiceVariation>> GetPersonAnnualServiceVariationsPerPersonAsync(Guid inPersonId);
    Task SavePersonAnnualServiceVariationAsync(PersonAnnualServiceVariation inPersonAnnualServiceVariation);
    Task DeletePersonAnnualServiceVariationAsync(Guid inPersonAnnualServiceVariationId);
    Task<bool> AnyPersonAnnualServiceVariationAsync(Guid inPersonAnnualServiceVariationId);
    Task<bool> CanPersonAnnualServiceVariationBeDeletedAsync(Guid inPersonAnnualServiceVariationId);
    Task<bool> IsPersonAnnualServiceVariationUniqueAsync(PersonAnnualServiceVariation inReferencePersonAnnualServiceVariation);
    Task<IEnumerable<PersonAnnualServiceVariation>> GetPersonAnnualServiceVariationsPerAnnualServiceAsync(Guid inAnnualServiceId);
    Task<bool> AnyPersonAnnualServiceVariationPerPersonAndSchoolYearAsync(Guid inPersonId, Guid inSchoolYearId);
    Task SetFullyBilledAsync(IEnumerable<Guid> inIds, bool inFullyBilled = true);
    Task<IEnumerable<CustomerInvoiceItem>> GetNonBilledPersonAnnualServiceVariationBilledItems();
    Task<IEnumerable<PersonAnnualServiceVariation>> GetNonBilledPersonAnnualServiceVariations();
    Task<IEnumerable<PersonAnnualServiceVariation>> GetConsultantNonBilledPersonAnnualServiceVariations();
    Task<IEnumerable<ConsultantInvoiceItem>> GetConsultantNonBilledPersonAnnualServiceVariationBilledItems();
    Task SetConsultantFullyBilledAsync(IEnumerable<Guid> inIds, bool inFullyBilled = true);
}