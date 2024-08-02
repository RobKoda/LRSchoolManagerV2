using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;

public class GetCurrentSchoolYearHandler(
    ISchoolYearsRepository inSchoolYearsRepository
    ) : IRequestHandler<GetCurrentSchoolYearQuery, GetCurrentSchoolYearResponse>
{
    public async Task<GetCurrentSchoolYearResponse> Handle(GetCurrentSchoolYearQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inSchoolYearsRepository.GetCurrentSchoolYearAsync(DateTime.Today));
}