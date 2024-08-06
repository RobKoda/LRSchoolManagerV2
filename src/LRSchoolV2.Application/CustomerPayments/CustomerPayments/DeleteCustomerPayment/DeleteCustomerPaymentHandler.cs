using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.DeleteCustomerPayment;

public class DeleteCustomerPaymentHandler : IRequestHandler<DeleteCustomerPaymentCommand>
{
    private readonly ICustomerPaymentsRepository _repository;

    public DeleteCustomerPaymentHandler(ICustomerPaymentsRepository inRepository)
    {
        _repository = inRepository;
    }

    public Task Handle(DeleteCustomerPaymentCommand inRequest, CancellationToken inCancellationToken) => 
        _repository.DeleteCustomerPaymentAsync(inRequest.CustomerPayment.Id);
}