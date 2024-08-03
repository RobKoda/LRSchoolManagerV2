using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.DeletePersonAnnualServiceVariation;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerPerson;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerService;
using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.SavePersonAnnualServiceVariation;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.Persons;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.Persons.Members.PersonServiceVariations;

public class PersonAnnualServiceVariationsService(
    ISender inMediator,
    IValidator<DeletePersonAnnualServiceVariationRequest> inDeletePersonServiceVariationRequestValidator,
    IValidator<SavePersonAnnualServiceVariationRequest> inSavePersonServiceVariationRequestValidator,
    IValidator<GetPersonAnnualServiceVariationsPerPersonRequest> inGetPersonServiceVariationsPerPersonRequestValidator,
    IValidator<GetPersonAnnualServiceVariationsPerServiceRequest> inGetPersonServiceVariationsPerServiceRequestValidator
    ) : IFrontDataService
{
    public async Task<Validation<string, IEnumerable<PersonAnnualServiceVariation>>> GetPersonServiceVariationsPerPersonAsync(Guid inPersonId)
    {
        var request = new GetPersonAnnualServiceVariationsPerPersonRequest(inPersonId);
        var result = await inMediator.SendRequestWithValidation<GetPersonAnnualServiceVariationsPerPersonRequest, GetPersonAnnualServiceVariationsPerPersonQuery, GetPersonAnnualServiceVariationsPerPersonResponse>(request, inGetPersonServiceVariationsPerPersonRequestValidator);
        return result.Map<IEnumerable<PersonAnnualServiceVariation>>(inSuccess => inSuccess.PersonAnnualServiceVariations);
    }

    public Task<Validation<string, Unit>> DeletePersonServiceVariationAsync(PersonAnnualServiceVariation inPersonAnnualServiceVariation)
    {
        var request = new DeletePersonAnnualServiceVariationRequest(inPersonAnnualServiceVariation);
        return inMediator.SendRequestWithValidation<DeletePersonAnnualServiceVariationRequest, DeletePersonAnnualServiceVariationCommand>(request, inDeletePersonServiceVariationRequestValidator);
    }

    public Task<Validation<string, Unit>> SavePersonServiceVariationAsync(PersonAnnualServiceVariation inPersonAnnualServiceVariation)
    {
        var request = new SavePersonAnnualServiceVariationRequest(inPersonAnnualServiceVariation);
        return inMediator.SendRequestWithValidation<SavePersonAnnualServiceVariationRequest, SavePersonAnnualServiceVariationCommand>(request, inSavePersonServiceVariationRequestValidator);
    }

    public async Task<Validation<string, IEnumerable<PersonAnnualServiceVariation>>> GetPersonServiceVariationsPerServiceAsync(Guid inServiceId)
    {
        var request = new GetPersonAnnualServiceVariationsPerServiceRequest(inServiceId);
        var result = await inMediator.SendRequestWithValidation<GetPersonAnnualServiceVariationsPerServiceRequest, GetPersonAnnualServiceVariationsPerServiceQuery, GetPersonAnnualServiceVariationsPerServiceResponse>(request, inGetPersonServiceVariationsPerServiceRequestValidator);
        return result.Map<IEnumerable<PersonAnnualServiceVariation>>(inSuccess => inSuccess.PersonAnnualServiceVariations);
    }
}