using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.SchoolYears.GetSchoolYears;

public class GetSchoolYearsHandler(ISchoolYearsRepository inSchoolYearsRepository) : IRequestHandler<GetSchoolYearsQuery, GetSchoolYearsResponse>
{
    public async Task<GetSchoolYearsResponse> Handle(GetSchoolYearsQuery inRequest, CancellationToken inCancellationToken) =>
        new(await inSchoolYearsRepository.GetSchoolYearsAsync());
}