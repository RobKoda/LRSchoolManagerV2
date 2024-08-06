using LRSchoolV2.Domain.CustomerPayments;
using Mapster;
// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.CustomerPayments.SaveCustomerPayment;

public class SaveCustomerPaymentFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveCustomerPaymentFormModel, CustomerPayment>
            .NewConfig()
            .Map(inCustomerPayment => inCustomerPayment.CustomerPaymentTypeValue, inFormModel => inFormModel.CustomerInvoicePaymentType!.Value);
    }
}