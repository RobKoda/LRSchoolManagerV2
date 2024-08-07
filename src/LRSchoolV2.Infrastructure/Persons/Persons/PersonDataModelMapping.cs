using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.Persons.PersonRegistrations;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.Persons.Persons;

public class PersonDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<Person, PersonDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.ContactPerson1, _ => (PersonDataModel?) null)
            .Map(inDataModel => inDataModel.ContactPerson2, _ => (PersonDataModel?) null)
            .Map(inDataModel => inDataModel.Registrations, _ => new List<PersonRegistrationDataModel>());

        TypeAdapterConfig<PersonDataModel, Person>
            .NewConfig()
            .MaxDepth(3)
            .MapWith(inDataModel => new Person(
                inDataModel.Id,
                inDataModel.LastName,
                inDataModel.FirstName
                ,inDataModel.BirthDate,
                inDataModel.PhoneNumber,
                inDataModel.Email,
                inDataModel.Address.Adapt<Address>(),
                inDataModel.ContactPerson1.Adapt<Person>(),
                inDataModel.ContactPerson2.Adapt<Person>(),
                inDataModel.BillingToContactPerson1,
                inDataModel.CustomerInvoices.SelectMany(inInvoice => inInvoice.Items).Sum(inItem => inItem.UnitPrice * inItem.Quantity),
                inDataModel.CustomerPayments.Sum(inPayment => inPayment.Amount)
                ));
    }
}