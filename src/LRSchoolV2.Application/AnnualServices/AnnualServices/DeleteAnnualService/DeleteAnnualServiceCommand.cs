using LRSchoolV2.Domain.AnnualServices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServices.DeleteAnnualService;

public record DeleteAnnualServiceCommand(AnnualService AnnualService) : IRequest;
