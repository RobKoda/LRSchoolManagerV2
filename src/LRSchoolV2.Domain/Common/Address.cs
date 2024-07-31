namespace LRSchoolV2.Domain.Common;

public record Address(
    Guid Id,
    string Street,
    string StreetComplement,
    string ZipCode,
    string City
)
{
    public string GetAddressDisplay() => $"{Street} {ZipCode} {City}";
    public string GetAddressFullDisplay() => $"{Street} {StreetComplement} {ZipCode} {City}";
    public string GetFormattedAddress() => $"{Street}\n{StreetComplement}\n{ZipCode} {City}".Replace("\n\n", "\n");
}