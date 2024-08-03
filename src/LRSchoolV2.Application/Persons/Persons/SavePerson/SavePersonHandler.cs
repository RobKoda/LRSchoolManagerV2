using LRSchoolV2.Application.Persons.Persons.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.Persons.SavePerson;

public class SavePersonHandler : IRequestHandler<SavePersonCommand>
{
    private readonly IPersonsRepository _personsRepository;

    public SavePersonHandler(IPersonsRepository inPersonsRepository)
    {
        _personsRepository = inPersonsRepository;
    }

    public Task Handle(SavePersonCommand inRequest, CancellationToken inCancellationToken) => 
        _personsRepository.SavePersonAsync(inRequest.Person);
}