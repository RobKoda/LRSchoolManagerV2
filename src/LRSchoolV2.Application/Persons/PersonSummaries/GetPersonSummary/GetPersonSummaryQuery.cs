using LRSchoolV2.Domain.Persons;
using MediatR;

namespace LRSchoolV2.Application.Persons.PersonSummaries.GetPersonSummary;

public record GetPersonSummaryQuery(Person Person) : IRequest<GetPersonSummaryResponse>;