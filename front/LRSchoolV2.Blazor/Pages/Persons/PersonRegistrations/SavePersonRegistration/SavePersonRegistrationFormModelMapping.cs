using LRSchoolV2.Domain.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Persons.PersonRegistrations.SavePersonRegistration;

public class SavePersonRegistrationFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SavePersonRegistrationFormModel, PersonRegistration>
            .NewConfig()
            .MapWith(inFormModel => new PersonRegistration(
                inFormModel.Id,
                inFormModel.Person!,
                inFormModel.SchoolYear!,
                inFormModel.ImageRightsGranted,
                inFormModel.BilledPerson
            ));
    }
}