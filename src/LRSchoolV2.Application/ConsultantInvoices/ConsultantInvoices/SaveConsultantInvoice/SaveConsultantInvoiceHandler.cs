using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SaveConsultantInvoice;

public class SaveConsultantInvoiceHandler(IConsultantInvoicesRepository inConsultantInvoicesRepository) : IRequestHandler<SaveConsultantInvoiceCommand>
{
    public Task Handle(SaveConsultantInvoiceCommand inRequest, CancellationToken inCancellationToken) => 
        inConsultantInvoicesRepository.SaveConsultantInvoiceAsync(inRequest.ConsultantInvoice);
}