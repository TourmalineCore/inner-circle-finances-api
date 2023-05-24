using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace SalaryService.Api
{
    public class UserClaimsProvider : IUserClaimsProvider
    {
        public const string PermissionClaimType = "permissions";

        public const string ViewPersonalProfile = "ViewPersonalProfile";
        public const string EditPersonalProfile = "EditPersonalProfile";
        public const string ViewContacts = "ViewContacts";
        public const string ViewSalaryAndDocumentsData = "ViewSalaryAndDocumentsData";
        public const string EditFullEmployeesData = "EditFullEmployeesData";
        public const string AccessAnalyticalForecastsPage = "AccessAnalyticalForecastsPage";

        public Task<List<Claim>> GetUserClaimsAsync(string login)
        {
            throw new NotImplementedException();
        }
    }
}
