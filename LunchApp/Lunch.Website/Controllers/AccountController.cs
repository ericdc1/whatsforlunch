using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using Lunch.Core.Jobs;
using Lunch.Core.Logic;
using Lunch.Website.Services;
using Lunch.Website.ViewModels;
using RazorEngine;

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

        [HttpPost]
        public JsonResult RegenerateGuid()
        {
            var result = _userLogic.Get(User.Identity.Name);

            result.Guid = Guid.NewGuid();

            _userLogic.Update(result);

            return new JsonResult{Data = "success"};
        }

        //[HttpPost]
        //public JsonResult ResetPassword()
        //{
        //    var membershipUser = Membership.GetUser(User.Identity.Name);
        //    if (membershipUser == null)
        //        return new JsonResult { Data = "failure" };

        //    var tempPassword = membershipUser.ResetPassword();
        //    var newPassword = Membership.GeneratePassword(8, 0);
        //    membershipUser.ChangePassword(tempPassword, newPassword);

        //    var user = _userLogic.Get(membershipUser.UserName);

        //    var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");
        //    var baseurl = System.Configuration.ConfigurationManager.AppSettings.Get("BaseURL");
        //    var link = string.Format("{0}?GUID={1}", baseurl, user.Guid);
        //    var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        //    var template = System.IO.File.ReadAllText(new Uri(path + "/Views/_MailTemplates/ResetPassword.cshtml").AbsolutePath);
        //    var messagemodel = new MailDetails() { User = user,  Url = link, Password = newPassword };

        //    string result = Razor.Parse(template, messagemodel);
        //    Helpers.SendMail(user.Email, fromaddress, "Password reset", result);

        //    return new JsonResult { Data = "success" };
        //}

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(ForgotModel model)
        {
            bool isValid = false;
            string resetToken = "";

            if (ModelState.IsValid)
            {
                if (_webSecurityService.GetUserId(model.Email) > -1)
                {
                    resetToken = _webSecurityService.GeneratePasswordResetToken(model.Email);
                    isValid = true;
                }

                var user = _userLogic.Get(model.Email);

                if (isValid)
                {
                    string hostUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
                    string resetUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/PasswordReset?resetToken=" + HttpUtility.UrlEncode(resetToken));

                    var fromaddress = System.Configuration.ConfigurationManager.AppSettings.Get("FromEmail");
                    var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    var template = System.IO.File.ReadAllText(new Uri(path + "/Views/_MailTemplates/ResetPassword.cshtml").AbsolutePath);
                    var messagemodel = new MailDetails() { User = user, Url = resetUrl };

                    string result = Razor.Parse(template, messagemodel);
                    Helpers.SendMail(user.Email, fromaddress, "Password Reset", result);
                }
                return RedirectToAction("ForgotPasswordMessage");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordMessage()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PasswordReset(PasswordResetModel model)
        {
            if (ModelState.IsValid)
            {
                if (_webSecurityService.ResetPassword(model.ResetToken, model.NewPassword))
                {
                    return RedirectToAction("PasswordResetSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The password reset token is invalid.");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PasswordResetSuccess()
        {
            return View();
        }
    }
}
