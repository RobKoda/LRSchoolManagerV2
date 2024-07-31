using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;

public class SaveSchoolYearHandler : IRequestHandler<SaveSchoolYearCommand>
{
    private readonly ISchoolYearsRepository _schoolYearsRepository;

    public SaveSchoolYearHandler(ISchoolYearsRepository inSchoolYearsRepository)
    {
        _schoolYearsRepository = inSchoolYearsRepository;
    }

    public Task Handle(SaveSchoolYearCommand inRequest, CancellationToken inCancellationToken)
    {
        return _schoolYearsRepository.SaveSchoolYearAsync(inRequest.SchoolYear);
    }
}