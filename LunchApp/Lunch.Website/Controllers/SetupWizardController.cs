using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lunch.Website.Controllers
{
    public class SetupWizardController : Controller
    {
        private readonly bool _setupComplete = Convert.ToBoolean(ConfigurationManager.AppSettings["SetupComplete"]);
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["AzureSQL"].ConnectionString;

        public ActionResult Index()
        {
            if (_setupComplete)
            {
                return RedirectToAction("Index", "Home");
            }
            if (_connectionString.Length > 5)
            {
                
            }
            return View();
        }

    }
}
