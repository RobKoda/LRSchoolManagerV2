using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationYearlyPrices;

public class AnnualServiceVariationYearlyPriceDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<AnnualServiceVariationYearlyPrice, AnnualServiceVariationYearlyPriceDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.AnnualServiceVariation, _ => (AnnualServiceVariationDataModel?)null)
            .Map(inDataModel => inDataModel.SchoolYear, _ => (SchoolYearDataModel?)null);
    }
}