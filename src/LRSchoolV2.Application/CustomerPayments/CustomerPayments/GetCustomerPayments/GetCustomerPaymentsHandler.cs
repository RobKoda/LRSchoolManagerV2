using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetCustomerPayments;

public class GetCustomerPaymentsHandler : IRequestHandler<GetCustomerPaymentsQuery, GetCustomerPaymentsResponse>
{
    private readonly ICustomerPaymentsRepository _customerPaymentsRepository;

    public GetCustomerPaymentsHandler(ICustomerPaymentsRepository inCustomerPaymentsRepository)
    {
        _customerPaymentsRepository = inCustomerPaymentsRepository;
    }

    public async Task<GetCustomerPaymentsResponse> Handle(GetCustomerPaymentsQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _customerPaymentsRepository.GetCustomerPaymentsAsync());
}