using LRSchoolV2.Domain.CheckDeposits;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Blazor.Pages.CheckDeposits.CheckDepositPayments.SaveCheckDepositPayment;

public class SaveCheckDepositPaymentFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveCheckDepositPaymentFormModel, CheckDepositPayment>
            .NewConfig()
            .MapWith(inFormModel => new CheckDepositPayment(
                inFormModel.Id,
                inFormModel.CheckDepositId,
                inFormModel.CustomerPayment!
            ));
    }
}