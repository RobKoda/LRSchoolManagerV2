using LRSchoolV2.Domain.CheckDeposits;
using MediatR;

// ReSharper disable ClassNeverInstantiated.Global - Implicit use

namespace LRSchoolV2.Application.CheckDeposits.CheckDeposits.DeleteCheckDeposit;

public record DeleteCheckDepositCommand(CheckDeposit CheckDeposit) : IRequest;
