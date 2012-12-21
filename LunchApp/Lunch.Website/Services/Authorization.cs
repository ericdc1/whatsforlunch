using System;
using System.Collections.Generic;
using System.Linq;
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


            var guid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(httpContext.Request.QueryString.Get("guid")))
                guid = Guid.Parse(httpContext.Request.QueryString.Get("guid"));

            var isAuthorized = base.AuthorizeCore(httpContext);
            if (isAuthorized)
                return true;

            if (!_webSecurityService.IsAuthenticated && guid != Guid.Empty)
            {
                var user = _userLogic.Get(guid);

                var identity = new GenericIdentity(user.Email, "Forms");
                httpContext.User = new GenericPrincipal(identity, new string[] { });
                Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[] { });
            }

            return base.AuthorizeCore(httpContext);


            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (isAuthorized)
            //{
            //    var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            //    if (authCookie != null)
            //    {
            //        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            //        var identity = new GenericIdentity(authTicket.Name, "Forms");
            //        var principal = new GenericPrincipal(identity, new string[] { });
            //        httpContext.User = principal;
            //    }
            //}
            //return isAuthorized;
        }
    }
}