using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;

public class SaveSchoolYearHandler(
    ISchoolYearsRepository inSchoolYearsRepository
) : IRequestHandler<SaveSchoolYearCommand>
{
    public Task Handle(SaveSchoolYearCommand inRequest, CancellationToken inCancellationToken) => 
        inSchoolYearsRepository.SaveSchoolYearAsync(inRequest.SchoolYear);
}