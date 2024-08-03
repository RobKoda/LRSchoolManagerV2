using LRSchoolV2.Application.Common.Addresses.Persistence;
using LRSchoolV2.Application.Persons.Persons.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.DeletePerson;

public class DeletePersonHandler(IPersonsRepository inPersonsRepository, IAddressesRepository inAddressesRepository) : IRequestHandler<DeletePersonCommand>
{
    public async Task Handle(DeletePersonCommand inRequest, CancellationToken inCancellationToken) =>
        await (await inPersonsRepository.GetPersonAsync(inRequest.Person.Id))
            .IfSomeAsync(async inPerson =>
            {
                await inPersonsRepository.DeletePersonAsync(inRequest.Person.Id);
                await inAddressesRepository.DeleteAddressAsync(inPerson.Address.Id);
            });
}