using LanguageExt;
using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.Persons.GetMembers;

public record GetMembersResponse(OptionAsync<IEnumerable<Person>> Persons);
