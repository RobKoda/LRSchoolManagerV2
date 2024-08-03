using LanguageExt;
using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.Persons.GetNonMembers;

public record GetNonMembersResponse(OptionAsync<IEnumerable<Person>> Persons);
