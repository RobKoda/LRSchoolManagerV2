using LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.GetConsultantQuotes;

public class GetConsultantQuotesHandler(IConsultantQuotesRepository inConsultantQuotesRepository) : IRequestHandler<GetConsultantQuotesQuery, GetConsultantQuotesResponse>
{
    public async Task<GetConsultantQuotesResponse> Handle(GetConsultantQuotesQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inConsultantQuotesRepository.GetConsultantQuotesAsync());
}