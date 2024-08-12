using LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.ConsultantInvoices.ConsultantInvoices.SetConsultantInvoiceEmailSent;

public class SetConsultantInvoiceEmailSentHandler(IConsultantInvoicesRepository inConsultantInvoicesRepository) : IRequestHandler<SetConsultantInvoiceEmailSentCommand>
{
    public async Task Handle(SetConsultantInvoiceEmailSentCommand inRequest, CancellationToken inCancellationToken) =>
        await (await inConsultantInvoicesRepository.GetConsultantInvoiceAsync(inRequest.ConsultantInvoiceId))
            .IfSomeAsync(async inInvoice =>
            {
                inInvoice = inInvoice with { EmailSent = true };
                await inConsultantInvoicesRepository.SaveConsultantInvoiceAsync(inInvoice);
            });
}