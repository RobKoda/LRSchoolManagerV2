using MediatR;

namespace LRSchoolV2.Application.Persons.Persons.GetMembers;

public record GetMembersQuery : IRequest<GetMembersResponse>;