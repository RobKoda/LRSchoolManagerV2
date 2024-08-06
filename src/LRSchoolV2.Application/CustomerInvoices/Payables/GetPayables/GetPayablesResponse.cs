using LRSchoolV2.Domain.CustomerInvoices;

namespace LRSchoolV2.Application.CustomerInvoices.Payables.GetPayables;

public record GetPayablesResponse(IEnumerable<Payable> Payables);