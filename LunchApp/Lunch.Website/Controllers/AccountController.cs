using System;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Lunch.Core.Logic;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;

namespace Lunch.Website.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IWebSecurityService _webSecurityService;
        private readonly IUserLogic _userLogic;

        public AccountController(IWebSecurityService webSecurityService, IUserLogic userLogic)
        {
            _webSecurityService = webSecurityService;
            _userLogic = userLogic;
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return RedirectToAction("LogOn");
        }

        [AllowAnonymous]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_webSecurityService.Login(model.UserName, model.Password, model.RememberMe))
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("LogOff");
        }

        public ActionResult LogOff()
        {
            _webSecurityService.Logout();

            return RedirectToAction("Index", "Home");
        }

        //public ActionResult Register()
        //{
        //    ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        var requireEmailConfirmation = false;
        //        var token = WebSecurityService.CreateUserAndAccount(model.Email, model.Password, new {FullName = model.UserName, GUID = Guid.NewGuid()});

        //        if (requireEmailConfirmation)
        //        {
        //            // TODO: Send email to user with confirmation token

        //            // Thank the user for registering and let them know an email is on its way
        //            return RedirectToAction("Thanks", "Account");
        //        }
        //        else
        //        {
        //            // Navigate back to the homepage and exit
        //            WebSecurityService.Login(model.UserName, model.Password);
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
        //    return View(model);
        //}
        
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (_webSecurityService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = _webSecurityService.MinPasswordLength;
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult Manage()
        {
            var result = Mapper.Map<Core.Models.User, UserManage>(_userLogic.Get(User.Identity.Name));

            return View(result);
        }

        [HttpPost]
        public ActionResult Manage(UserManage entity)
        {
            var result = new UserManage();

            if (ModelState.IsValid)
            {
                result = Mapper.Map<Core.Models.User, UserManage>(_userLogic.Update(Mapper.Map<UserManage, Core.Models.User>(entity)));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "There was a problem updating your account.");
            }

            return View(result);
        }

        public JsonResult RegenerateGuid()
        {
            var result = _userLogic.Get(User.Identity.Name);

            result.Guid = Guid.NewGuid();

            return new JsonResult{Data = "success"};
        }
    }
}
