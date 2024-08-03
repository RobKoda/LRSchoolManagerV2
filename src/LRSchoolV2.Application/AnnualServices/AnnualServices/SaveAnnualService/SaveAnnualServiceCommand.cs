using LRSchoolV2.Domain.AnnualServices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.SaveAnnualService;

public record SaveAnnualServiceCommand(AnnualService AnnualService) : IRequest;
