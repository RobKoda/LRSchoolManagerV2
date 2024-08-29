using LRSchoolV2.Application.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.GetConsultants;

public class GetConsultantsHandler(IConsultantsRepository inConsultantsRepository) : IRequestHandler<GetConsultantsQuery, GetConsultantsResponse>
{
    public async Task<GetConsultantsResponse> Handle(GetConsultantsQuery inRequest, CancellationToken inCancellationToken) =>
        new(await inConsultantsRepository.GetConsultantsAsync());
}