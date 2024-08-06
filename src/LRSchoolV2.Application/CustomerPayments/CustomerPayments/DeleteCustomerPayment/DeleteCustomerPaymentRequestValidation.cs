using FluentValidation;
using FluentValidation.Results;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.DeleteCustomerPayment;

public class DeleteCustomerPaymentRequestValidation : AbstractValidator<DeleteCustomerPaymentRequest>
{
    private readonly ICustomerPaymentsRepository _repository;

    public DeleteCustomerPaymentRequestValidation(ICustomerPaymentsRepository inRepository)
    {
        _repository = inRepository;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteCustomerPaymentRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();

        return base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.CustomerPayment.Id)
            .MustAsync((inCustomerPaymentId, _) => _repository.AnyCustomerPaymentAsync(inCustomerPaymentId))
            .WithMessage(DeleteCustomerPaymentRequestValidationErrors.CustomerPaymentNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.CustomerPayment)
            .MustAsync((inCustomerPayment, _) => _repository.CanCustomerPaymentBeDeleted(inCustomerPayment.Id))
            .WithMessage(DeleteCustomerPaymentRequestValidationErrors.CustomerPaymentCannotBeDeleted);
}