using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Helpers;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobLogic _jobLogic;
       

        public JobsController(IJobLogic jobLogic)
        {
            _jobLogic = jobLogic;
        }

        public ActionResult Index(int? categoryid)
        {
            ViewBag.HasCategoryFilter = categoryid > 0;
            var result = _jobLogic.GetAll().Take(50).OrderByDescending(f=>f.Id);
            return View(result);
        }




    }
}
