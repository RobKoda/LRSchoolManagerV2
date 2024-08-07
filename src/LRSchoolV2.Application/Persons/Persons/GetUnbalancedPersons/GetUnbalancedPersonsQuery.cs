using MediatR;

namespace LRSchoolV2.Application.Persons.Persons.GetUnbalancedPersons;

public record GetUnbalancedPersonsQuery : IRequest<GetUnbalancedPersonsResponse>;