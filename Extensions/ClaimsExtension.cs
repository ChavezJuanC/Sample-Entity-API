using System.Security.Claims;

namespace api.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetUserName(this ClaimsPrincipal User)
        {
            return User.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value ?? String.Empty;
        }
    }
}