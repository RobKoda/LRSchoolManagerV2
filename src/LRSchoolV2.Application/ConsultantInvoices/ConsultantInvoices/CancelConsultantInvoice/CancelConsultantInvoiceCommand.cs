using LRSchoolV2.Domain.ConsultantInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.CancelConsultantInvoice;

public record CancelConsultantInvoiceCommand(ConsultantInvoice ConsultantInvoice) : IRequest;