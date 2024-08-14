using LRSchoolV2.Domain.ConsultantInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GenerateConsultantInvoices;

public record GenerateConsultantInvoicesQuery(IEnumerable<ConsultantInvoiceable> ConsultantInvoiceables) : IRequest<GenerateConsultantInvoicesResponse>;