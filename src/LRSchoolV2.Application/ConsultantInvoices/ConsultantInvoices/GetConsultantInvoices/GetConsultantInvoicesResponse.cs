﻿using LRSchoolV2.Domain.ConsultantInvoices;

namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.GetConsultantInvoices;

public record GetConsultantInvoicesResponse(IEnumerable<ConsultantInvoice> ConsultantInvoices);