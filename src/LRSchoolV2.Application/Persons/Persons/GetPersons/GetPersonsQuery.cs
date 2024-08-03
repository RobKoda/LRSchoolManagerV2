using MediatR;

namespace LRSchoolV2.Application.Persons.Persons.GetPersons;

public record GetPersonsQuery : IRequest<GetPersonsResponse>;