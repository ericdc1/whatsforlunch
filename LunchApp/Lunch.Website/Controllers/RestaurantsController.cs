using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantLogic _restaurantLogic;
        private readonly IRestaurantTypeLogic _restaurantTypeLogic;
        
        public RestaurantsController(IRestaurantLogic restaurantLogic, IRestaurantTypeLogic restaurantTypeLogic)
        {
            _restaurantLogic = restaurantLogic;
            _restaurantTypeLogic = restaurantTypeLogic;
        }

        //
        // GET: /Restaurants/

        public ActionResult Index()
        {
            var results =
                _restaurantLogic.GetAll();

            return View(results);
        }

        //
        // GET: /Restaurants/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Restaurants/Create

        public ActionResult Create()
        {
            ViewBag.RestaurantTypes = new SelectList(_restaurantTypeLogic.GetAll(), "ID", "TypeName");

            return View();
        }

        //
        // POST: /Restaurants/Create

        [HttpPost]
        public ActionResult Create(Restaurant entity)
        {
            try
            {
                entity = _restaurantLogic.SaveOrUpdate(entity);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.RestaurantTypes = new SelectList(_restaurantTypeLogic.GetAll(), "ID", "TypeName");

                return View(entity);
            }
        }

        //
        // GET: /Restaurants/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Restaurants/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Restaurants/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Restaurants/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
