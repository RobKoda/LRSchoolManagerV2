using LRSchoolV2.Domain.ConsultantQuotes;
using LRSchoolV2.Domain.Consultants;
using LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuoteItems;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.ConsultantQuotes.ConsultantQuotes;

public class ConsultantQuoteDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<ConsultantQuote, ConsultantQuoteDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.Consultant, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.Items, _ => new List<ConsultantQuoteItemDataModel>());

        TypeAdapterConfig<ConsultantQuoteDataModel, ConsultantQuote>
            .NewConfig()
            .MapWith(inDataModel => new ConsultantQuote(
                inDataModel.Id,
                inDataModel.Number,
                inDataModel.Date,
                inDataModel.Consultant.Adapt<Consultant>(),
                inDataModel.QuoteConsultantName,
                inDataModel.QuoteConsultantAddress,
                inDataModel.Items.Sum(inItem => inItem.UnitPrice * inItem.Quantity),
                inDataModel.EmailSent
                ));
    }
}