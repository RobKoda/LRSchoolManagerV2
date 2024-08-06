using LRSchoolV2.Domain.CustomerPayments;

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetCustomerPayments;

public record GetCustomerPaymentsResponse(IEnumerable<CustomerPayment> CustomerPayments);
