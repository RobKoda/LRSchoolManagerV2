﻿using LRSchoolV2.Domain.Consultants;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.Consultants.SaveConsultant;

public record SaveConsultantCommand(Consultant Consultant) : IRequest;
