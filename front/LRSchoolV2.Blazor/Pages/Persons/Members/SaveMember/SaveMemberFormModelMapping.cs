using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Persons;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Persons.Members.SaveMember;

public class SaveMemberFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveMemberFormModel, Person>
            .NewConfig()
            .MapWith(inFormModel => new Person(
                inFormModel.Id,
                inFormModel.LastName.Trim(),
                inFormModel.FirstName.Trim(),
                inFormModel.BirthDate,
                inFormModel.PhoneNumber.Trim(),
                inFormModel.Email.Trim(),
                new Address(
                    inFormModel.AddressId,
                    inFormModel.AddressStreet,
                    inFormModel.AddressStreetComplement.Trim(),
                    inFormModel.AddressZipCode.Trim(),
                    inFormModel.AddressCity.Trim()
                ),
                inFormModel.ContactPerson1,
                inFormModel.ContactPerson2,
                inFormModel.BillingToContactPerson1
            ));
    }
}