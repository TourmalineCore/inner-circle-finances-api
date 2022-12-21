using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace SalaryService.Api
{
    public class UserClaimsProvider : IUserClaimsProvider
    {
        public const string PermissionClaimType = "permissions";

        public const string CanViewFinanceForPayrollPermission = "CanViewFinanceForPayroll";

        public const string CanViewAnalyticPermission = "CanViewAnalytic";

        public const string CanManageEmployeesPermission = "CanManageEmployees";
        public Task<List<Claim>> GetUserClaimsAsync(string login)
        {
            throw new NotImplementedException();
        }
    }
}
