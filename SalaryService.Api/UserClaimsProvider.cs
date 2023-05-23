using System.Security.Claims;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Contract;

namespace SalaryService.Api
{
    public class UserClaimsProvider : IUserClaimsProvider
    {
        public const string PermissionClaimType = "permissions";

        public const string ViewPersonalProfile = "View personal profile";
        public const string EditPersonalProfile = "Edit personal profile";
        public const string ViewContacts = "View contacts";
        public const string ViewSalaryAndDocumentsData = "View salary and documents data";
        public const string EditFullEmployeesData = "Edit full employees data";
        public const string AccessAnalyticalForecastsPage = "Access to analytical forecasts page";

        public Task<List<Claim>> GetUserClaimsAsync(string login)
        {
            throw new NotImplementedException();
        }
    }
}
