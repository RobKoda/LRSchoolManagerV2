using LRSchoolV2.Application.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.SetConsultantInvoiceDocument;

public class SetConsultantInvoiceDocumentHandler(IConsultantsRepository inConsultantsRepository) : IRequestHandler<SetConsultantInvoiceDocumentCommand>
{
    public Task Handle(SetConsultantInvoiceDocumentCommand inRequest, CancellationToken inCancellationToken) => 
        inConsultantsRepository.SetConsultantInvoiceDocument(inRequest.ConsultantId, inRequest.InvoiceDocument);
}