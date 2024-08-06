using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.DeleteCheckDeposit;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.GetCheckDeposits;
using LRSchoolV2.Application.CheckDeposits.CheckDeposits.SaveCheckDeposit;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CheckDeposits;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CheckDeposits;

public class CheckDepositsService(
    ISender inMediator,
    IValidator<DeleteCheckDepositRequest> inDeleteCheckDepositRequestValidator,
    IValidator<SaveCheckDepositRequest> inSaveCheckDepositRequestValidator
    ) : IFrontDataService
{
    public async Task<IEnumerable<CheckDeposit>> GetCheckDepositsAsync() => 
        (await inMediator.Send(new GetCheckDepositsQuery())).CheckDeposits;

    public Task<Validation<string, Unit>> DeleteCheckDepositAsync(CheckDeposit inCheckDeposit) => 
        inMediator.SendRequestWithValidation<DeleteCheckDepositRequest, DeleteCheckDepositCommand>(new DeleteCheckDepositRequest(inCheckDeposit), inDeleteCheckDepositRequestValidator);

    public Task<Validation<string, Unit>> SaveCheckDepositAsync(CheckDeposit inCheckDeposit) => 
        inMediator.SendRequestWithValidation<SaveCheckDepositRequest, SaveCheckDepositCommand>(new SaveCheckDepositRequest(inCheckDeposit), inSaveCheckDepositRequestValidator);
}