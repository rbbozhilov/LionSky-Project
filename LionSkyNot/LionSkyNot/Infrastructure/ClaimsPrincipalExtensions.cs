using System.Security.Claims;

namespace LionSkyNot.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {

        public static string GetUserId(ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
