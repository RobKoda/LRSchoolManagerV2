using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.Persons.GetPersons;

public record GetPersonsResponse(IEnumerable<Person> Persons);
