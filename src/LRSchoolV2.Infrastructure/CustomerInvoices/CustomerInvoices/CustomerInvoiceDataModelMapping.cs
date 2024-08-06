using LRSchoolV2.Domain.CustomerInvoices;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;

public class CustomerInvoiceDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<CustomerInvoice, CustomerInvoiceDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.Customer, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.Items, _ => new List<CustomerInvoiceItemDataModel>());

        TypeAdapterConfig<CustomerInvoiceDataModel, CustomerInvoice>
            .NewConfig()
            .MapWith(inDataModel => new CustomerInvoice(
                inDataModel.Id,
                inDataModel.Number,
                inDataModel.Date,
                inDataModel.Customer.Adapt<Person>(),
                inDataModel.InvoiceCustomerName,
                inDataModel.InvoiceCustomerAddress,
                inDataModel.Items.Sum(inItem => inItem.UnitPrice * inItem.Quantity),
                inDataModel.EmailSent
                ));
    }
}