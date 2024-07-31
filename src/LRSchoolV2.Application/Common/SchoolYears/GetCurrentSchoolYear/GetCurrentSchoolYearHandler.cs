using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;

public class GetCurrentSchoolYearHandler : IRequestHandler<GetCurrentSchoolYearQuery, GetCurrentSchoolYearResponse>
{
    private readonly ISchoolYearsRepository _schoolYearsRepository;

    public GetCurrentSchoolYearHandler(ISchoolYearsRepository inSchoolYearsRepository)
    {
        _schoolYearsRepository = inSchoolYearsRepository;
    }

    public async Task<GetCurrentSchoolYearResponse> Handle(GetCurrentSchoolYearQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _schoolYearsRepository.GetCurrentSchoolYearAsync());
}