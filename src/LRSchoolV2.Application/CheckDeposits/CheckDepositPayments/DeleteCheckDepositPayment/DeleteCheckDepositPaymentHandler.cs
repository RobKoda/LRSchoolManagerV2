using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.DeleteCheckDepositPayment;

public class DeleteCheckDepositPaymentHandler : IRequestHandler<DeleteCheckDepositPaymentCommand>
{
    private readonly ICheckDepositPaymentsRepository _checkDepositPaymentsRepository;

    public DeleteCheckDepositPaymentHandler(ICheckDepositPaymentsRepository inCheckDepositPaymentsRepository)
    {
        _checkDepositPaymentsRepository = inCheckDepositPaymentsRepository;
    }

    public Task Handle(DeleteCheckDepositPaymentCommand inRequest, CancellationToken inCancellationToken) => 
        _checkDepositPaymentsRepository.DeleteCheckDepositPaymentAsync(inRequest.CheckDepositPayment.Id);
}