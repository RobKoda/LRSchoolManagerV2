using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SetConsultantInvoiceEmailSent;

public record SetConsultantInvoiceEmailSentCommand(Guid ConsultantInvoiceId) : IRequest;
