using LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;
using LRSchoolV2.Application.Persons.Persons.Persistence;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.GetMembers;

public class GetMembersHandler(
    IPersonsRepository inPersonsRepository, 
    ISender inMediator
    ) : IRequestHandler<GetMembersQuery, GetMembersResponse>
{
    public async Task<GetMembersResponse> Handle(GetMembersQuery inRequest, CancellationToken inCancellationToken)
    {
        var currentSchoolYear = await inMediator.Send(new GetCurrentSchoolYearQuery(), inCancellationToken);
        return new GetMembersResponse(currentSchoolYear.SchoolYear
            .MapAsync<SchoolYear, IEnumerable<Person>>(inSome => inPersonsRepository.GetPersonsAsync(inSome.Id, true)));
    }
}