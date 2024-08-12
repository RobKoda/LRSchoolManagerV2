using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GetConsultantInvoices;

public record GetConsultantInvoicesQuery : IRequest<GetConsultantInvoicesResponse>;