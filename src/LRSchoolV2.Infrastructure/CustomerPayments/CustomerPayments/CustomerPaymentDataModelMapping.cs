using LRSchoolV2.Domain.CustomerPayments;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments;

public class CustomerPaymentDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<CustomerPayment, CustomerPaymentDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.Person, _ => (PersonDataModel?)null);
        
        TypeAdapterConfig<CustomerPaymentDataModel, CustomerPayment>
            .NewConfig()
            .MapWith(inDataModel => new CustomerPayment(
                inDataModel.Id,
                inDataModel.Person.Adapt<Person>(),
                inDataModel.Date,
                inDataModel.CustomerPaymentTypeValue,
                inDataModel.Amount,
                inDataModel.Reference)
            );
    }
}