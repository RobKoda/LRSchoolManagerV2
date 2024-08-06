using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.SaveCustomerPayment;

public class SaveCustomerPaymentHandler : IRequestHandler<SaveCustomerPaymentCommand>
{
    private readonly ICustomerPaymentsRepository _customerPaymentsRepository;

    public SaveCustomerPaymentHandler(ICustomerPaymentsRepository inCustomerPaymentsRepository)
    {
        _customerPaymentsRepository = inCustomerPaymentsRepository;
    }

    public Task Handle(SaveCustomerPaymentCommand inRequest, CancellationToken inCancellationToken) => 
        _customerPaymentsRepository.SaveCustomerPaymentAsync(inRequest.CustomerPayment);
}