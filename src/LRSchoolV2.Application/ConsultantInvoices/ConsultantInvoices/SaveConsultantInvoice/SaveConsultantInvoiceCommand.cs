using LRSchoolV2.Domain.ConsultantInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SaveConsultantInvoice;

public record SaveConsultantInvoiceCommand(ConsultantInvoice ConsultantInvoice) : IRequest;
