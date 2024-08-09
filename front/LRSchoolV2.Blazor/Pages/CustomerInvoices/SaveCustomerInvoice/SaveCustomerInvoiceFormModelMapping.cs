using LRSchoolV2.Domain.CustomerInvoices;
using Mapster;
// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.CustomerInvoices.SaveCustomerInvoice;

public class SaveCustomerInvoiceFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveCustomerInvoiceFormModel, CustomerInvoice>
            .NewConfig()
            .Map(inCustomerInvoice => inCustomerInvoice.InvoiceCustomerName, inFormModel => inFormModel.Customer!.GetFullName())
            .Map(inCustomerInvoice => inCustomerInvoice.InvoiceCustomerAddress, inFormModel => inFormModel.Customer!.Address.GetFormattedAddress());
    }
}