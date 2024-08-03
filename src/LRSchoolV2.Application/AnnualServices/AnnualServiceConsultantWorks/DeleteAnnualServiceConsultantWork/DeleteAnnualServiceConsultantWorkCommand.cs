using LRSchoolV2.Domain.AnnualServices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.AnnualServices.AnnualServiceConsultantWorks.DeleteAnnualServiceConsultantWork;

public record DeleteAnnualServiceConsultantWorkCommand(AnnualServiceConsultantWork AnnualServiceConsultantWork) : IRequest;
