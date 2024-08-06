using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;
using LRSchoolV2.Domain.CustomerPayments;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.SaveCustomerPayment;

public class SaveCustomerPaymentRequestValidation : AbstractValidator<SaveCustomerPaymentRequest>
{
    private readonly ICheckDepositPaymentsRepository _checkDepositPaymentsRepository;

    public SaveCustomerPaymentRequestValidation( 
        ICheckDepositPaymentsRepository inCheckDepositPaymentsRepository)
    {
        _checkDepositPaymentsRepository = inCheckDepositPaymentsRepository;
    }
    
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCustomerPaymentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateAmount();
        ValidateNonCheckIsNotInCustomerPayment();

        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateAmount() =>
        RuleFor(inRequest => inRequest.CustomerPayment.Amount)
            .Must(inAmount => inAmount > 0)
            .WithMessage(SaveCustomerPaymentRequestValidationErrors.InvalidAmount);

    private void ValidateNonCheckIsNotInCustomerPayment() =>
        RuleFor(inRequest => inRequest.CustomerPayment)
            .MustAsync(async (inPayment, _) => inPayment.CustomerPaymentTypeValue == CustomerPaymentType.BankCheck ||
                                               !await _checkDepositPaymentsRepository.AnyCheckDepositPaymentPerCheckIdAsync(inPayment.Id))
            .WithMessage(SaveCustomerPaymentRequestValidationErrors.NonCheckIsReferencedInCustomerPayment);
}