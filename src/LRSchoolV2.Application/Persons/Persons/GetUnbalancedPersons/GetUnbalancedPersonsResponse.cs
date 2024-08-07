using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.Persons.GetUnbalancedPersons;

public record GetUnbalancedPersonsResponse(IEnumerable<Person> Persons);
