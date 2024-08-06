using MediatR;

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetUnlinkedChecks;

public record GetUnlinkedChecksQuery : IRequest<GetUnlinkedChecksResponse>;