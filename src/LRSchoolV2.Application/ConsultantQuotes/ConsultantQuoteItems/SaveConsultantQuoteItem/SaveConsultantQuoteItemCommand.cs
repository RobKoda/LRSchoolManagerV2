using LRSchoolV2.Domain.ConsultantQuotes;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.SaveConsultantQuoteItem;

public record SaveConsultantQuoteItemCommand(ConsultantQuoteItem ConsultantQuoteItem) : IRequest;
