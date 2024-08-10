using LRSchoolV2.Domain.CustomerQuotes;
using Mapster;
// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.CustomerQuotes.SaveCustomerQuote;

public class SaveCustomerQuoteFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveCustomerQuoteFormModel, CustomerQuote>
            .NewConfig()
            .Map(inCustomerQuote => inCustomerQuote.QuoteCustomerName, inFormModel => inFormModel.Customer!.GetFullName())
            .Map(inCustomerQuote => inCustomerQuote.QuoteCustomerAddress, inFormModel => inFormModel.Customer!.Address.GetFormattedAddress());
    }
}