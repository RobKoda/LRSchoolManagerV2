using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.Consultants.GetConsultantQuoteDocument;

public record GetConsultantQuoteDocumentQuery(Guid ConsultantId) : IRequest<GetConsultantQuoteDocumentResponse>;
