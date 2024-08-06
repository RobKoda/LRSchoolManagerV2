using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.SaveCheckDepositPayment;

public class SaveCheckDepositPaymentRequestValidation : AbstractValidator<SaveCheckDepositPaymentRequest>
{
    private readonly ICheckDepositsRepository _checkDepositsRepository;
    private readonly ICustomerPaymentsRepository _customerPaymentsRepository;

    public SaveCheckDepositPaymentRequestValidation(ICheckDepositsRepository inCheckDepositsRepository, ICustomerPaymentsRepository inCustomerPaymentsRepository)
    {
        _checkDepositsRepository = inCheckDepositsRepository;
        _customerPaymentsRepository = inCustomerPaymentsRepository;
    }
    
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCheckDepositPaymentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateCheckDeposit();
        ValidateCustomerPayment();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateCheckDeposit() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.CheckDepositId)
            .MustAsync((inCheckDepositId, _) => _checkDepositsRepository.AnyCheckDepositAsync(inCheckDepositId))
            .WithMessage(SaveCheckDepositPaymentRequestValidationErrors.CheckDepositNotFound);
    
    private void ValidateCustomerPayment() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.CustomerPayment)
            .MustAsync((inCustomerPayment, _) => _customerPaymentsRepository.AnyCustomerPaymentAsync(inCustomerPayment.Id))
            .WithMessage(SaveCheckDepositPaymentRequestValidationErrors.CustomerPaymentNotFound);
}