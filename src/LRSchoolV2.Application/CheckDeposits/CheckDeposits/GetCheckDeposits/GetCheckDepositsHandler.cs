using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.GetCheckDeposits;

public class GetCheckDepositsHandler : IRequestHandler<GetCheckDepositsQuery, GetCheckDepositsResponse>
{
    private readonly ICheckDepositsRepository _checkDepositsRepository;

    public GetCheckDepositsHandler(ICheckDepositsRepository inCheckDepositsRepository)
    {
        _checkDepositsRepository = inCheckDepositsRepository;
    }

    public async Task<GetCheckDepositsResponse> Handle(GetCheckDepositsQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _checkDepositsRepository.GetCheckDepositsAsync());
}