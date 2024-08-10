using LRSchoolV2.Domain.CustomerQuotes;
using LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems;

public class CustomerQuoteItemDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<CustomerQuoteItem, CustomerQuoteItemDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.CustomerQuote, _ => (CustomerQuoteDataModel?) null);
    }
}