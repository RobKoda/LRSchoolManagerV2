﻿using LRSchoolV2.Domain.ConsultantQuotes;

namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuotes.GetConsultantQuotes;

public record GetConsultantQuotesResponse(IEnumerable<ConsultantQuote> ConsultantQuotes);