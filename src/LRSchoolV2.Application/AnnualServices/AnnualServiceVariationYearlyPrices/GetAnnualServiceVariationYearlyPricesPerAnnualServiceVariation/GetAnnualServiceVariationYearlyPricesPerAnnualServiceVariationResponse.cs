﻿using LRSchoolV2.Domain.AnnualServices;

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariation;

public record GetAnnualServiceVariationYearlyPricesPerAnnualServiceVariationResponse(IEnumerable<AnnualServiceVariationYearlyPrice> AnnualServiceVariationYearlyPrices);
