using LanguageExt;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.Consultants.GetConsultantInvoiceDocument;

public record GetConsultantInvoiceDocumentResponse(Option<byte[]> ConsultantInvoiceDocument);
