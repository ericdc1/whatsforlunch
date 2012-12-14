using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Models;

namespace Lunch.Website.Controllers
{
    public class RestaurantTypesController : Controller
    {
        private readonly IRestaurantTypeLogic _restaurantTypeLogic;
        
        public RestaurantTypesController(IRestaurantTypeLogic restaurantTypeLogic)
        {
            _restaurantTypeLogic = restaurantTypeLogic;
        }

        //
        // GET: /RestaurantTypes/

        public ActionResult Index()
        {
            var results = _restaurantTypeLogic.GetAll();

            return View(results);
        }

        //
        // GET: /RestaurantTypes/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RestaurantTypes/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RestaurantTypes/Create

        [HttpPost]
        public ActionResult Create(RestaurantType entity)
        {
            try
            {
                _restaurantTypeLogic.SaveOrUpdate(entity);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(entity);
            }
        }

        //
        // GET: /RestaurantTypes/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RestaurantTypes/Edit/5

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
        // GET: /RestaurantTypes/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RestaurantTypes/Delete/5

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
