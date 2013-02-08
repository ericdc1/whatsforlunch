using System;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
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
        private Lunch.Website.Controllers.BaseController _aController;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            _webSecurityService = ObjectFactory.GetInstance<IWebSecurityService>();
            _userLogic = ObjectFactory.GetInstance<IUserLogic>();

            // if the user is already authenticated let them in fast
            if (_webSecurityService.IsAuthenticated)
            {
                var roleAuthorized = false;
                foreach (var role in Regex.Split(Roles, @"\s*,\s*"))
                {
                    roleAuthorized = httpContext.User.IsInRole(role);
                }
                if (!roleAuthorized)
                    return false;

                return true;
            }
                
            // if we're not authenticated and there's a guid check if it matches a user
            var guid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(httpContext.Request.QueryString.Get("guid")))
                guid = Guid.Parse(httpContext.Request.QueryString.Get("guid"));
            if (!_webSecurityService.IsAuthenticated && guid != Guid.Empty)
            {
                var user = _userLogic.Get(guid, null);

                // if a user has a matching guid let them in
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    _aController.CurrentUser = user;
                    // have to set the current principal as well as the cookie or we won't have it until refresh
                    var identity = new GenericIdentity(user.Email, "Forms");
                    httpContext.User = new GenericPrincipal(identity, new string[] { });
                    Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[] { });

                    return true;
                }
            }

            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                _aController = (Controllers.BaseController)filterContext.Controller;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot cast current controller to MVC Base Controller!", ex);
            }
            base.OnAuthorization(filterContext);
        }
    }
}