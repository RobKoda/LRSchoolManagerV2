using LRSchoolV2.Domain.AnnualServices;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.SaveAnnualService;

public class SaveAnnualServiceFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveAnnualServiceFormModel, AnnualService>
            .NewConfig()
            .MapWith(inFormModel => new AnnualService(
                inFormModel.Id,
                inFormModel.Name.Trim()
            ));
    }
}