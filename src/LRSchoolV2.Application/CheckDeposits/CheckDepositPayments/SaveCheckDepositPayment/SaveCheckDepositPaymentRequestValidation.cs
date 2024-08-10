using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.SaveCheckDepositPayment;

public class SaveCheckDepositPaymentRequestValidation(
    ICheckDepositsRepository inCheckDepositsRepository, 
    ICustomerPaymentsRepository inCustomerPaymentsRepository
    ) : AbstractValidator<SaveCheckDepositPaymentRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCheckDepositPaymentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateCheckDeposit();
        ValidateCustomerPayment();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateCheckDeposit() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.CheckDepositId)
            .MustAsync((inCheckDepositId, _) => inCheckDepositsRepository.AnyCheckDepositAsync(inCheckDepositId))
            .WithMessage(SaveCheckDepositPaymentRequestValidationErrors.CheckDepositNotFound);
    
    private void ValidateCustomerPayment() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.CustomerPayment)
            .MustAsync((inCustomerPayment, _) => inCustomerPaymentsRepository.AnyCustomerPaymentAsync(inCustomerPayment.Id))
            .WithMessage(SaveCheckDepositPaymentRequestValidationErrors.CustomerPaymentNotFound);
}