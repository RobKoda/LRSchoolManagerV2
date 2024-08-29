using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Domain.Consultants;
using LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoiceItems;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoices;

public class ConsultantInvoiceDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<ConsultantInvoice, ConsultantInvoiceDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.Consultant, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.Items, _ => new List<ConsultantInvoiceItemDataModel>());

        TypeAdapterConfig<ConsultantInvoiceDataModel, ConsultantInvoice>
            .NewConfig()
            .MapWith(inDataModel => new ConsultantInvoice(
                inDataModel.Id,
                inDataModel.Number,
                inDataModel.Date,
                inDataModel.Consultant.Adapt<Consultant>(),
                inDataModel.InvoiceConsultantName,
                inDataModel.InvoiceConsultantAddress,
                inDataModel.Items.Sum(inItem => inItem.UnitPrice * inItem.Quantity),
                inDataModel.EmailSent
                ));
    }
}