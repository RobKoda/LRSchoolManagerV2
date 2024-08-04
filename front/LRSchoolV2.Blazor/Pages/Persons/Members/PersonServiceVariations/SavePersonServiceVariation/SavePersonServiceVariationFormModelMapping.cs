using LRSchoolV2.Domain.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Persons.Members.PersonServiceVariations.SavePersonServiceVariation;

public class SavePersonServiceVariationFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SavePersonServiceVariationFormModel, PersonAnnualServiceVariation>
            .NewConfig()
            .MapWith(inFormModel => new PersonAnnualServiceVariation(
                inFormModel.Id,
                inFormModel.Person!,
                inFormModel.SchoolYear!,
                inFormModel.AnnualServiceVariation!,
                inFormModel.PaymentsCount,
                inFormModel.BilledPerson
            ));
    }
}