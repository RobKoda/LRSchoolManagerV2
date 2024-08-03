using LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.SavePersonAnnualServiceVariation;

public class SavePersonAnnualServiceVariationHandler : IRequestHandler<SavePersonAnnualServiceVariationCommand>
{
    private readonly IPersonAnnualServiceVariationsRepository _personAnnualServiceVariationsRepository;

    public SavePersonAnnualServiceVariationHandler(IPersonAnnualServiceVariationsRepository inPersonAnnualServiceVariationsRepository)
    {
        _personAnnualServiceVariationsRepository = inPersonAnnualServiceVariationsRepository;
    }

    public Task Handle(SavePersonAnnualServiceVariationCommand inRequest, CancellationToken inCancellationToken) => 
        _personAnnualServiceVariationsRepository.SavePersonAnnualServiceVariationAsync(inRequest.PersonAnnualServiceVariation);
}