using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.DeleteCheckDepositPayment;
using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.GetCheckDepositPaymentsPerCheckDeposit;
using LRSchoolV2.Application.CheckDeposits.CheckDepositPayments.SaveCheckDepositPayment;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CheckDeposits;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CheckDeposits.CheckDepositPayments;

public class CheckDepositPaymentsService(
    ISender inMediator,
    IValidator<DeleteCheckDepositPaymentRequest> inDeleteCheckDepositPaymentRequestValidator,
    IValidator<GetCheckDepositPaymentsPerCheckDepositRequest> inGetCheckDepositPaymentsPerCheckDepositRequestValidator,
    IValidator<SaveCheckDepositPaymentRequest> inSaveCheckDepositPaymentRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<CheckDepositPayment>>> GetCheckDepositPaymentsPerCheckDepositAsync(Guid inCheckDepositId)
    {
        var request = new GetCheckDepositPaymentsPerCheckDepositRequest(inCheckDepositId);
        var result = await inMediator.SendRequestWithValidation<GetCheckDepositPaymentsPerCheckDepositRequest, GetCheckDepositPaymentsPerCheckDepositQuery, GetCheckDepositPaymentsPerCheckDepositResponse>(request, inGetCheckDepositPaymentsPerCheckDepositRequestValidator);
        return result.Map(inSuccess => inSuccess.CheckDepositPayments);
    }

    public Task<Validation<string, Unit>> DeleteCheckDepositPaymentAsync(CheckDepositPayment inCheckDepositPayment)
    {
        var request = new DeleteCheckDepositPaymentRequest(inCheckDepositPayment);
        return inMediator.SendRequestWithValidation<DeleteCheckDepositPaymentRequest, DeleteCheckDepositPaymentCommand>(request, inDeleteCheckDepositPaymentRequestValidator);
    }

    public Task<Validation<string, Unit>> SaveCheckDepositPaymentAsync(CheckDepositPayment inCheckDepositPayment)
    {
        var request = new SaveCheckDepositPaymentRequest(inCheckDepositPayment);
        return inMediator.SendRequestWithValidation<SaveCheckDepositPaymentRequest, SaveCheckDepositPaymentCommand>(request, inSaveCheckDepositPaymentRequestValidator);
    }
}