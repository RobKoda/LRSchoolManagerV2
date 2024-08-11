using LRSchoolV2.Domain.Consultants;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.Consultants.GetConsultantInvoiceDocument;

public record GetConsultantInvoiceDocumentQuery(Guid ConsultantId) : IRequest<GetConsultantInvoiceDocumentResponse>;
