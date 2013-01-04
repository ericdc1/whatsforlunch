using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Lunch.Core.Logic;
using StructureMap;

namespace Lunch.Website.Services
{
    public class LunchAuthorizeAttribute : AuthorizeAttribute
    {
        private IWebSecurityService _webSecurityService;
        private IUserLogic _userLogic;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            _webSecurityService = ObjectFactory.GetInstance<IWebSecurityService>();
            _userLogic = ObjectFactory.GetInstance<IUserLogic>();

            // if the user is already authenticated let them in fast
            if (_webSecurityService.IsAuthenticated)
                return true;

            // if we're not authenticated and there's a guid check if it matches a user
            var guid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(httpContext.Request.QueryString.Get("guid")))
                guid = Guid.Parse(httpContext.Request.QueryString.Get("guid"));
            if (!_webSecurityService.IsAuthenticated && guid != Guid.Empty)
            {
                var user = _userLogic.Get(guid);

                // if a user has a matching guid let them in
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    // have to set the current principal as well as the cookie or we won't have it until refresh
                    var identity = new GenericIdentity(user.Email, "Forms");
                    httpContext.User = new GenericPrincipal(identity, new string[] { });
                    Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[] { });

                    return true;
                }
            }

            // let the base class handle the rest
            return base.AuthorizeCore(httpContext);
        }
    }
}