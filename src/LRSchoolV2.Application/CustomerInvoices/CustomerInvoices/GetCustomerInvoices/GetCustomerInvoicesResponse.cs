﻿using LRSchoolV2.Domain.CustomerInvoices;

namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.GetCustomerInvoices;

public record GetCustomerInvoicesResponse(IEnumerable<CustomerInvoice> CustomerInvoices);