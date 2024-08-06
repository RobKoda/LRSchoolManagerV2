using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.GetCheckDepositPaymentsPerCheckDeposit;

public class GetCheckDepositPaymentsPerCheckDepositHandler : IRequestHandler<GetCheckDepositPaymentsPerCheckDepositQuery, GetCheckDepositPaymentsPerCheckDepositResponse>
{
    private readonly ICheckDepositPaymentsRepository _checkDepositPaymentsRepository;

    public GetCheckDepositPaymentsPerCheckDepositHandler(ICheckDepositPaymentsRepository inCheckDepositPaymentsRepository)
    {
        _checkDepositPaymentsRepository = inCheckDepositPaymentsRepository;
    }

    public async Task<GetCheckDepositPaymentsPerCheckDepositResponse> Handle(GetCheckDepositPaymentsPerCheckDepositQuery inRequest, CancellationToken inCancellationToken) =>
        new(await _checkDepositPaymentsRepository.GetCheckDepositPaymentsPerCheckDepositAsync(inRequest.CheckDepositId));
}