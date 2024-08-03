using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;

public class AnnualServiceDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<AnnualService, AnnualServiceDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.ConsultantWorks, _ => new List<AnnualServiceConsultantWorkDataModel>())
            .Map(inDataModel => inDataModel.Variations, _ => new List<AnnualServiceVariationDataModel>());

    }
}