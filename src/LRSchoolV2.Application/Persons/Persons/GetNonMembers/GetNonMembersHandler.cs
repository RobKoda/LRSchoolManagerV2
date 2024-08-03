using LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;
using LRSchoolV2.Application.Persons.Persons.Persistence;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.GetNonMembers;

public class GetNonMembersHandler : IRequestHandler<GetNonMembersQuery, GetNonMembersResponse>
{
    private readonly IPersonsRepository _personsRepository;
    private readonly IMediator _mediator;

    public GetNonMembersHandler(IPersonsRepository inPersonsRepository, IMediator inMediator)
    {
        _personsRepository = inPersonsRepository;
        _mediator = inMediator;
    }

    public async Task<GetNonMembersResponse> Handle(GetNonMembersQuery inRequest, CancellationToken inCancellationToken)
    {
        var currentSchoolYear = await _mediator.Send(new GetCurrentSchoolYearQuery(), inCancellationToken);
        return new GetNonMembersResponse(currentSchoolYear.SchoolYear
            .MapAsync<SchoolYear, IEnumerable<Person>>(inSome => _personsRepository.GetPersonsAsync(inSome.Id, false)));
    }
}