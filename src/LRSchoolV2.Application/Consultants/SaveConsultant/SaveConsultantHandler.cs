using LRSchoolV2.Application.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.SaveConsultant;

public class SaveConsultantHandler : IRequestHandler<SaveConsultantCommand>
{
    private readonly IConsultantsRepository _consultantsRepository;

    public SaveConsultantHandler(IConsultantsRepository inConsultantsRepository)
    {
        _consultantsRepository = inConsultantsRepository;
    }

    public Task Handle(SaveConsultantCommand inRequest, CancellationToken inCancellationToken) => 
        _consultantsRepository.SaveConsultantAsync(inRequest.Consultant);
}