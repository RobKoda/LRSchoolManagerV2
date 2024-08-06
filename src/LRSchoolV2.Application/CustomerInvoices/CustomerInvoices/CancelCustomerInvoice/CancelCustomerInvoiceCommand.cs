using LRSchoolV2.Domain.CustomerInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.CancelCustomerInvoice;

public record CancelCustomerInvoiceCommand(CustomerInvoice CustomerInvoice) : IRequest;