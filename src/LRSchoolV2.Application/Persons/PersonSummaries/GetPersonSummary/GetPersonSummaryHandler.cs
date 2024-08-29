using LRSchoolV2.Application.CustomerInvoices.CustomerInvoices.Persistence;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.Persistence;
using LRSchoolV2.Domain.CustomerPayments;
using LRSchoolV2.Domain.Persons;
using MediatR;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Application.Persons.PersonSummaries.GetPersonSummary;

public class GetPersonSummaryHandler(
    ICustomerInvoicesRepository inCustomerInvoicesRepository,
    ICustomerPaymentsRepository inCustomerPaymentsRepository
    ) : IRequestHandler<GetPersonSummaryQuery, GetPersonSummaryResponse>
{
    public async Task<GetPersonSummaryResponse> Handle(GetPersonSummaryQuery inRequest, CancellationToken inCancellationToken)
    {
        IList<PersonSummaryLine> result = [];
        
        var invoices = (await inCustomerInvoicesRepository.GetCustomerInvoicesAsync()).Where(inInvoice => inInvoice.Customer.Id == inRequest.Person.Id);
        foreach (var invoice in invoices)
        {
            result.Add(new PersonSummaryLine(
                invoice.Date,
                PersonSummaryLineType.CustomerInvoice, 
                invoice.Number,
                invoice.TotalToPay,
                0m
            ));
        }
        
        var payments = (await inCustomerPaymentsRepository.GetCustomerPaymentsAsync()).Where(inPayment => inPayment.Person.Id == inRequest.Person.Id);
        foreach (var payment in payments)
        {
            result.Add(new PersonSummaryLine(
                payment.Date,
                CustomerPaymentType.FromValue(payment.CustomerPaymentTypeValue).PersonSummaryLineType,
                payment.Reference,
                0m,
                payment.Amount
            ));
        }
        
        return new GetPersonSummaryResponse(result);
    }
}