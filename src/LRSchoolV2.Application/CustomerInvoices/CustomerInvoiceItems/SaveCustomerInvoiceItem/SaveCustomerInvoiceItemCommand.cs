using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceItems.SaveCustomerInvoiceItem;

public record SaveCustomerInvoiceItemCommand(CustomerInvoiceItem CustomerInvoiceItem) : IRequest;
