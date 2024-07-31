using FluentValidation;
using LanguageExt;
using Mapster;
using MediatR;
using Unit = LanguageExt.Unit;
// ReSharper disable MemberCanBePrivate.Global - Extension methods

namespace LRSchoolV2.Application.Core;

public static class MediatorExtensions
{
    public static async Task<Validation<string, Unit>> SendRequestWithValidation<TRequest, TCommand>(this ISender inMediator, TRequest inRequest, IValidator<TRequest> inValidator)
    {
        var validationResult = await inValidator.ValidateAsync(inRequest);
        if (!validationResult.IsValid)
        {
            return Validation<string, Unit>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage)));
        }
        
        await inMediator.Send(inRequest.Adapt<TCommand>()!);
        return Validation<string, Unit>.Success(default!);
    }

    public static async Task<Validation<string, TResult>> SendRequestWithValidation<TRequest, TCommand, TResult>(this ISender inMediator, TRequest inRequest, IValidator<TRequest> inValidator)
    {
        var validationResult = await inValidator.ValidateAsync(inRequest);
        if (!validationResult.IsValid)
        {
            return Validation<string, TResult>.Fail(new Seq<string>(validationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage)));
        }

        var result = await inMediator.Send(inRequest.Adapt<TCommand>()!);
        return Validation<string, TResult>.Success((TResult) result!);
    }
}