using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Lunch.Core.Logic;
using Lunch.Core.Logic.Implementations;
using Lunch.Core.RepositoryInterfaces;
using Lunch.Data.Repositories;
using Lunch.Website.Services;

namespace Lunch.Website.Controllers
{
    public class BaseController : Controller
    {
        //private readonly IWebSecurityService _webSecurityService;
        //private IUserLogic _userLogic;

        //public BaseController()
        //{
        //    _webSecurityService = new WebSecurityService();
        //    _userLogic = new UserLogic(new UserRepository());
        //}

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var guid = Guid.Empty;
        //    if (!string.IsNullOrWhiteSpace(Request.QueryString.Get("guid")))
        //        guid = Guid.Parse(Request.QueryString.Get("guid"));

        //    if (!_webSecurityService.IsAuthenticated && guid != Guid.Empty)
        //    {
        //        var user = _userLogic.Get(guid);

        //        Thread.CurrentPrincipal = HttpContext.User = new GenericPrincipal(new GenericIdentity(user.Email), new string[] { });
        //    }
                
        //}
    }
}
