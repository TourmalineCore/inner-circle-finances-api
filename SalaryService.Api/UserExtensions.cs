using System.Security.Claims;

namespace SalaryService.Api
{
    public static class UserExtensions
    {
        private const string CorporateEmailClaimType = "corporateEmail";

        public static string GetCorporateEmail(this ClaimsPrincipal context)
        {
            return context.FindFirstValue(CorporateEmailClaimType);
        }
    }
}
