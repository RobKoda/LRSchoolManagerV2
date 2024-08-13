using LRSchoolV2.Domain.ConsultantInvoices;
using Mapster;
// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.ConsultantInvoices.SaveConsultantInvoice;

public class SaveConsultantInvoiceFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveConsultantInvoiceFormModel, ConsultantInvoice>
            .NewConfig()
            .Map(inConsultantInvoice => inConsultantInvoice.InvoiceConsultantName, inFormModel => inFormModel.Consultant.GetFullName())
            .Map(inConsultantInvoice => inConsultantInvoice.InvoiceConsultantAddress, inFormModel => inFormModel.Consultant.Address.GetFormattedAddress());
    }
}