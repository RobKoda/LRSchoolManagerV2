using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LRSchoolV2.Blazor.Pages.Identity;

[AllowAnonymous]
public class LoginModel : PageModel
{
    // ReSharper disable once InconsistentNaming - Naming used in URL    
    public IActionResult OnGetAsync(string? returnUrl = null)
    {
        const string provider = "Google";

        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = Url.Page("./Login",
                pageHandler: "Callback",
                values: new { returnUrl })
        };
        
        return new ChallengeResult(provider, authenticationProperties);
    }

    // ReSharper disable InconsistentNaming - Naming used in URL
    public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl = null, string? remoteError = null)
    {
        var GoogleUser = User.Identities.FirstOrDefault();

        if (GoogleUser is not { IsAuthenticated: true }) return LocalRedirect("/");
        
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            RedirectUri = Request.Host.Value
        };
            
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(GoogleUser),
            authProperties);

        return LocalRedirect("/");
    }
}