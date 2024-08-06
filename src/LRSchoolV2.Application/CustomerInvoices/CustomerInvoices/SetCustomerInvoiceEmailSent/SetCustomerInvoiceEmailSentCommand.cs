using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SetCustomerInvoiceEmailSent;

public record SetCustomerInvoiceEmailSentCommand(Guid CustomerInvoiceId) : IRequest;
