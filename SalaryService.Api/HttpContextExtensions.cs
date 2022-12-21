using System.Security.Claims;

namespace SalaryService.Api
{
    public static class HttpContextExtensions
    {
        public static long GetCurrentUser(this HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return long.Parse(identity.Claims.Single(x => x.Type == "accountId").Value);
        }
    }
}
