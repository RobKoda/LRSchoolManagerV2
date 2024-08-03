using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.GetAnnualServiceVariationsPerService;

public record GetAnnualServiceVariationsPerAnnualServiceQuery(Guid AnnualServiceId) : IRequest<GetAnnualServiceVariationsPerAnnualServiceResponse>;