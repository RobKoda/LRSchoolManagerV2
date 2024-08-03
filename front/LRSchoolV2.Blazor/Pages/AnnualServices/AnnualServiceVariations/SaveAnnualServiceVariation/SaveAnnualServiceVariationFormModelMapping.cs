using LRSchoolV2.Domain.AnnualServices;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations.SaveAnnualServiceVariation;

public class SaveAnnualServiceVariationFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveAnnualServiceVariationFormModel, AnnualServiceVariation>
            .NewConfig()
            .MapWith(inFormModel => new AnnualServiceVariation(
                inFormModel.Id,
                inFormModel.AnnualService,
                inFormModel.Name.Trim(),
                inFormModel.InvoiceName.Trim(),
                null
            ));
    }
}