using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.WebPages;

namespace Coolntern.Filters
{
    public class Authenticated : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.RedirectLocal("/");
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}