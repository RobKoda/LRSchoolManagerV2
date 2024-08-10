using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.GetCheckDepositPaymentsPerCheckDeposit;

public class GetCheckDepositPaymentsPerCheckDepositRequestValidation(
    ICheckDepositsRepository inCheckDepositsRepository
    ) : AbstractValidator<GetCheckDepositPaymentsPerCheckDepositRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetCheckDepositPaymentsPerCheckDepositRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.CheckDepositId)
            .MustAsync((inCheckDepositId, _) => inCheckDepositsRepository.AnyCheckDepositAsync(inCheckDepositId))
            .WithMessage(GetCheckDepositPaymentsPerCheckDepositRequestValidationErrors.CheckDepositNotFound);
}