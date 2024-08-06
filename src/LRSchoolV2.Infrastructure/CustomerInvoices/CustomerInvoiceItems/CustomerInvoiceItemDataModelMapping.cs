using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems;

public class CustomerInvoiceItemDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<CustomerInvoiceItem, CustomerInvoiceItemDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.CustomerInvoice, _ => (CustomerInvoiceDataModel?) null);
    }
}