using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ParentCare.API.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static string DisplayName(this ClaimsPrincipal principal) =>
            principal.Get("name")
            ?? principal.Get("http://schemas.microsoft.com/identity/claims/givenname")
            ?? principal.Get(ClaimTypes.Name);

        public static string Email(this ClaimsPrincipal principal) =>
            principal.Get(ClaimTypes.Email)
            ?? principal.Get("emails");

        public static string FirstName(this ClaimsPrincipal principal) =>
            principal.Get("given_name")
            ?? principal.Get("http://schemas.microsoft.com/identity/claims/givenname")
            ?? principal.Get(ClaimTypes.GivenName);

        public static string LastName(this ClaimsPrincipal principal) =>
            principal.Get("family_name")
            ?? principal.Get("http://schemas.microsoft.com/identity/claims/lastname")
            ?? principal.Get("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")
            ?? principal.Get("surname")
            ?? principal.Get(ClaimTypes.Name);

        public static int ID(this ClaimsPrincipal principal)
        {
            return 1;
            //return principal.FindFirst(x => x.Type == "Id)?.Value;
        }

        public static string Get(this ClaimsPrincipal principal, string claimName) =>
            principal.FindFirst(claimName)?.Value;
    }
}
