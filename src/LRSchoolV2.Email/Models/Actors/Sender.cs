namespace LRSchoolV2.Email.Models.Actors;

public record Sender(string Name, string Address) : EmailActor(Name, Address);
