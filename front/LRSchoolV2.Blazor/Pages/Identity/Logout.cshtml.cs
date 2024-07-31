using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LRSchoolV2.Blazor.Pages.Identity;

public class LogoutModel : PageModel
{
    // ReSharper disable once InconsistentNaming - Naming used in URL
    public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
    {
        await HttpContext
            .SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/");
    }
}