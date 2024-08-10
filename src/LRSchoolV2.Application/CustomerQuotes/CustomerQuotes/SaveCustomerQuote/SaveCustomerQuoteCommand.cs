using LRSchoolV2.Domain.CustomerQuotes;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SaveCustomerQuote;

public record SaveCustomerQuoteCommand(CustomerQuote CustomerQuote) : IRequest;
