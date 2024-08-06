using LRSchoolV2.Domain.CustomerPayments;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerPayments.CustomerPayments.SaveCustomerPayment;

public record SaveCustomerPaymentCommand(CustomerPayment CustomerPayment) : IRequest;
