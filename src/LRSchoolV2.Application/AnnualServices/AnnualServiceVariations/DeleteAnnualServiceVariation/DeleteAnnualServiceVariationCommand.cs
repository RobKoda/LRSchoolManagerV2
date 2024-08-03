using LRSchoolV2.Domain.AnnualServices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariations.DeleteAnnualServiceVariation;

public record DeleteAnnualServiceVariationCommand(AnnualServiceVariation AnnualServiceVariation) : IRequest;
