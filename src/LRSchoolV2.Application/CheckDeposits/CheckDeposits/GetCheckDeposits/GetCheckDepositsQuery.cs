using MediatR;

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.GetCheckDeposits;

public record GetCheckDepositsQuery : IRequest<GetCheckDepositsResponse>;