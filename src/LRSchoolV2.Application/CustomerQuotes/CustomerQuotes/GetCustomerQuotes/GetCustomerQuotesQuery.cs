using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.GetCustomerQuotes;

public record GetCustomerQuotesQuery : IRequest<GetCustomerQuotesResponse>;