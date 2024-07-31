using Microsoft.AspNetCore.Authorization;

namespace LRSchoolV2.Authentication;

public class EmailRequirement : IAuthorizationRequirement
{
    public readonly string[] AllowedEmails =
    [
        "robin.kebaili@gmail.com",
        "selvette55@gmail.com",
        "ludovic.heintzmann@gmail.com"
    ];
}