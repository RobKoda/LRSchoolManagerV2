using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetUnlinkedChecks;

public class GetUnlinkedChecksHandler : IRequestHandler<GetUnlinkedChecksQuery, GetUnlinkedChecksResponse>
{
    private readonly ICustomerPaymentsRepository _customerPaymentsRepository;

    public GetUnlinkedChecksHandler(ICustomerPaymentsRepository inCustomerPaymentsRepository)
    {
        _customerPaymentsRepository = inCustomerPaymentsRepository;
    }

    public async Task<GetUnlinkedChecksResponse> Handle(GetUnlinkedChecksQuery inRequest, CancellationToken inCancellationToken) => 
        new(await _customerPaymentsRepository.GetUnlinkedChecksAsync());
}