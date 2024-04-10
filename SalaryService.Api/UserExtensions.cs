using System.Security.Claims;

namespace SalaryService.Api
{
    public static class UserExtensions
    {
        private const string CorporateEmailClaimType = "corporateEmail";

        private const string TenantIdClaimType = "tenantId";

        public static string GetCorporateEmail(this ClaimsPrincipal context)
        {
            return context.FindFirstValue(CorporateEmailClaimType);
        }

        public static long GetTenantId(this ClaimsPrincipal context)
        {
            var tenantId = context.FindFirstValue(TenantIdClaimType);

            if (string.IsNullOrEmpty(tenantId))
            {
                throw new NullReferenceException("Tenant id is null or empty");
            }

            return long.Parse(tenantId);
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
