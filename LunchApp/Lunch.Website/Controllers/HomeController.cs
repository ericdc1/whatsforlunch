using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Data.Repositories;
using Lunch.Website.Services;
using StackExchange.Exceptional;
using Lunch.Core.Jobs;
using StructureMap;
using RazorEngine;
using System.IO;
using Restaurant = Lunch.Website.ViewModels.Restaurant;
using User = Lunch.Website.ViewModels.User;

namespace Lunch.Website.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Keepalive()
        {
            return Content("alive");
        }




        /// <summary>
        /// This lets you access the error handler via a route in your application, secured by whatever
        /// mechanisms are already in place.
        /// </summary>
        /// <remarks>If mapping via RouteAttribute: [Route("errors/{path?}/{subPath?}")]</remarks>
        public ActionResult Exceptions()
        {
            var context = System.Web.HttpContext.Current;
            var page = new HandlerFactory().GetHandler(context, Request.RequestType, Request.Url.ToString(), Request.PathInfo);
            page.ProcessRequest(context);

            return null;
        }

    }
}
