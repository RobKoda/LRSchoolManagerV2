using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GenerateCustomerInvoices;

public record GenerateCustomerInvoicesQuery(IEnumerable<Payable> Payables) : IRequest<GenerateCustomerInvoicesResponse>;