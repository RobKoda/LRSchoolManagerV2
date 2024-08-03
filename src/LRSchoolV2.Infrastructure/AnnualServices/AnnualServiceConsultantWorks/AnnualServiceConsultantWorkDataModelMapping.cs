using LRSchoolV2.Domain.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Consultants.Consultants;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks;

public class AnnualServiceConsultantWorkDataModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<AnnualServiceConsultantWork, AnnualServiceConsultantWorkDataModel>
            .NewConfig()
            .Map(inDataModel => inDataModel.AnnualService, _ => (AnnualServiceDataModel?) null)
            .Map(inDataModel => inDataModel.SchoolYear, _ => (SchoolYearDataModel?) null)
            .Map(inDataModel => inDataModel.Consultant, _ => (ConsultantDataModel?) null);
    }
}