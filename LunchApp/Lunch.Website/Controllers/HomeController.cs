using System;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Data.Repositories;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;
using StackExchange.Exceptional;
using Lunch.Core.Jobs;
using StructureMap;
using RazorEngine;
using System.IO;

namespace Lunch.Website.Controllers
{
    [AllowAnonymous]
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

        public ActionResult Testemail()
        {
            var messagemodel = new MailDetails();
            var userlogic = new Lunch.Core.Logic.Implementations.UserLogic(new UserRepository());
            var _userlogic = ObjectFactory.GetInstance<IUserLogic>();
            var _restaurantlogic = ObjectFactory.GetInstance<IRestaurantLogic>();
            var todayschoices = _restaurantlogic.GenerateRestaurants().ToList();
            var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
            var link = "Test.com";
            User myModel = new User();
            myModel.FullName = "World";
            messagemodel.User = _userlogic.Get(38);
            messagemodel.Restaurants = _restaurantlogic.GenerateRestaurants();
            //var path = "C:/Users/edehaan/Documents/GitHub/whatsforlunch/LunchApp/Lunch.Website/Views/_MailTemplates/";
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Replace("bin", "");
            path = path + "Views\\_MailTemplates\\";
            path = path.Replace("\\", "/");
            path = path.Replace("file:/", "");
            var template = System.IO.File.ReadAllText((path + "Morning.cshtml"));

            string result = Razor.Parse(template, messagemodel);
            Helpers.SendMail("edehaan@hsc.wvu.edu", "test@test.com", "What's for Lunch Message of the day2", result);
            return Content(result);

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
