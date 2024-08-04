using LRSchoolV2.Application.Common.Addresses.Persistence;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Infrastructure.Common.Addresses;

public class AddressesRepository(IDbContextFactory<ApplicationContext> inContext) : IAddressesRepository
{
    public Task DeleteAddressAsync(Guid inAddressId) =>
        inContext.DeleteAsync<AddressDataModel>(inAddressId);
}