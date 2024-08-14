using LRSchoolV2.Domain.Common;

// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.Consultants;

public record Consultant(
    Guid Id,
    string LastName,
    string FirstName,
    string CompanyName,
    string PhoneNumber,
    string Email,
    Address Address,
    string Iban,
    string BicCode
)
{
    public string GetFullName() => $"{LastName} {FirstName}";
}
