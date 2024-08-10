using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.DeleteCustomerPayment;

public class DeleteCustomerPaymentRequestValidation(
    ICustomerPaymentsRepository inRepository
    ) : AbstractValidator<DeleteCustomerPaymentRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteCustomerPaymentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.CustomerPayment.Id)
            .MustAsync((inCustomerPaymentId, _) => inRepository.AnyCustomerPaymentAsync(inCustomerPaymentId))
            .WithMessage(DeleteCustomerPaymentRequestValidationErrors.CustomerPaymentNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CustomerPayment)
            .MustAsync((inCustomerPayment, _) => inRepository.CanCustomerPaymentBeDeleted(inCustomerPayment.Id))
            .WithMessage(DeleteCustomerPaymentRequestValidationErrors.CustomerPaymentCannotBeDeleted);
}