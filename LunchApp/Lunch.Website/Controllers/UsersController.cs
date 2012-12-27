using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Data;

namespace Lunch.Website.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserLogic _userLogic;


        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public ActionResult Index()
        {
            var result = _userLogic.GetList(new { });
            var xmodel = Mapper.Map<IEnumerable<User>, IEnumerable<ViewModels.User>>(result);
            return View(xmodel);
        }

        public ActionResult Create()
        {
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] ViewModels.User model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.User, User>(model);
                _userLogic.SaveOrUpdate(xmodel);
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            var user = _userLogic.Get(id);
            var model = Mapper.Map<User, ViewModels.User>(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.User model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.User, User>(model);
                _userLogic.SaveOrUpdate(xmodel);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var rest = _userLogic.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
