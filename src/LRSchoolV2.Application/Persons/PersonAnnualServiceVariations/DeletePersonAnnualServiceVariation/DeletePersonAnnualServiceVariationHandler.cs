using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.DeletePersonAnnualServiceVariation;

public class DeletePersonAnnualServiceVariationHandler : IRequestHandler<DeletePersonAnnualServiceVariationCommand>
{
    private readonly IPersonAnnualServiceVariationsRepository _personAnnualServiceVariationsRepository;

    public DeletePersonAnnualServiceVariationHandler(IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository)
    {
        _personAnnualServiceVariationsRepository = inPersonAnnualServiceVariationsRepository;
    }

    public Task Handle(DeletePersonAnnualServiceVariationCommand inRequest, CancellationToken inCancellationToken) => 
        _personAnnualServiceVariationsRepository.DeletePersonAnnualServiceVariationAsync(inRequest.PersonAnnualServiceVariation.Id);
}