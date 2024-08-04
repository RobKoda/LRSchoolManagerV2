using LRSchoolV2.Application.Persons.PersonRegistrations.Persistence;
using LRSchoolV2.Domain.Persons;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan 
namespace LRSchoolV2.Infrastructure.Persons.PersonRegistrations;

public class PersonRegistrationsRepository(IDbContextFactory<ApplicationContext> inContext) : IPersonRegistrationsRepository
{
    public Task<IEnumerable<PersonRegistration>> GetPersonRegistrationsPerPersonAsync(Guid inPersonId) =>
        inContext.GetAllAsync<PersonRegistrationDataModel, PersonRegistration>(inQueryable =>
            GetPersonRegistrationQueryableAsync(inQueryable)
                .Where(inPersonRegistration => inPersonRegistration.PersonId == inPersonId));

    public Task<bool> AnyPersonRegistrationAsync(Guid inId) =>
        inContext.AnyAsync<PersonRegistrationDataModel>(inId);

    public async Task<bool> CanPersonRegistrationBeDeletedAsync(Guid inPersonRegistrationId)
    {
        //var isBilled = await (await inContext.GetQueryableAsNoTrackingAsync<CustomerInvoiceItemDataModel>()).AnyAsync(inItem => inItem.ReferenceId == inPersonRegistrationId);
        var isBilled = false;
        return await inContext.CanBeDeleted<PersonRegistrationDataModel>(inPersonRegistrationId) && !isBilled;
    }

    public async Task<bool> IsPersonRegistrationUniqueAsync(PersonRegistration inReferencePersonRegistration) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonRegistrationDataModel>())
            .Where(inPersonRegistration => inPersonRegistration.Id != inReferencePersonRegistration.Id)
            .AllAsync(inPersonRegistration =>
                inPersonRegistration.PersonId != inReferencePersonRegistration.Person.Id ||
                inPersonRegistration.SchoolYearId != inReferencePersonRegistration.SchoolYear.Id);

    public Task<IEnumerable<PersonRegistration>> GetNonBilledPersonRegistrations() =>
        inContext.GetAllAsync<PersonRegistrationDataModel, PersonRegistration>(inQueryable =>
            GetPersonRegistrationQueryableAsync(inQueryable)
                .Where(inPersonRegistration => !inPersonRegistration.IsFullyBilled));

    public async Task SetFullyBilledAsync(IEnumerable<Guid> inIds, bool inIsFullyBilled) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonRegistrationDataModel>())
            .Where(inPersonRegistration => inIds.Contains(inPersonRegistration.Id))
            .ExecuteUpdateAsync(inUpdate => inUpdate.SetProperty(
                inPersonRegistration => inPersonRegistration.IsFullyBilled, inIsFullyBilled));

    public Task DeletePersonRegistrationAsync(Guid inPersonId) =>
        inContext.DeleteAsync<PersonRegistrationDataModel>(inPersonId);

    public Task SavePersonRegistrationAsync(PersonRegistration inPerson) =>
        inContext.SaveAsync<PersonRegistrationDataModel, PersonRegistration>(inPerson);

    private static IQueryable<PersonRegistrationDataModel> GetPersonRegistrationQueryableAsync(IQueryable<PersonRegistrationDataModel> inQueryable) =>
        inQueryable
            .Include(inPersonRegistration => inPersonRegistration.Person)
            .ThenInclude(inPerson => inPerson!.ContactPerson1)
            .ThenInclude(inContactPerson => inContactPerson!.Address)
            .Include(inPersonRegistration => inPersonRegistration.Person)
            .ThenInclude(inPerson => inPerson!.Address)
            .Include(inPersonRegistration => inPersonRegistration.SchoolYear)
            .Include(inPersonRegistration => inPersonRegistration.BilledPerson)
            .ThenInclude(inBilledPerson => inBilledPerson!.Address);
}