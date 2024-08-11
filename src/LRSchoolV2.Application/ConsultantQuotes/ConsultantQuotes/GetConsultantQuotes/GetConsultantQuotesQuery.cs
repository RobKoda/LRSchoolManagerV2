using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.GetConsultantQuotes;

public record GetConsultantQuotesQuery : IRequest<GetConsultantQuotesResponse>;