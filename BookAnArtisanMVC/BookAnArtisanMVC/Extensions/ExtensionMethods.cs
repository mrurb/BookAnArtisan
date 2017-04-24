using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace BookAnArtisanMVC.Extensions
{
    public static class ExtensionMethods
    {
        // Why don't this work? :(
        public static string FirstName(this IPrincipal principal)
        {
            var claimsPrincipal = principal as ClaimsPrincipal;
            var personNameClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName);
            return personNameClaim.Value;
        }
    }
}