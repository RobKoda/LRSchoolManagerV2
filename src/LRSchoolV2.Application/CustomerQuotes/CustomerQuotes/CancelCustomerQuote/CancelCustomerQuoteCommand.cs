using LRSchoolV2.Domain.CustomerQuotes;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.CancelCustomerQuote;

public record CancelCustomerQuoteCommand(CustomerQuote CustomerQuote) : IRequest;