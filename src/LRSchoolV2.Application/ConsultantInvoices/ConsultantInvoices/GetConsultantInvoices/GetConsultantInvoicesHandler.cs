using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GetConsultantInvoices;

public class GetConsultantInvoicesHandler(IConsultantInvoicesRepository inConsultantInvoicesRepository) : IRequestHandler<GetConsultantInvoicesQuery, GetConsultantInvoicesResponse>
{
    public async Task<GetConsultantInvoicesResponse> Handle(GetConsultantInvoicesQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inConsultantInvoicesRepository.GetConsultantInvoicesAsync());
}