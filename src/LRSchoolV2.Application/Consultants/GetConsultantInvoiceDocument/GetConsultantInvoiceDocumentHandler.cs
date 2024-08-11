using LanguageExt;
using LRSchoolV2.Application.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.Consultants.GetConsultantInvoiceDocument;

public class GetConsultantInvoiceDocumentHandler(IConsultantsRepository inConsultantsRepository) : IRequestHandler<GetConsultantInvoiceDocumentQuery, GetConsultantInvoiceDocumentResponse>
{
    public async Task<GetConsultantInvoiceDocumentResponse> Handle(GetConsultantInvoiceDocumentQuery inRequest, CancellationToken inCancellationToken) =>
        new(await inConsultantsRepository
            .GetConsultantInvoiceDocument(inRequest.ConsultantId)
            .Match(inSome => inSome.Length == 0 ? Option<byte[]>.None : inSome,
                () => Option<byte[]>.None));
}