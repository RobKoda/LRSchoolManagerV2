﻿using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantQuotes.ConsultantQuoteItems.GetConsultantQuoteItemsPerConsultantQuote;

public record GetConsultantQuoteItemsPerConsultantQuoteQuery(Guid ConsultantQuoteId) : IRequest<GetConsultantQuoteItemsPerConsultantQuoteResponse>;