using LRSchoolV2.Application.Core;

namespace LRSchoolV2.Application.Common.Addresses.Persistence;

public interface IAddressesRepository : IRepository
{
    Task DeleteAddressAsync(Guid inAddressId);
}