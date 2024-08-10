using LRSchoolV2.Domain.CustomerQuotes;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes;

public class CustomerQuoteDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<CustomerQuote, CustomerQuoteDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.Customer, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.Items, _ => new List<CustomerQuoteItemDataModel>());

        TypeAdapterConfig<CustomerQuoteDataModel, CustomerQuote>
            .NewConfig()
            .MapWith(inDataModel => new CustomerQuote(
                inDataModel.Id,
                inDataModel.Number,
                inDataModel.Date,
                inDataModel.Customer.Adapt<Person>(),
                inDataModel.QuoteCustomerName,
                inDataModel.QuoteCustomerAddress,
                inDataModel.Items.Sum(inItem => inItem.UnitPrice * inItem.Quantity),
                inDataModel.EmailSent
                ));
    }
}