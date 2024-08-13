using LRSchoolV2.Domain.ConsultantQuotes;
using Mapster;
// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.ConsultantQuotes.SaveConsultantQuote;

public class SaveConsultantQuoteFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveConsultantQuoteFormModel, ConsultantQuote>
            .NewConfig()
            .Map(inConsultantQuote => inConsultantQuote.QuoteConsultantName, inFormModel => inFormModel.Consultant.GetFullName())
            .Map(inConsultantQuote => inConsultantQuote.QuoteConsultantAddress, inFormModel => inFormModel.Consultant.Address.GetFormattedAddress());
    }
}