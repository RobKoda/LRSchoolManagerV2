using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.SetCustomerInvoiceEmailSent;

public class SetCustomerInvoiceEmailSentHandler(ICustomerInvoicesRepository inCustomerInvoicesRepository) : IRequestHandler<SetCustomerInvoiceEmailSentCommand>
{
    public async Task Handle(SetCustomerInvoiceEmailSentCommand inRequest, CancellationToken inCancellationToken) =>
        await (await inCustomerInvoicesRepository.GetCustomerInvoiceAsync(inRequest.CustomerInvoiceId))
            .IfSomeAsync(async inInvoice =>
            {
                inInvoice = inInvoice with { EmailSent = true };
                await inCustomerInvoicesRepository.SaveCustomerInvoiceAsync(inInvoice);
            });
}