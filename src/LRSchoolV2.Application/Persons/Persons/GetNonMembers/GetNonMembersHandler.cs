using LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;
using LRSchoolV2.Application.Persons.Persons.Persistence;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.GetNonMembers;

public class GetNonMembersHandler(
    IPersonsRepository inPersonsRepository, 
    ISender inMediator
) : IRequestHandler<GetNonMembersQuery, GetNonMembersResponse>
{
    public async Task<GetNonMembersResponse> Handle(GetNonMembersQuery inRequest, CancellationToken inCancellationToken)
    {
        var currentSchoolYear = await inMediator.Send(new GetCurrentSchoolYearQuery(), inCancellationToken);
        return new GetNonMembersResponse(currentSchoolYear.SchoolYear
            .MapAsync<SchoolYear, IEnumerable<Person>>(inSome => inPersonsRepository.GetPersonsAsync(inSome.Id, false)));
    }
}