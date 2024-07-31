using MediatR;

namespace LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;

public record GetCurrentSchoolYearQuery : IRequest<GetCurrentSchoolYearResponse>;