using MediatR;

namespace LRSchoolV2.Application.Persons.Persons.GetNonMembers;

public record GetNonMembersQuery : IRequest<GetNonMembersResponse>;