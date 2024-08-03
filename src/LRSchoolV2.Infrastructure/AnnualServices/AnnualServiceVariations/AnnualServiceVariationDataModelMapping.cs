using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;

public class AnnualServiceVariationDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<AnnualServiceVariation, AnnualServiceVariationDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.AnnualService, _ => (AnnualServiceDataModel?) null)
            .Map(inDataModel => inDataModel.ConsultantWorks, _ => new List<AnnualServiceConsultantWorkDataModel>())
            .Map(inDataModel => inDataModel.YearlyPrices, _ => new List<AnnualServiceVariationConsultantWorkDataModel>());

    }
}