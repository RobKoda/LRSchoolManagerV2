using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.DeleteCheckDeposit;

public class DeleteCheckDepositHandler : IRequestHandler<DeleteCheckDepositCommand>
{
    private readonly ICheckDepositsRepository _repository;

    public DeleteCheckDepositHandler(ICheckDepositsRepository inRepository)
    {
        _repository = inRepository;
    }

    public Task Handle(DeleteCheckDepositCommand inRequest, CancellationToken inCancellationToken)
    {
        return _repository.DeleteCheckDepositAsync(inRequest.CheckDeposit.Id);
    }
}