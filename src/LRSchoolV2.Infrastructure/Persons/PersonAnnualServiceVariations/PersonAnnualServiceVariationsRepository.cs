using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Domain.Persons;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LRSchoolV2.Infrastructure.Persons.PersonAnnualServiceVariations;

public class PersonAnnualServiceVariationsRepository(IDbContextFactory<ApplicationContext> inContext) : IPersonAnnualServiceVariationsRepository
{
    public Task<IEnumerable<PersonAnnualServiceVariation>> GetPersonAnnualServiceVariationsPerPersonAsync(Guid inPersonId) =>
        inContext.GetAllAsync<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>(inQueryable =>
            GetPersonServiceVariationQueryableAsync(inQueryable)
                .Where(inPersonServiceVariation => inPersonServiceVariation.PersonId == inPersonId));

    public Task<bool> AnyPersonAnnualServiceVariationAsync(Guid inId) =>
        inContext.AnyAsync<PersonAnnualServiceVariationDataModel>(inId);

    public async Task<bool> CanPersonAnnualServiceVariationBeDeletedAsync(Guid inPersonAnnualServiceVariationId)
    {
        // TODO
        //var isBilled = await (await inContext.GetQueryableAsNoTrackingAsync<CustomerInvoiceItemDataModel>()).AnyAsync(inItem => inItem.ReferenceId == inPersonAnnualServiceVariationId);
        var isBilled = false;
        return await inContext.CanBeDeleted<PersonAnnualServiceVariationDataModel>(inPersonAnnualServiceVariationId) && !isBilled;
    }

    public async Task<bool> IsPersonAnnualServiceVariationUniqueAsync(PersonAnnualServiceVariation inReferencePersonAnnualServiceVariation) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonAnnualServiceVariationDataModel>())
            .Where(inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.Id != inReferencePersonAnnualServiceVariation.Id)
            .AllAsync(inPersonAnnualServiceVariation =>
                inPersonAnnualServiceVariation.PersonId != inReferencePersonAnnualServiceVariation.Person.Id ||
                inPersonAnnualServiceVariation.SchoolYearId != inReferencePersonAnnualServiceVariation.SchoolYear.Id ||
                inPersonAnnualServiceVariation.AnnualServiceVariationId != inReferencePersonAnnualServiceVariation.AnnualServiceVariation.Id);

    public Task<IEnumerable<PersonAnnualServiceVariation>> GetNonBilledPersonAnnualServiceVariations() =>
        inContext.GetAllAsync<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>(inQueryable =>
            GetPersonServiceVariationQueryableAsync(inQueryable)
                .Where(inPersonAnnualServiceVariation => !inPersonAnnualServiceVariation.IsFullyBilled));

    public async Task SetFullyBilledAsync(IEnumerable<Guid> inIds, bool inIsFullyBilled) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonAnnualServiceVariationDataModel>())
            .Where(inPersonRegistration => inIds.Contains(inPersonRegistration.Id))
            .ExecuteUpdateAsync(inUpdate => inUpdate.SetProperty(
                inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.IsFullyBilled, inIsFullyBilled));

    public async Task<IEnumerable<PersonAnnualServiceVariation>> GetPersonAnnualServiceVariationsPerAnnualServiceAsync(Guid inServiceId)
    {
        var context = await inContext.GetContextAsync();
        var serviceVariationIds = context.AnnualServiceVariations.AsNoTracking()
            .Where(inAnnualServiceVariation => inAnnualServiceVariation.AnnualServiceId == inServiceId)
            .Select(inAnnualServiceVariation => inAnnualServiceVariation.Id);

        return await context.PersonAnnualServiceVariations.AsNoTracking()
            .Where(inPersonAnnualServiceVariation => serviceVariationIds.Contains(inPersonAnnualServiceVariation.AnnualServiceVariationId))
            .ProjectToType<PersonAnnualServiceVariation>()
            .ToListAsync();
    }

    public Task<IEnumerable<PersonAnnualServiceVariation>> GetPersonAnnualServiceVariationsPerSchoolYearAsync(Guid inSchoolYearId) =>
        inContext.GetAllAsync<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>(inQueryable =>
            GetPersonServiceVariationQueryableAsync(inQueryable)
                .Where(inPersonServiceVariation => inPersonServiceVariation.SchoolYearId == inSchoolYearId));

    public Task DeletePersonAnnualServiceVariationAsync(Guid inPersonId) =>
        inContext.DeleteAsync<PersonAnnualServiceVariationDataModel>(inPersonId);

    public Task SavePersonAnnualServiceVariationAsync(PersonAnnualServiceVariation inPersonAnnual) =>
        inContext.SaveAsync<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>(inPersonAnnual);
    
    // TODO
    /*public async Task<IEnumerable<CustomerInvoiceItem>> GetNonBilledPersonAnnualServiceVariationBilledItems()
    {
        var context = await inContext.GetContextAsync();
        var nonBilledVariationsQueryable = context.PersonAnnualServiceVariations.AsNoTracking()
            .Where(inPersonAnnualServiceVariation => !inPersonAnnualServiceVariation.IsFullyBilled)
            .Select(inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.Id);
        
        return await context.CustomerInvoiceItems.AsNoTracking()
            .Where(inCustomerInvoiceItem => nonBilledVariationsQueryable.Contains(inCustomerInvoiceItem.ReferenceId))
            .ProjectToType<CustomerInvoiceItem>()
            .ToListAsync();
    }*/

    private static IQueryable<PersonAnnualServiceVariationDataModel> GetPersonServiceVariationQueryableAsync(IQueryable<PersonAnnualServiceVariationDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Person)
            .ThenInclude(inPerson => inPerson!.ContactPerson1)
            .ThenInclude(inContactPerson => inContactPerson!.Address)
            .Include(inPerson => inPerson.Person)
            .ThenInclude(inContactPerson => inContactPerson!.Address)
            .Include(inPerson => inPerson.AnnualServiceVariation)
            .ThenInclude(inAnnualServiceVariation => inAnnualServiceVariation!.AnnualService)
            .Include(inPersonRegistration => inPersonRegistration.BilledPerson)
            .ThenInclude(inBilledPerson => inBilledPerson!.Address);
}