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

        public static bool IsAvailableToViewSalaryAndDocumentData(this ClaimsPrincipal context)
        {
            return context.HasClaim(x => x is
            {
                Type: UserClaimsProvider.PermissionClaimType,
                Value: UserClaimsProvider.ViewSalaryAndDocumentsData
            });
        }
    }
}
