using LRSchoolV2.Domain.AnnualServices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.SaveAnnualServiceVariationYearlyPrice;

public record SaveAnnualServiceVariationYearlyPriceCommand(AnnualServiceVariationYearlyPrice AnnualServiceVariationYearlyPrice) : IRequest;
