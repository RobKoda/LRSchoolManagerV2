using LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.GetCustomerQuotes;

public class GetCustomerQuotesHandler(ICustomerQuotesRepository inCustomerQuotesRepository) : IRequestHandler<GetCustomerQuotesQuery, GetCustomerQuotesResponse>
{
    public async Task<GetCustomerQuotesResponse> Handle(GetCustomerQuotesQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inCustomerQuotesRepository.GetCustomerQuotesAsync());
}