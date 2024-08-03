using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;

public interface IPersonRegistrationsRepository : IRepository
{
    Task<IEnumerable<PersonRegistration>> GetPersonRegistrationsPerPersonAsync(Guid inPersonId);
    Task SavePersonRegistrationAsync(PersonRegistration inPersonRegistration);
    Task DeletePersonRegistrationAsync(Guid inRequestPersonRegistrationId);
    Task<bool> AnyPersonRegistrationAsync(Guid inContactPersonRegistrationId);
    Task<bool> CanPersonRegistrationBeDeletedAsync(Guid inPersonRegistrationId);
    Task<bool> IsPersonRegistrationUniqueAsync(PersonRegistration inReferencePersonRegistration);
    Task<IEnumerable<PersonRegistration>> GetNonBilledPersonRegistrations();
    Task SetFullyBilledAsync(IEnumerable<Guid> inIds, bool inFullyBilled = true);
}