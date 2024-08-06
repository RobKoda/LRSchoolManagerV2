using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.SaveCheckDeposit;

public class SaveCheckDepositHandler : IRequestHandler<SaveCheckDepositCommand>
{
    private readonly ICheckDepositsRepository _checkDepositsRepository;

    public SaveCheckDepositHandler(ICheckDepositsRepository inCheckDepositsRepository)
    {
        _checkDepositsRepository = inCheckDepositsRepository;
    }

    public Task Handle(SaveCheckDepositCommand inRequest, CancellationToken inCancellationToken) => 
        _checkDepositsRepository.SaveCheckDepositAsync(inRequest.CheckDeposit);
}