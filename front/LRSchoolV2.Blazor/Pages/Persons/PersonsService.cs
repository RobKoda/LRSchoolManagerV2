using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.Persons.Persons.DeletePerson;
using LRSchoolV2.Application.Persons.Persons.GetMembers;
using LRSchoolV2.Application.Persons.Persons.GetNonMembers;
using LRSchoolV2.Application.Persons.Persons.GetPersons;
using LRSchoolV2.Application.Persons.Persons.GetUnbalancedPersons;
using LRSchoolV2.Application.Persons.Persons.SavePerson;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.Persons;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.Persons;

public class PersonsService(
    ISender inMediator,
    IValidator<DeletePersonRequest> inDeletePersonRequestValidator
    ) : IFrontDataService
{
    public async Task<IEnumerable<Person>> GetPersonsAsync() =>
        (await inMediator.Send(new GetPersonsQuery())).Persons;
    public async Task<IEnumerable<Person>> GetUnbalancedPersons() =>
        (await inMediator.Send(new GetUnbalancedPersonsQuery())).Persons;

    public async Task<OptionAsync<IEnumerable<Person>>> GetMembersAsync() => 
        (await inMediator.Send(new GetMembersQuery())).Persons;

    public async Task<OptionAsync<IEnumerable<Person>>> GetNonMembersAsync() =>
        (await inMediator.Send(new GetNonMembersQuery())).Persons;

    public Task<Validation<string, Unit>> DeletePersonAsync(Person inPerson)
    {
        var request = new DeletePersonRequest(inPerson);
        return inMediator.SendRequestWithValidation<DeletePersonRequest, DeletePersonCommand>(request, inDeletePersonRequestValidator);
    }

    public Task SaveMemberAsync(Person inPerson) => 
        SavePersonAsync(inPerson);

    public Task SavePersonAsync(Person inPerson) => 
        inMediator.Send(new SavePersonCommand(inPerson));
}