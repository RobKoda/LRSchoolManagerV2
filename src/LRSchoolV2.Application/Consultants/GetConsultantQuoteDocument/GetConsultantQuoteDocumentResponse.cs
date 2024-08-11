// ReSharper disable ClassNeverInstantiated.Global - Implicit use

using LanguageExt;

namespace LRSchoolV2.Application.Consultants.GetConsultantQuoteDocument;

public record GetConsultantQuoteDocumentResponse(Option<byte[]> ConsultantQuoteDocument);
