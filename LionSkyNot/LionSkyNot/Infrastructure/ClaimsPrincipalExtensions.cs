using System.Security.Claims;

namespace LionSkyNot.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static string GetUsername(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.Name).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
           => user.IsInRole("Administrator");

    }
}
