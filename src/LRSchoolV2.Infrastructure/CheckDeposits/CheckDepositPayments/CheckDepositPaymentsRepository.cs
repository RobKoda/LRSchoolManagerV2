using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;
using LRSchoolV2.Domain.CheckDeposits;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CheckDeposits.CheckDepositPayments;

public class CheckDepositPaymentsRepository(IDbContextFactory<ApplicationContext> inContext) : ICheckDepositPaymentsRepository
{
    public Task<bool> AnyCheckDepositPaymentAsync(Guid inCheckDepositPaymentId) => 
        inContext.AnyAsync<CheckDepositPaymentDataModel>(inCheckDepositPaymentId);

    public async Task<bool> AnyCheckDepositPaymentPerCheckIdAsync(Guid inCheckId) =>
        await (await inContext.GetQueryableAsNoTrackingAsync<CheckDepositPaymentDataModel>())
            .AnyAsync(inPayment => inPayment.CustomerPaymentId == inCheckId);

    public Task<IEnumerable<CheckDepositPayment>> GetCheckDepositPaymentsPerCheckDepositAsync(Guid inCheckDepositId) =>
        inContext.GetAllAsync<CheckDepositPaymentDataModel, CheckDepositPayment>(inQueryable => 
            GetCheckDepositPaymentQueryableAsync(inQueryable)
                .Where(inPayment => inPayment.CheckDepositId == inCheckDepositId)
        );

    public Task<bool> CanCheckDepositPaymentBeDeletedAsync(Guid inCheckDepositPaymentId) =>
        inContext.CanBeDeleted<CheckDepositPaymentDataModel>(inCheckDepositPaymentId);

    public Task DeleteCheckDepositPaymentAsync(Guid inCheckDepositPaymentId) =>
        inContext.DeleteAsync<CheckDepositPaymentDataModel>(inCheckDepositPaymentId);

    public Task SaveCheckDepositPaymentAsync(CheckDepositPayment inCheckDepositPayment) =>
        inContext.SaveAsync<CheckDepositPaymentDataModel, CheckDepositPayment>(inCheckDepositPayment);

    private static IQueryable<CheckDepositPaymentDataModel> GetCheckDepositPaymentQueryableAsync(IQueryable<CheckDepositPaymentDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.CheckDeposit);
}