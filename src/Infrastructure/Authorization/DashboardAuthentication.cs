using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Dashboard;

namespace AspNetCoreIHostedService.Infrastructure.Authorization
{
    public class DashboardAuthentication : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;

            //Filters are useful to check if user is authenticated, claims, etc
            //var httpContext = context.GetHttpContext();
            //return httpContext.User.Identity.IsAuthenticated;
        }
    }
}
