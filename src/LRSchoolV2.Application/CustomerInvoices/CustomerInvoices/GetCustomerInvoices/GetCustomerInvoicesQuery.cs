using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GetCustomerInvoices;

public record GetCustomerInvoicesQuery : IRequest<GetCustomerInvoicesResponse>;