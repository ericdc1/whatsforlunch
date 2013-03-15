using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Lunch.Core.Logic;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;

namespace Lunch.Website.Controllers
{
    public class SetupWizardController : Controller
    {
        private readonly bool _setupComplete = Convert.ToBoolean(ConfigurationManager.AppSettings["SetupComplete"]);
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["AzureSQL"].ConnectionString;
        private readonly IDBVersionLogic _dbVersionLogic;
        private readonly IWebSecurityService _webSecurityService;

        public SetupWizardController(IDBVersionLogic dbVersionLogic, IWebSecurityService webSecurityService)
        {
            _dbVersionLogic = dbVersionLogic;
            _webSecurityService = webSecurityService;
        }

        public ActionResult Index()
        {
            if (_setupComplete)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Configuration");
        }

        public ActionResult Configuration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Configuration(ViewModels.SetupWizard.Configuration model)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    //Tryout the connection string to see if it's valid
                    _dbVersionLogic.CheckDbAccess(model.AzureSQL);

           
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("AzureSQL", "Provided connection string is not valid");
                    return View(model);
                }

                //setup database
                _dbVersionLogic.GenerateInitialDatabase(model.AzureSQL);


                var configObj = WebConfigurationManager.OpenWebConfiguration("~");
                var connStringSection = (ConnectionStringsSection)configObj.GetSection("connectionStrings");
                connStringSection.ConnectionStrings["AzureSQL"].ConnectionString = model.AzureSQL;
                var appSettingsSection = (AppSettingsSection)configObj.GetSection("appSettings");
                appSettingsSection.Settings["BaseURL"].Value = model.BaseURL;
                appSettingsSection.Settings["SetupComplete"].Value = "true";
                configObj.Save();

                return RedirectToAction("index", "Home");

            }
            return View(model);
        }

    }
}

