using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.SetConsultantInvoiceDocument;

public record SetConsultantInvoiceDocumentCommand(Guid ConsultantId, byte[] InvoiceDocument) : IRequest;
