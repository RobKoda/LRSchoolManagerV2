﻿using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.GetAnnualServiceConsultantWorksPerAnnualService;

public record GetAnnualServiceConsultantWorksPerAnnualServiceQuery(Guid AnnualServiceId) : IRequest<GetAnnualServiceConsultantWorksPerAnnualServiceResponse>;