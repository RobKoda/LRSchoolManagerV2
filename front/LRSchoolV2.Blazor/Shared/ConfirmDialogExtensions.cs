using MudBlazor;

namespace LRSchoolV2.Blazor.Shared;

public static class ConfirmDialogExtensions
{
    public static Task<IDialogReference> Confirm(this IDialogService inDialogService, string inContent, string inTitle, string inCancelButtonText = "Non", string inSuccessButtonText = "Oui")
    {
        var theParameters = new DialogParameters
        {
            { "ContentText", inContent },
            { "CancelButtonText", inCancelButtonText },
            { "CancelColor", Color.Error },
            { "SuccessButtonText", inSuccessButtonText },
            { "SuccessColor", Color.Success }
        };

        return inDialogService.ShowAsync<ConfirmDialog>(inTitle, theParameters);
    }
}