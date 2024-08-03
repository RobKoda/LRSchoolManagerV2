using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Consultants.Consultants;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks;

public class AnnualServiceVariationConsultantWorkDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<AnnualServiceVariationConsultantWork, AnnualServiceVariationConsultantWorkDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.AnnualServiceVariation, _ => (AnnualServiceVariationDataModel?) null)
            .Map(inDataModel => inDataModel.SchoolYear, _ => (SchoolYearDataModel?) null)
            .Map(inDataModel => inDataModel.Consultant, _ => (ConsultantDataModel?) null);

    }
}