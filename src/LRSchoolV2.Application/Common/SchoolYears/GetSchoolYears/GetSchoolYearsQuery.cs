using MediatR;

namespace LRSchoolV2.Application.Common.SchoolYears.GetSchoolYears;

public record GetSchoolYearsQuery : IRequest<GetSchoolYearsResponse>;