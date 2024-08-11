using LRSchoolV2.Application.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.GetConsultants;

public class GetConsultantsHandler : IRequestHandler<GetConsultantsQuery, GetConsultantsResponse>
{
    private readonly IConsultantsRepository _consultantsRepository;

    public GetConsultantsHandler(IConsultantsRepository inConsultantsRepository)
    {
        _consultantsRepository = inConsultantsRepository;
    }

    public async Task<GetConsultantsResponse> Handle(GetConsultantsQuery inRequest, CancellationToken inCancellationToken) =>
        new(await _consultantsRepository.GetConsultantsAsync());
}