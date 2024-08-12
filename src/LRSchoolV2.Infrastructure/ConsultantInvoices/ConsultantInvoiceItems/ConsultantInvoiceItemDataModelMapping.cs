using static LanguageExt.Prelude;
using LRSchoolV2.Domain.ConsultantInvoices;
using LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoices;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantInvoices.ConsultantInvoiceItems;

public class ConsultantInvoiceItemDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<ConsultantInvoiceItem, ConsultantInvoiceItemDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.ConsultantInvoice, _ => (ConsultantInvoiceDataModel?) null)
            .Map(inDataModel => inDataModel.ReferenceId, inItem => inItem.ReferenceId.ToNullable());
        
        TypeAdapterConfig<ConsultantInvoiceItemDataModel, ConsultantInvoiceItem>
            .NewConfig()
            .Map(inItem => inItem.ReferenceId, inDataModel => Optional(inDataModel.ReferenceId));
    }
}