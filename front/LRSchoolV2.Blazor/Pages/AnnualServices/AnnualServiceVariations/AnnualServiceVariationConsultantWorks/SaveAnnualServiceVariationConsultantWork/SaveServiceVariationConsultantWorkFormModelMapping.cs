using LRSchoolV2.Domain.AnnualServices;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceVariations.AnnualServiceVariationConsultantWorks.SaveAnnualServiceVariationConsultantWork;

public class SaveServiceVariationConsultantWorkFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveServiceVariationConsultantWorkFormModel, AnnualServiceVariationConsultantWork>
            .NewConfig()
            .MapWith(inFormModel => new AnnualServiceVariationConsultantWork(
                inFormModel.Id,
                inFormModel.ServiceVariationId,
                inFormModel.SchoolYear!,
                inFormModel.Consultant!,
                inFormModel.IndividualWorkHours,
                inFormModel.IndividualWorkHoursComment.Trim()
            ));
    }
}