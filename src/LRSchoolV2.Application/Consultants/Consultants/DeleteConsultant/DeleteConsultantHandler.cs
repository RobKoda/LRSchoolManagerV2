using LRSchoolV2.Application.Common.Addresses.Persistence;
using LRSchoolV2.Application.Consultants.Consultants.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use

namespace LRSchoolV2.Application.Consultants.Consultants.DeleteConsultant;

public class DeleteConsultantHandler(
    IConsultantsRepository inConsultantsRepository, 
    IAddressesRepository inAddressesRepository
    ) : IRequestHandler<DeleteConsultantCommand>
{
    public async Task Handle(DeleteConsultantCommand inRequest, CancellationToken inCancellationToken) =>
        await (await inConsultantsRepository.GetConsultantAsync(inRequest.Consultant.Id))
        .IfSomeAsync(async inConsultant =>
        {
            await inConsultantsRepository.DeleteConsultantAsync(inRequest.Consultant.Id);
            await inAddressesRepository.DeleteAddressAsync(inConsultant.Address.Id);
        });
}