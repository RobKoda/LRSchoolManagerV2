using LRSchoolV2.Domain.Consultants;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.Consultants.SetConsultantQuoteDocument;

public record SetConsultantQuoteDocumentCommand(Guid ConsultantId, byte[] QuoteDocument) : IRequest;
