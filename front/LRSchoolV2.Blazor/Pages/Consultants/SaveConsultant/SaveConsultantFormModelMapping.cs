using LRSchoolV2.Domain.Common;
using LRSchoolV2.Domain.Consultants;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.Consultants.SaveConsultant;

public class SaveConsultantFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveConsultantFormModel, Consultant>
            .NewConfig()
            .MapWith(inFormModel => new Consultant(
                inFormModel.Id,
                inFormModel.LastName.Trim(),
                inFormModel.FirstName.Trim(),
                inFormModel.CompanyName.Trim(),
                inFormModel.PhoneNumber.Trim(),
                inFormModel.Email.Trim(),
                new Address(
                    inFormModel.AddressId,
                    inFormModel.AddressStreet.Trim(),
                    inFormModel.AddressStreetComplement.Trim(),
                    inFormModel.AddressZipCode.Trim(),
                    inFormModel.AddressCity.Trim()
                    ),
                inFormModel.Iban.Trim(),
                inFormModel.BicCode.Trim()
            ));
    }
}