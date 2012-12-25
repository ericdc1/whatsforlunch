﻿using System;
using System.Globalization;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Helpers;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantTypeLogic _restaurantTypeLogic;


        public HomeController(IRestaurantLogic restaurantLogic, IRestaurantTypeLogic restaurantTypeLogic)
        {
            _restaurantLogic = restaurantLogic;
            _restaurantTypeLogic = restaurantTypeLogic;
        }

        public ActionResult Index(int? categoryid)
        {
            ViewBag.HasCategoryFilter = categoryid > 0;
            var result = _restaurantLogic.GetAllDetailed(categoryid);
            return View(result);
        }



        public ActionResult Keepalive()
        {
            return Content("alive");
        }

        public ActionResult Details(int id)
        {
            var model = _restaurantLogic.Get(id);
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View("Edit");
        }


        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] ViewModels.Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.Restaurant, Restaurant>(model);
                _restaurantLogic.SaveOrUpdate(xmodel);
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            var rest = _restaurantLogic.Get(id);
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            var model = Mapper.Map<Restaurant, ViewModels.Restaurant>(rest);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Restaurant model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.Restaurant, Restaurant>(model);
                _restaurantLogic.SaveOrUpdate(xmodel);
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantTypeList = _restaurantTypeLogic.GetAll();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var rest = _restaurantLogic.Get(id);
            _restaurantLogic.Delete(rest);
            return RedirectToAction("Index");
        }


        public ActionResult usertest()
        {
            var udb = new Lunch.Data.Repositories.UserRepository();
            var user = new User() { Email = "ecoffman@hsc.wvu.edu", FullName = "Eric Coffman", IsAdministrator = true, GUID = "hi" };
            var newid = udb.SaveOrUpdate(user);

            var x = udb.GetAll();
            var y = udb.Get(2);
            var z = udb.GetList(new { FullName = "Eric Coffman" });

            y.FullName = DateTime.Now.ToLongDateString();
            var zz = udb.SaveOrUpdate(y);


            return Content("hi");
        }
    }
}
