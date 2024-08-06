using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;
using LRSchoolV2.Domain.CheckDeposits;
using Mapster;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedType.Global
namespace LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits;

public class CheckDepositsRepository(IDbContextFactory<ApplicationContext> inContext) : ICheckDepositsRepository
{
    public Task SaveCheckDepositAsync(CheckDeposit inCheckDeposit) => 
        inContext.SaveAsync<CheckDepositDataModel, CheckDeposit>(inCheckDeposit);

    public Task<bool> AnyCheckDepositAsync(Guid inCheckDepositId) => 
        inContext.AnyAsync<CheckDepositDataModel>(inCheckDepositId);

    public Task<bool> CanCheckDepositBeDeleted(Guid inCheckDepositId) => 
        inContext.CanBeDeleted<CheckDepositDataModel>(inCheckDepositId);

    public Task DeleteCheckDepositAsync(Guid inCheckDepositId) => 
        inContext.DeleteAsync<CheckDepositDataModel>(inCheckDepositId);

    public async Task<IEnumerable<CheckDeposit>> GetCheckDepositsAsync()
    {
        var context = await inContext.GetContextAsync();
        var checkDeposits = context.CheckDeposits.AsNoTracking()
            .Include(inCheckDeposit => inCheckDeposit.CheckDepositPayments)
            .ThenInclude(inPayment => inPayment.CustomerPayment);
        var documentReferenceIds = await context.Documents.AsNoTracking()
            .Where(inDocument => checkDeposits.Select(inCheckDeposit => inCheckDeposit.Id).Contains(inDocument.ReferenceId))
            .Select(inDocument => inDocument.ReferenceId)
            .ToListAsync();
        
        var checkDepositsDataModels = await checkDeposits.ToListAsync();
        checkDepositsDataModels.ForEach(inCheckDeposit => inCheckDeposit.HasDocument = documentReferenceIds.Contains(inCheckDeposit.Id));
        return checkDepositsDataModels.Adapt<IEnumerable<CheckDeposit>>();
    }
}