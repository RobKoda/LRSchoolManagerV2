using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.GetCheckDepositPaymentsPerCheckDeposit;

public class GetCheckDepositPaymentsPerCheckDepositRequestValidation : AbstractValidator<GetCheckDepositPaymentsPerCheckDepositRequest>
{
    private readonly ICheckDepositsRepository _checkDepositsRepository;

    public GetCheckDepositPaymentsPerCheckDepositRequestValidation(ICheckDepositsRepository inCheckDepositsRepository)
    {
        _checkDepositsRepository = inCheckDepositsRepository;
    }
    
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetCheckDepositPaymentsPerCheckDepositRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.CheckDepositId)
            .MustAsync((inCheckDepositId, _) => _checkDepositsRepository.AnyCheckDepositAsync(inCheckDepositId))
            .WithMessage(GetCheckDepositPaymentsPerCheckDepositRequestValidationErrors.CheckDepositNotFound);
}