using LRSchoolV2.Application.Common.Addresses.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LRSchoolV2.Infrastructure.Common.Addresses;

public class AddressesRepository(IDbContextFactory<ApplicationContext> inContext) : IAddressesRepository
{
    public Task DeleteAddressAsync(Guid inAddressId) =>
        inContext.DeleteAsync<AddressDataModel>(inAddressId);
}