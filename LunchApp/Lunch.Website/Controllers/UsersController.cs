using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Core.Models;
using Lunch.Website.Services;

namespace Lunch.Website.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserLogic _userLogic;
        private readonly IWebSecurityService _webSecurityService;

        public UsersController(IUserLogic userLogic, IWebSecurityService webSecurityService)
        {
            _userLogic = userLogic;
            _webSecurityService = webSecurityService;
        }


        public ActionResult Index()
        {
            var result = _userLogic.GetList(new { });
            var xmodel = Mapper.Map<IEnumerable<User>, IEnumerable<ViewModels.User>>(result);
            return View(xmodel);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] ViewModels.UserCreate model)
        {
            if (ModelState.IsValid)
            {
                var guid = Guid.NewGuid();
                _webSecurityService.CreateUserAndAccount(model.Email, model.Password, new { model.FullName, model.SendMail1, model.SendMail2, model.SendMail3, model.SendMail4, GUID = guid });

                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            var user = _userLogic.Get(id);
            var model = Mapper.Map<User, ViewModels.UserEdit>(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.UserEdit model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.UserEdit, User>(model);
                _userLogic.Update(xmodel);
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
