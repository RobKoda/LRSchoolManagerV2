using MediatR;

namespace LRSchoolV2.Application.Consultants.GetConsultants;

public record GetConsultantsQuery : IRequest<GetConsultantsResponse>;