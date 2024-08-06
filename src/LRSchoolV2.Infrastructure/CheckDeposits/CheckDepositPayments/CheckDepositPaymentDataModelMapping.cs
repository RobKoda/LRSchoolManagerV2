using LRSchoolV2.Domain.CheckDeposits;
using LRSchoolV2.Domain.CustomerPayments;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CheckDeposits.CheckDepositPayments;

public class CheckDepositPaymentDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<CheckDepositPayment, CheckDepositPaymentDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.CustomerPayment, _ => (CustomerPayment?) null);
    }
}