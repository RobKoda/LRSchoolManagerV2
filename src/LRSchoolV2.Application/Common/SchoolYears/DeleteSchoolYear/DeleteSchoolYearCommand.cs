using LRSchoolV2.Domain.Common;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;

public record DeleteSchoolYearCommand(SchoolYear SchoolYear) : IRequest;
