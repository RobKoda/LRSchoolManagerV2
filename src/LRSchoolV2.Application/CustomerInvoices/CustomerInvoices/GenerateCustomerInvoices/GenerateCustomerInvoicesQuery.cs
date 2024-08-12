using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GenerateCustomerInvoices;

public record GenerateCustomerInvoicesQuery(IEnumerable<CustomerInvoiceable> CustomerInvoiceables) : IRequest<GenerateCustomerInvoicesResponse>;