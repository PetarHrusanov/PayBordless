using System.Security.Claims;

namespace PayBordless;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAdministrator(this ClaimsPrincipal user)
        => user.IsInRole("Administrator");
}