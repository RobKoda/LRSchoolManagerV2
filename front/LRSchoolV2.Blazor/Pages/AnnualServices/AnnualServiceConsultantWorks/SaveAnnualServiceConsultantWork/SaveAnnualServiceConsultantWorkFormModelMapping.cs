using LRSchoolV2.Domain.AnnualServices;
using Mapster;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Blazor.Pages.AnnualServices.AnnualServiceConsultantWorks.SaveAnnualServiceConsultantWork;

public class SaveAnnualServiceConsultantWorkFormModelMapping : IRegister
{
    public void Register(TypeAdapterConfig inConfig)
    {
        TypeAdapterConfig<SaveAnnualServiceConsultantWorkFormModel, AnnualServiceConsultantWork>
            .NewConfig()
            .MapWith(inFormModel => new AnnualServiceConsultantWork(
                inFormModel.Id,
                inFormModel.AnnualService,
                inFormModel.SchoolYear!,
                inFormModel.Consultant!,
                inFormModel.CommonWorkHours,
                inFormModel.CommonWorkHoursComment.Trim()
            ));
    }
}