using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuotes.SetCustomerQuoteEmailSent;

public record SetCustomerQuoteEmailSentCommand(Guid CustomerQuoteId) : IRequest;
