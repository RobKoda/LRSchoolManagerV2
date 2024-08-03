using LRSchoolV2.Domain.AnnualServices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.SaveAnnualServiceVariation;

public record SaveAnnualServiceVariationCommand(AnnualServiceVariation AnnualServiceVariation) : IRequest;
