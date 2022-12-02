using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace SalaryService.Api
{
    public class UserClaimsProvider : IUserClaimsProvider
    {
        public const string PermissionClaimType = "Permissions";

        public const string CanViewFinanceForPayrollClaim = "CanViewFinanceForPayroll";

        public const string CanViewAnalyticClaim = "CanViewAnalytic";

        public const string CanManageEmployeesClaim = "CanManageEmployees";

        public Task<List<Claim>> GetUserClaimsAsync(string login)
        {
            return Task.FromResult(new List<Claim>
                {
                    new Claim(PermissionClaimType, CanViewFinanceForPayrollClaim),
                    new Claim(PermissionClaimType, CanViewAnalyticClaim),
                    new Claim(PermissionClaimType, CanManageEmployeesClaim)
                }
            );
        }
    }
}
