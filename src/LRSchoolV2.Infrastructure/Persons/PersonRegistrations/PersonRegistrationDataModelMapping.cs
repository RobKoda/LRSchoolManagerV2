using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.Persons.PersonRegistrations;

public class PersonRegistrationDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<PersonRegistration, PersonRegistrationDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.Person, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.BilledPerson, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.SchoolYear, _ => (SchoolYearDataModel?)null);

        TypeAdapterConfig<PersonRegistrationDataModel, PersonRegistration>
            .NewConfig()
            .MapWith(inDataModel => new PersonRegistration(
                    inDataModel.Id,
                    inDataModel.Person.Adapt<Person>(),
                    inDataModel.SchoolYear.Adapt<SchoolYear>(),
                    inDataModel.ImageRightsGranted,
                    inDataModel.BilledPerson.Adapt<Person>()
                ));
    }
}