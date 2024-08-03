using LanguageExt;
using LRSchoolV2.Application.Persons.Persons.Persistence;
using LRSchoolV2.Domain.Persons;
using Microsoft.EntityFrameworkCore;

namespace LRSchoolV2.Infrastructure.Persons.Persons;

public class PersonsRepository(
    IDbContextFactory<ApplicationContext> inContext
    ) : IPersonsRepository
{
    public Task<IEnumerable<Person>> GetPersonsAsync() =>
        inContext.GetAllAsync<PersonDataModel, Person>(GetPersonQueryableAsync);
    
    public Task<IEnumerable<Person>> GetPersonsAsync(Guid inCurrentSchoolYear, bool inIsMember) => 
        inContext.GetAllAsync<PersonDataModel, Person>(inQueryable => GetMemberQueryable(inQueryable, inCurrentSchoolYear, inIsMember));

    public Task<Option<Person>> GetPersonAsync(Guid inId) =>
        inContext.GetSingleAsync<PersonDataModel, Person>(inId, GetPersonQueryableAsync);

    public Task<bool> CanPersonBeDeletedAsync(Guid inPersonId) => 
        inContext.CanBeDeleted<PersonDataModel>(inPersonId);

    public Task<bool> AnyPersonAsync(Guid inId) =>
        inContext.AnyAsync<PersonDataModel>(inId);

    public Task DeletePersonAsync(Guid inPersonId) =>
        inContext.DeleteAsync<PersonDataModel>(inPersonId);

    public Task SavePersonAsync(Person inPerson) => 
        inContext.SaveAsync<PersonDataModel, Person>(inPerson);

    private static IQueryable<PersonDataModel> GetPersonQueryableAsync(IQueryable<PersonDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Address)
            .Include(inPerson => inPerson.ContactPerson1)
            .ThenInclude(inContactPerson => inContactPerson!.Address)
            .Include(inPerson => inPerson.ContactPerson2)
            .ThenInclude(inContactPerson => inContactPerson!.Address);

    private static IQueryable<PersonDataModel> GetMemberQueryable(IQueryable<PersonDataModel> inQueryable, Guid inCurrentSchoolYear, bool inIsMember)
    {
        if (inIsMember)
        {
            return GetPersonQueryableAsync(inQueryable)
                .Where(inPerson => inPerson.Registrations.Any(inRegistration => inRegistration.SchoolYearId == inCurrentSchoolYear));
        }

        return GetPersonQueryableAsync(inQueryable)
            .Where(inPerson => inPerson.Registrations.All(inRegistration => inRegistration.SchoolYearId != inCurrentSchoolYear));
    }
}