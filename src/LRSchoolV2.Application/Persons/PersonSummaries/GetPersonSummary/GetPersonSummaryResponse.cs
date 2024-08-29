using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.PersonSummaries.GetPersonSummary;

public record GetPersonSummaryResponse(IEnumerable<PersonSummaryLine> SummaryLines);