﻿using MediatR;

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoiceables.GetCustomerInvoiceables;

public record GetCustomerInvoiceablesQuery : IRequest<GetCustomerInvoiceablesResponse>;