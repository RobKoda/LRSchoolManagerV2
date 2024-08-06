using LRSchoolV2.Domain.CheckDeposits;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits;

public class CheckDepositDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<CheckDepositDataModel, CheckDeposit>
            .NewConfig()
            .Map(inCheckDeposit => inCheckDeposit.Total, inDataModel => inDataModel.CheckDepositPayments.Sum(inPayment => inPayment.CustomerPayment!.Amount));
    }
}