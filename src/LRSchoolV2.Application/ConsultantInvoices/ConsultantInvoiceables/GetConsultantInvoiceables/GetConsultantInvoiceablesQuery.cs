﻿using MediatR;

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoiceables.GetConsultantInvoiceables;

public record GetConsultantInvoiceablesQuery : IRequest<GetConsultantInvoiceablesResponse>;