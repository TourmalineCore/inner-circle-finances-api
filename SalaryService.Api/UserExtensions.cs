using System.Security.Claims;

namespace SalaryService.Api
{
    public static class UserExtensions
    {
        public static long GetAccountId(this ClaimsPrincipal context)
        {
            return long.Parse(context.FindFirstValue("accountId"));
        }
    }
}
