using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.SaveCheckDepositPayment;

public class SaveCheckDepositPaymentHandler : IRequestHandler<SaveCheckDepositPaymentCommand>
{
    private readonly ICheckDepositPaymentsRepository _checkDepositPaymentsRepository;

    public SaveCheckDepositPaymentHandler(ICheckDepositPaymentsRepository inCheckDepositPaymentsRepository)
    {
        _checkDepositPaymentsRepository = inCheckDepositPaymentsRepository;
    }

    public Task Handle(SaveCheckDepositPaymentCommand inRequest, CancellationToken inCancellationToken) => 
        _checkDepositPaymentsRepository.SaveCheckDepositPaymentAsync(inRequest.CheckDepositPayment);
}