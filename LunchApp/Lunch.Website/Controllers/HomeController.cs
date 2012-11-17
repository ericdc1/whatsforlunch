using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lunch.Website.Models;

namespace Lunch.Website.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            var entities = new WhatsForLunchEntities();
            var model = entities.JobLogs.OrderByDescending(f=>f.LogDTM).Take(20);
            return View(model);
        }

    }
}
