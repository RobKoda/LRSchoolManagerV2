using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceItems.GetConsultantInvoiceItemsPerConsultantInvoice;

public class GetConsultantInvoiceItemsPerConsultantInvoiceHandler(
    IConsultantInvoiceItemsRepository inConsultantInvoiceItemsRepository
) : IRequestHandler<GetConsultantInvoiceItemsPerConsultantInvoiceQuery, GetConsultantInvoiceItemsPerConsultantInvoiceResponse>
{
    public async Task<GetConsultantInvoiceItemsPerConsultantInvoiceResponse> Handle(GetConsultantInvoiceItemsPerConsultantInvoiceQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inConsultantInvoiceItemsRepository.GetConsultantInvoiceItemsPerConsultantInvoiceAsync(inRequest.ConsultantInvoiceId));
}