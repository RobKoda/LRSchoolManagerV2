using LRSchoolV2.Domain.AnnualServices;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations.AnnualServiceVariationYearlyPrices.SaveAnnualServiceVariationYearlyPrice;

public class SaveServiceVariationYearlyPriceFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveServiceVariationYearlyPriceFormModel, AnnualServiceVariationYearlyPrice>
            .NewConfig()
            .MapWith(inFormModel => new AnnualServiceVariationYearlyPrice(
                inFormModel.Id,
                inFormModel.ServiceVariationId,
                inFormModel.SchoolYear!,
                inFormModel.Price,
                inFormModel.Margin
            ));
    }
}