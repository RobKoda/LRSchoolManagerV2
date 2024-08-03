using LRSchoolV2.Domain.Common;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.Persons;

public record Person(
    Guid Id,
    string LastName,
    string FirstName,
    DateTime? BirthDate,
    string PhoneNumber,
    string Email,
    Address Address,
    Person? ContactPerson1,
    Person? ContactPerson2,
    bool BillingToContactPerson1)
{
    public string GetFullName() => $"{LastName} {FirstName}";
    public string GetDropdownText() => $"{GetFullName()} {Address.GetAddressDisplay()}";

    public bool IsAddressMissing() =>
        string.IsNullOrWhiteSpace(Address.Street) ||
        string.IsNullOrWhiteSpace(Address.ZipCode) ||
        string.IsNullOrWhiteSpace(Address.City);

    public string GetSearchString() => $"{LastName} {FirstName} {PhoneNumber} {Email} {Address.GetAddressFullDisplay()}";
}