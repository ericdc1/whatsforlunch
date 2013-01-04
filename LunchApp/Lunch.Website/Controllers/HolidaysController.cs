using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Website.Services;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "Administrator")]
    public class HolidaysController : BaseController
    {
        private readonly IHolidayLogic _holidayLogic; 

        public HolidaysController(IHolidayLogic holidayLogic)
        {
            _holidayLogic = holidayLogic;
        }


        public ActionResult Index()
        {
            var blackoutDays = new SortedList();
            var model = new SortedList();
            var startdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var enddate = startdate.AddMonths(12);
            var excludedDays = System.Configuration.ConfigurationManager.AppSettings.Get("ExcludedDays").Split(Convert.ToChar(","));

            for (DateTime date = startdate; date <= enddate; date = date.AddDays(1))
            {
                if (excludedDays.Contains(date.DayOfWeek.ToString()))
                    blackoutDays.Add(date, date);
            }
            ViewBag.BlackoutDays = blackoutDays;

            var alldates = _holidayLogic.GetAll();
            foreach(var item in alldates)
            {
                model.Add(item.ExcludedDate, item.ExcludedDate);
            } 
            return View(model);
        }

        public JsonResult AddDate(DateTime selecteddate)
        {
            var holiday = new Holiday() {ExcludedDate = selecteddate};
            _holidayLogic.Insert(holiday);
            return Json("Saved");
        }

        public JsonResult RemoveDate(DateTime selecteddate)
        {
            var holiday = new Holiday() { ExcludedDate = selecteddate };
            _holidayLogic.Delete(holiday);
            return Json("Saved");
        }

    }
}
