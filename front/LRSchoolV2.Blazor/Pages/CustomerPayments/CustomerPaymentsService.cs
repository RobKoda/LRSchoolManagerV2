using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.DeleteCustomerPayment;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetCustomerPayments;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.GetUnlinkedChecks;
using LRSchoolV2.Application.CustomerPayments.CustomerPayments.SaveCustomerPayment;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Domain.CustomerPayments;
using MediatR;
using Unit = LanguageExt.Unit;

namespace LRSchoolV2.Blazor.Pages.CustomerPayments;

public class CustomerPaymentsService(
    ISender inMediator,
    IValidator<SaveCustomerPaymentRequest> inSaveCustomerPaymentRequestValidator,
    IValidator<DeleteCustomerPaymentRequest> inDeleteCustomerPaymentRequestValidator
    ) : IFrontDataService
{
    public async Task<IEnumerable<CustomerPayment>> GetUnlinkedChecksAsync() => 
        (await inMediator.Send(new GetUnlinkedChecksQuery())).CustomerPayments;
    
    public async Task<IEnumerable<CustomerPayment>> GetCustomerPaymentsAsync() => 
        (await inMediator.Send(new GetCustomerPaymentsQuery())).CustomerPayments;

    public Task<Validation<string, Unit>> DeleteCustomerPaymentAsync(CustomerPayment inCustomerPayment) => 
        inMediator.SendRequestWithValidation<DeleteCustomerPaymentRequest, DeleteCustomerPaymentCommand>(new DeleteCustomerPaymentRequest(inCustomerPayment), inDeleteCustomerPaymentRequestValidator);

    public Task<Validation<string, Unit>> SaveCustomerPaymentAsync(CustomerPayment inCustomerPayment)
    {
        var request = new SaveCustomerPaymentRequest(inCustomerPayment);
        return inMediator.SendRequestWithValidation<SaveCustomerPaymentRequest, SaveCustomerPaymentCommand>(request, inSaveCustomerPaymentRequestValidator);
    }
}