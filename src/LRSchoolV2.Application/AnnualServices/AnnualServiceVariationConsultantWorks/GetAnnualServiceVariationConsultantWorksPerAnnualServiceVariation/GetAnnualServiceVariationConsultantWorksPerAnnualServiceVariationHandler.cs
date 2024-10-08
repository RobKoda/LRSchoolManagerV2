﻿using LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationConsultantWorks.GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariation;

public class GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationHandler : IRequestHandler<GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationQuery, GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationResponse>
{
    private readonly IAnnualServiceVariationConsultantWorksRepository _annualServiceVariationConsultantWorksRepository;

    public GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationHandler(IAnnualServiceVariationConsultantWorksRepository inAnnualServiceVariationConsultantWorksRepository)
    {
        _annualServiceVariationConsultantWorksRepository = inAnnualServiceVariationConsultantWorksRepository;
    }

    public async Task<GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationResponse> Handle(GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationQuery inRequest, CancellationToken inCancellationToken) =>
        new(await _annualServiceVariationConsultantWorksRepository.GetAnnualServiceVariationConsultantWorksPerAnnualServiceVariationAsync(inRequest.AnnualServiceVariationId));
}