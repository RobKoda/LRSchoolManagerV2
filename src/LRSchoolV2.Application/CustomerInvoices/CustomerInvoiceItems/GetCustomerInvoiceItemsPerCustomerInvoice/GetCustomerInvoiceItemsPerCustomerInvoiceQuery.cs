using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.GetCustomerInvoiceItemsPerCustomerInvoice;

public record GetCustomerInvoiceItemsPerCustomerInvoiceQuery(Guid CustomerInvoiceId) : IRequest<GetCustomerInvoiceItemsPerCustomerInvoiceResponse>;