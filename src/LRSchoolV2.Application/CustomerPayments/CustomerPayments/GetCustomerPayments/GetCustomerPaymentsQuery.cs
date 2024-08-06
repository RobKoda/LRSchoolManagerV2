using MediatR;

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetCustomerPayments;

public record GetCustomerPaymentsQuery : IRequest<GetCustomerPaymentsResponse>;