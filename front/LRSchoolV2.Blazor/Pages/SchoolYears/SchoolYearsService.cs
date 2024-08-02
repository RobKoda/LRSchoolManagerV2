using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;
using LRSchoolV2.Application.Common.SchoolYears.GetCurrentSchoolYear;
using LRSchoolV2.Application.Common.SchoolYears.GetSchoolYears;
using LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.Common;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.SchoolYears;

public class SchoolYearsService(
    ISender inMediator,
    IValidator<DeleteSchoolYearRequest> inDeleteSchoolYearRequestValidator,
    IValidator<SaveSchoolYearRequest> inSaveSchoolYearRequestValidator
    ) : IFrontDataService
{
    public async Task<IEnumerable<SchoolYear>> GetSchoolYearsAsync() => 
        (await inMediator.Send(new GetSchoolYearsQuery())).SchoolYears;

    public async Task<Option<SchoolYear>> GetCurrentSchoolYear() => 
        (await inMediator.Send(new GetCurrentSchoolYearQuery())).SchoolYear;

    public Task<Validation<string, Unit>> DeleteSchoolYearAsync(SchoolYear inSchoolYear) => 
        inMediator.SendRequestWithValidation<DeleteSchoolYearRequest, DeleteSchoolYearCommand>(new DeleteSchoolYearRequest(inSchoolYear.Id), inDeleteSchoolYearRequestValidator);

    public Task<Validation<string, Unit>> SaveSchoolYearAsync(SchoolYear inSchoolYear) => 
        inMediator.SendRequestWithValidation<SaveSchoolYearRequest, SaveSchoolYearCommand>(new SaveSchoolYearRequest(inSchoolYear), inSaveSchoolYearRequestValidator);
}