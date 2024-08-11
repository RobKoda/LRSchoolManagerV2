using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.SetConsultantQuoteEmailSent;

public record SetConsultantQuoteEmailSentCommand(Guid ConsultantQuoteId) : IRequest;
