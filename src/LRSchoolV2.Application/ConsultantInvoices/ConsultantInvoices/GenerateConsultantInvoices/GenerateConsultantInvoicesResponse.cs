using LanguageExt;

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GenerateConsultantInvoices;

public record GenerateConsultantInvoicesResponse(Validation<string,Unit> Validation);