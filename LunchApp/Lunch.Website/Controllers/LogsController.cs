using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class LogsController : Controller
    {
        private readonly IJobLogLogic _jobLogLogic;
       

        public LogsController(IJobLogLogic jobLogLogic)
        {
            _jobLogLogic = jobLogLogic;
        }

        public ActionResult Index(int? categoryid)
        {
            ViewBag.HasCategoryFilter = categoryid > 0;
            var result = _jobLogLogic.GetAll().OrderByDescending(f=>f.Id).Take(250);
            return View(result);
        }




    }
}
