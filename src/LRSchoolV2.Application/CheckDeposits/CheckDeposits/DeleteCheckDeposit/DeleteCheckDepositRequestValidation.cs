using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.DeleteCheckDeposit;

public class DeleteCheckDepositRequestValidation(
    ICheckDepositsRepository inRepository
    ) : AbstractValidator<DeleteCheckDepositRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteCheckDepositRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.CheckDeposit.Id)
            .MustAsync((inCheckDepositId, _) => inRepository.AnyCheckDepositAsync(inCheckDepositId))
            .WithMessage(DeleteCheckDepositRequestValidationErrors.CheckDepositNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CheckDeposit)
            .MustAsync((inCheckDeposit, _) => inRepository.CanCheckDepositBeDeleted(inCheckDeposit.Id))
            .WithMessage(DeleteCheckDepositRequestValidationErrors.CheckDepositCannotBeDeleted);
}