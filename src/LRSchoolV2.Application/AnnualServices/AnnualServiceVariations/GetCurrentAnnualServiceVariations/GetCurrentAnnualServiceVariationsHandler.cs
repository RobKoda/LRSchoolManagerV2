using LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.Persistence;
using LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;
using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.GetCurrentAnnualServiceVariations;

public class GetCurrentAnnualServiceVariationsHandler : IRequestHandler<GetCurrentAnnualServiceVariationsQuery, GetCurrentAnnualServiceVariationsResponse>
{
    private readonly IAnnualServiceVariationsRepository _annualServiceVariationsRepository;
    private readonly IMediator _mediator;

    public GetCurrentAnnualServiceVariationsHandler(IAnnualServiceVariationsRepository inAnnualServiceVariationsRepository, IMediator inMediator)
    {
        _annualServiceVariationsRepository = inAnnualServiceVariationsRepository;
        _mediator = inMediator;
    }

    public async Task<GetCurrentAnnualServiceVariationsResponse> Handle(GetCurrentAnnualServiceVariationsQuery inRequest, CancellationToken inCancellationToken)
    {
        var currentSchoolYear = await _mediator.Send(new GetCurrentSchoolYearQuery(), inCancellationToken);
        return new GetCurrentAnnualServiceVariationsResponse(currentSchoolYear.SchoolYear
            .MapAsync<SchoolYear, IEnumerable<AnnualServiceVariation>>(inSome => _annualServiceVariationsRepository.GetAnnualServiceVariationsPerSchoolYearAsync(inSome.Id)));
    }
}