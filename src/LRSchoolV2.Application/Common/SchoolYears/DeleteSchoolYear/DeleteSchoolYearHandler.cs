using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;

public class DeleteSchoolYearHandler(ISchoolYearsRepository inRepository) : IRequestHandler<DeleteSchoolYearCommand>
{
    public Task Handle(DeleteSchoolYearCommand inRequest, CancellationToken inCancellationToken)
    {
        return inRepository.DeleteSchoolYearAsync(inRequest.Id);
    }
}