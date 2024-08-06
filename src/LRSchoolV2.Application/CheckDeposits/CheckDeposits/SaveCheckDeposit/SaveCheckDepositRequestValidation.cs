using FluentValidation;
using FluentValidation.Results;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.SaveCheckDeposit;

public class SaveCheckDepositRequestValidation : AbstractValidator<SaveCheckDepositRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<SaveCheckDepositRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateContentType();

        return base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateContentType() =>
        RuleFor(inRequest => inRequest.CheckDeposit.Number)
            .Must(inContentType => inContentType.Length <= 64)
            .WithMessage(SaveCheckDepositRequestValidationErrors.NumberTooLong);
}