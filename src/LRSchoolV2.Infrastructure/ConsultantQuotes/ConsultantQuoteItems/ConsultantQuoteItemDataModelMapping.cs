using LRSchoolV2.Domain.ConsultantQuotes;
using LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuotes;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuoteItems;

public class ConsultantQuoteItemDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<ConsultantQuoteItem, ConsultantQuoteItemDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.ConsultantQuote, _ => (ConsultantQuoteDataModel?) null);
    }
}