using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MeetupContestManager.Core.DataAccess;
using MeetupContestManager.Web.Models.Account;

namespace MeetupContestManager.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet, AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
                
            var user = _userRepository.GetUser(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "The username or password you provided were not found");

                return View(model);
            }

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (Url.IsLocalUrl(model.ReturnUrl))
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Admin");
        }

        public RedirectToRouteResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
