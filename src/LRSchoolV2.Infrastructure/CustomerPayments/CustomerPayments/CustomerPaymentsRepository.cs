using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;
using LRSchoolV2.Domain.CustomerPayments;
using Mapster;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments;

public class CustomerPaymentsRepository(IDbContextFactory<ApplicationContext> inContext) : ICustomerPaymentsRepository
{
    public Task<bool> AnyCustomerPaymentAsync(Guid inCustomerPaymentId) => 
        inContext.AnyAsync<CustomerPaymentDataModel>(inCustomerPaymentId);

    public async Task<IEnumerable<CustomerPayment>> GetUnlinkedChecksAsync()
    {
        var context = await inContext.CreateDbContextAsync();

        return await context.CustomerPayments.AsNoTracking()
            .Where(inPayment => inPayment.CustomerPaymentTypeValue == CustomerPaymentType.BankCheck)
            .Where(inPayment => !context.CheckDepositPayments.Select(inCheckDepositPayment => inCheckDepositPayment.CustomerPayment!.Id).Contains(inPayment.Id))
            .ProjectToType<CustomerPayment>()
            .ToListAsync();
    }

    public Task SaveCustomerPaymentAsync(CustomerPayment inCustomerPayment) =>
        inContext.SaveAsync<CustomerPaymentDataModel, CustomerPayment>(inCustomerPayment);

    public Task DeleteCustomerPaymentAsync(Guid inCustomerPaymentId) => 
        inContext.DeleteAsync<CustomerPaymentDataModel>(inCustomerPaymentId);

    public Task<bool> CanCustomerPaymentBeDeleted(Guid inCustomerPaymentId) => 
        inContext.CanBeDeleted<CustomerPaymentDataModel>(inCustomerPaymentId);

    public Task<IEnumerable<CustomerPayment>> GetCustomerPaymentsAsync() => 
        inContext.GetAllAsync<CustomerPaymentDataModel, CustomerPayment>(GetCustomerPaymentQueryableAsync);
    
    private static IQueryable<CustomerPaymentDataModel> GetCustomerPaymentQueryableAsync(IQueryable<CustomerPaymentDataModel> inQueryable) =>
        inQueryable
            .Include(inPerson => inPerson.Person);
}