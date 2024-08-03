using MediatR;

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.GetAnnualServices;

public record GetAnnualServicesQuery : IRequest<GetAnnualServicesResponse>;