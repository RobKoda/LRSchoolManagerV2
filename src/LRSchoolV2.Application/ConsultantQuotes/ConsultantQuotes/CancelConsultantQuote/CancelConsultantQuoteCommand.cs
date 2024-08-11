using LRSchoolV2.Domain.ConsultantQuotes;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.CancelConsultantQuote;

public record CancelConsultantQuoteCommand(ConsultantQuote ConsultantQuote) : IRequest;