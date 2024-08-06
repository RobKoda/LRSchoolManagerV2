using LanguageExt;

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GenerateCustomerInvoices;

public record GenerateCustomerInvoicesResponse(Validation<string,Unit> Validation);