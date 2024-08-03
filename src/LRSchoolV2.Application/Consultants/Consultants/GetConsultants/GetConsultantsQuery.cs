using MediatR;

namespace LRSchoolV2.Application.Consultants.Consultants.GetConsultants;

public record GetConsultantsQuery : IRequest<GetConsultantsResponse>;