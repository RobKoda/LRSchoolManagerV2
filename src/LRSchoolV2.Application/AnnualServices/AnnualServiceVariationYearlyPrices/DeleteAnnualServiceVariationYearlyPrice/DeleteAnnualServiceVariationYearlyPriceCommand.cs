﻿using LRSchoolV2.Domain.AnnualServices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.DeleteAnnualServiceVariationYearlyPrice;

public record DeleteAnnualServiceVariationYearlyPriceCommand(AnnualServiceVariationYearlyPrice AnnualServiceVariationYearlyPrice) : IRequest;
