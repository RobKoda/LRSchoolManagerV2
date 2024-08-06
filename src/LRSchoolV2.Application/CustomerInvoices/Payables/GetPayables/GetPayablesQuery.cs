using MediatR;

namespace LRSchoolV2.Application.CustomerInvoices.Payables.GetPayables;

public record GetPayablesQuery : IRequest<GetPayablesResponse>;