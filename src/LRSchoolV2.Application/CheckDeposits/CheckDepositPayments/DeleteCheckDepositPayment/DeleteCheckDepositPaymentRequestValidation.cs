using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.DeleteCheckDepositPayment;

public class DeleteCheckDepositPaymentRequestValidation : AbstractValidator<DeleteCheckDepositPaymentRequest>
{
    private readonly ICheckDepositPaymentsRepository _checkDepositPaymentsRepository;

    public DeleteCheckDepositPaymentRequestValidation(ICheckDepositPaymentsRepository inCheckDepositPaymentsRepository)
    {
        _checkDepositPaymentsRepository = inCheckDepositPaymentsRepository;
    }
    
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteCheckDepositPaymentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        
        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.Id)
            .MustAsync((inCheckDepositPaymentId, _) => _checkDepositPaymentsRepository.AnyCheckDepositPaymentAsync(inCheckDepositPaymentId))
            .WithMessage(DeleteCheckDepositPaymentRequestValidationErrors.CheckDepositPaymentNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CheckDepositPayment.Id)
            .MustAsync((inCheckDepositPaymentId, _) => _checkDepositPaymentsRepository.CanCheckDepositPaymentBeDeletedAsync(inCheckDepositPaymentId))
            .WithMessage(DeleteCheckDepositPaymentRequestValidationErrors.CheckDepositPaymentCannotBeDeleted);
}