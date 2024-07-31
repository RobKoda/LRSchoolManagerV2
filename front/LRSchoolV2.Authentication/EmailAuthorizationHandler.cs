using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LRSchoolV2.Authentication;

public class EmailAuthorizationHandler(
    IHttpContextAccessor inHttpContextAccessor
    ) : AuthorizationHandler<EmailRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext inContext, 
        EmailRequirement inRequirement)
    {
        var userEmail = inHttpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(inClaim => inClaim.Type == ClaimTypes.Email)?.Value;

        if (userEmail != null && inRequirement.AllowedEmails.Contains(userEmail))
        {
            inContext.Succeed(inRequirement);
        }

        return Task.CompletedTask;
    }
}