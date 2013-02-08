using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;
using User = Lunch.Core.Models.User;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "Administrator")]
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

            foreach(var user in xmodel)
                user.Administrator = Roles.IsUserInRole(user.Email, LunchRoles.Administrator.ToString());
            
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

                if (model.Administrator)
                    Roles.AddUserToRole(model.Email, LunchRoles.Administrator.ToString());
                
                Roles.AddUserToRole(model.Email, "User");

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = _userLogic.Get(id, null);
            var model = Mapper.Map<User, ViewModels.UserEdit>(user);

            model.Administrator = Roles.IsUserInRole(model.Email, LunchRoles.Administrator.ToString());

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.UserEdit model)
        {
            if (ModelState.IsValid)
            {
                var xmodel = Mapper.Map<ViewModels.UserEdit, User>(model);
                _userLogic.Update(xmodel);

                if (model.Administrator)
                {
                    if (!Roles.IsUserInRole(model.Email, LunchRoles.Administrator.ToString()))
                        Roles.AddUserToRole(model.Email, LunchRoles.Administrator.ToString());
                }
                else
                {
                    if (Roles.IsUserInRole(model.Email, LunchRoles.Administrator.ToString()))
                        Roles.RemoveUserFromRole(model.Email, LunchRoles.Administrator.ToString());
                }
                    
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = _userLogic.Get(id, null);

            if (Roles.IsUserInRole(user.Email, LunchRoles.Administrator.ToString()))
                Roles.RemoveUserFromRole(user.Email, LunchRoles.Administrator.ToString());
            if (Roles.IsUserInRole(user.Email, LunchRoles.User.ToString()))
                Roles.RemoveUserFromRole(user.Email, LunchRoles.User.ToString());
            _webSecurityService.DeleteUserAndAccount(user.Email);

            return RedirectToAction("Index");
        }
    }
}
