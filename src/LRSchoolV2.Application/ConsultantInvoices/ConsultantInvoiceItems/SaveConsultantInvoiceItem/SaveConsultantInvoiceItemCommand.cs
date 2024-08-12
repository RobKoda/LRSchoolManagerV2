using LRSchoolV2.Domain.ConsultantInvoices;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.SaveConsultantInvoiceItem;

public record SaveConsultantInvoiceItemCommand(ConsultantInvoiceItem ConsultantInvoiceItem) : IRequest;
