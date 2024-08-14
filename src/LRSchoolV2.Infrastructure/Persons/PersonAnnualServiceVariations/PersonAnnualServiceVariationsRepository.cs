using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan
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
        var isBilled = await (await inContext.GetQueryableAsNoTrackingAsync<CustomerInvoiceItemDataModel>()).AnyAsync(inItem => inItem.ReferenceId == inPersonAnnualServiceVariationId);
        return await inContext.CanBeDeleted<PersonAnnualServiceVariationDataModel>(inPersonAnnualServiceVariationId) && !isBilled;
    }

    public async Task<bool> IsPersonAnnualServiceVariationUniqueAsync(PersonAnnualServiceVariation inReferencePersonAnnualServiceVariation) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonAnnualServiceVariationDataModel>())
            .Where(inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.Id != inReferencePersonAnnualServiceVariation.Id)
            .AllAsync(inPersonAnnualServiceVariation =>
                inPersonAnnualServiceVariation.PersonId != inReferencePersonAnnualServiceVariation.Person.Id ||
                inPersonAnnualServiceVariation.SchoolYearId != inReferencePersonAnnualServiceVariation.SchoolYear.Id ||
                inPersonAnnualServiceVariation.AnnualServiceVariationId != inReferencePersonAnnualServiceVariation.AnnualServiceVariation.Id);

    public async Task<IEnumerable<PersonAnnualServiceVariation>> GetPersonAnnualServiceVariationsPerAnnualServiceAsync(Guid inServiceId)
    {
        var context = await inContext.GetContextAsync();
        var annualServiceVariationIds = context.AnnualServiceVariations.AsNoTracking()
            .Where(inAnnualServiceVariation => inAnnualServiceVariation.AnnualServiceId == inServiceId)
            .Select(inAnnualServiceVariation => inAnnualServiceVariation.Id);

        return await context.PersonAnnualServiceVariations.AsNoTracking()
            .Where(inPersonAnnualServiceVariation => annualServiceVariationIds.Contains(inPersonAnnualServiceVariation.AnnualServiceVariationId))
            .ProjectToType<PersonAnnualServiceVariation>()
            .ToListAsync();
    }
    
    public async Task<bool> AnyPersonAnnualServiceVariationPerPersonAndSchoolYearAsync(Guid inPersonId, Guid inSchoolYearId) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonAnnualServiceVariationDataModel>())
            .AnyAsync(inPersonAnnualServiceVariation => 
                inPersonAnnualServiceVariation.PersonId != inPersonId &&
                inPersonAnnualServiceVariation.SchoolYearId == inSchoolYearId);

    public Task DeletePersonAnnualServiceVariationAsync(Guid inPersonId) =>
        inContext.DeleteAsync<PersonAnnualServiceVariationDataModel>(inPersonId);

    public Task SavePersonAnnualServiceVariationAsync(PersonAnnualServiceVariation inPersonAnnual) =>
        inContext.SaveAsync<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>(inPersonAnnual);
    
    public Task<IEnumerable<PersonAnnualServiceVariation>> GetNonBilledPersonAnnualServiceVariations() =>
        inContext.GetAllAsync<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>(inQueryable =>
            GetPersonServiceVariationQueryableAsync(inQueryable)
                .Where(inPersonAnnualServiceVariation => !inPersonAnnualServiceVariation.IsFullyBilled));
    
    public async Task SetFullyBilledAsync(IEnumerable<Guid> inIds, bool inIsFullyBilled) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonAnnualServiceVariationDataModel>())
            .Where(inPersonRegistration => inIds.Contains(inPersonRegistration.Id))
            .ExecuteUpdateAsync(inUpdate => inUpdate.SetProperty(
                inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.IsFullyBilled, inIsFullyBilled));
    
    public async Task SetConsultantFullyBilledAsync(IEnumerable<Guid> inIds, bool inIsFullyBilled) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<PersonAnnualServiceVariationDataModel>())
            .Where(inPersonRegistration => inIds.Contains(inPersonRegistration.Id))
            .ExecuteUpdateAsync(inUpdate => inUpdate.SetProperty(
                inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.ConsultantIsFullyBilled, inIsFullyBilled));
    
    public async Task<IEnumerable<CustomerInvoiceItem>> GetNonBilledPersonAnnualServiceVariationBilledItems()
    {
        var context = await inContext.GetContextAsync();
        var nonBilledVariationsQueryable = context.PersonAnnualServiceVariations.AsNoTracking()
            .Where(inPersonAnnualServiceVariation => !inPersonAnnualServiceVariation.IsFullyBilled)
            .Select(inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.Id);
        
        return await context.CustomerInvoiceItems.AsNoTracking()
            .Where(inCustomerInvoiceItem => inCustomerInvoiceItem.ReferenceId.HasValue && nonBilledVariationsQueryable.Contains(inCustomerInvoiceItem.ReferenceId.Value))
            .ProjectToType<CustomerInvoiceItem>()
            .ToListAsync();
    }
    
    public Task<IEnumerable<PersonAnnualServiceVariation>> GetConsultantNonBilledPersonAnnualServiceVariations() =>
        inContext.GetAllAsync<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>(inQueryable =>
            GetPersonServiceVariationQueryableAsync(inQueryable)
                .Where(inPersonAnnualServiceVariation => !inPersonAnnualServiceVariation.ConsultantIsFullyBilled));
    
    public async Task<IEnumerable<ConsultantInvoiceItem>> GetConsultantNonBilledPersonAnnualServiceVariationBilledItems()
    {
        var context = await inContext.GetContextAsync();
        var nonBilledVariationsQueryable = context.PersonAnnualServiceVariations.AsNoTracking()
            .Where(inPersonAnnualServiceVariation => !inPersonAnnualServiceVariation.ConsultantIsFullyBilled)
            .Select(inPersonAnnualServiceVariation => inPersonAnnualServiceVariation.Id);
        
        return await context.ConsultantInvoiceItems.AsNoTracking()
            .Where(inCustomerInvoiceItem => inCustomerInvoiceItem.ReferenceId.HasValue && nonBilledVariationsQueryable.Contains(inCustomerInvoiceItem.ReferenceId.Value))
            .ProjectToType<ConsultantInvoiceItem>()
            .ToListAsync();
    }

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