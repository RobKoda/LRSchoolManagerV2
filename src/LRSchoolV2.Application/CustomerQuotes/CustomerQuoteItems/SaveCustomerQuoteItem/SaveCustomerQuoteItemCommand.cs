using LRSchoolV2.Domain.CustomerQuotes;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.CustomerQuotes.CustomerQuoteItems.SaveCustomerQuoteItem;

public record SaveCustomerQuoteItemCommand(CustomerQuoteItem CustomerQuoteItem) : IRequest;
