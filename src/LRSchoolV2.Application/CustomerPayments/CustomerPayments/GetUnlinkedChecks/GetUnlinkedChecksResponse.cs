using LRSchoolV2.Domain.CustomerPayments;

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetUnlinkedChecks;

public record GetUnlinkedChecksResponse(IEnumerable<CustomerPayment> CustomerPayments);