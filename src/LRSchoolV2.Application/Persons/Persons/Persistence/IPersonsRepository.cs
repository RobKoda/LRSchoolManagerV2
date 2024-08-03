using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.Persons.Persistence;

public interface IPersonsRepository : IRepository
{
    Task<IEnumerable<Person>> GetPersonsAsync();
    Task<IEnumerable<Person>> GetPersonsAsync(Guid inCurrentSchoolYear, bool inIsMember);
    Task SavePersonAsync(Person inPerson);
    Task DeletePersonAsync(Guid inRequestPersonId);
    Task<bool> AnyPersonAsync(Guid inContactPersonId);
    Task<Option<Person>> GetPersonAsync(Guid inPersonId);
    Task<bool> CanPersonBeDeletedAsync(Guid inPersonId);
}