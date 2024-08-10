using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.DeleteCheckDepositPayment;

public class DeleteCheckDepositPaymentRequestValidation(
    ICheckDepositPaymentsRepository inCheckDepositPaymentsRepository
    ) : AbstractValidator<DeleteCheckDepositPaymentRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteCheckDepositPaymentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.Id)
            .MustAsync((inCheckDepositPaymentId, _) => inCheckDepositPaymentsRepository.AnyCheckDepositPaymentAsync(inCheckDepositPaymentId))
            .WithMessage(DeleteCheckDepositPaymentRequestValidationErrors.CheckDepositPaymentNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.Id)
            .MustAsync((inCheckDepositPaymentId, _) => inCheckDepositPaymentsRepository.CanCheckDepositPaymentBeDeletedAsync(inCheckDepositPaymentId))
            .WithMessage(DeleteCheckDepositPaymentRequestValidationErrors.CheckDepositPaymentCannotBeDeleted);
}