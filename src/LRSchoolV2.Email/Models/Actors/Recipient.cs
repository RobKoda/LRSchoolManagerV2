namespace LRSchoolV2.Email.Models.Actors;

public record Recipient(string Name, string Address) : EmailActor(Name, Address)
{
    public static Recipient FromEmail(string inAddress) => new(string.Empty, inAddress);
}
