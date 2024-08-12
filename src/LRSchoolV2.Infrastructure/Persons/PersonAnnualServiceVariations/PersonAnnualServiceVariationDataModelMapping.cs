using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.Persons.PersonAnnualServiceVariations;

public class PersonAnnualServiceVariationDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<PersonAnnualServiceVariation, PersonAnnualServiceVariationDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.Person, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.BilledPerson, _ => (PersonDataModel?)null)
            .Map(inDataModel => inDataModel.SchoolYear, _ => (SchoolYearDataModel?)null)
            .Map(inDataModel => inDataModel.AnnualServiceVariation, _ => (AnnualServiceVariationDataModel?)null);
        
        TypeAdapterConfig<PersonAnnualServiceVariationDataModel, PersonAnnualServiceVariation>
            .NewConfig()
            .MapWith(inDataModel => new PersonAnnualServiceVariation(
                inDataModel.Id,
                inDataModel.Person.Adapt<Person>(),
                inDataModel.SchoolYear.Adapt<SchoolYear>(),
                inDataModel.AnnualServiceVariation.Adapt<AnnualServiceVariation>(),
                inDataModel.PaymentsCount,
                inDataModel.BilledPerson.Adapt<Person>(),
                inDataModel.ConsultantPaymentsCount
                ));
    }
}