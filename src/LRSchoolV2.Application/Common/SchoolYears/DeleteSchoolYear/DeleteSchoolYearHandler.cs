using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;

public class DeleteSchoolYearHandler : IRequestHandler<DeleteSchoolYearCommand>
{
    private readonly ISchoolYearsRepository _repository;

    public DeleteSchoolYearHandler(ISchoolYearsRepository inRepository)
    {
        _repository = inRepository;
    }

    public Task Handle(DeleteSchoolYearCommand inRequest, CancellationToken inCancellationToken)
    {
        return _repository.DeleteSchoolYearAsync(inRequest.SchoolYear.Id);
    }
}