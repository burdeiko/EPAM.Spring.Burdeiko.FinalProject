using System.Web.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Mvc.Models;
using System.Web.Security;
using SocialNetwork.Mvc.Providers;
using System;
using NLog;

namespace SocialNetwork.Mvc.Controllers
{
    public class AccountController : Controller
    {
        #region Constant
        private static readonly ILogger logger = System.Web.Mvc.DependencyResolver.Current.GetService<ILogger>();
        #endregion
        #region Constructor
        public AccountController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        #endregion
        #region Fields
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        #endregion
        #region Action Methods
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                try
                {
                    if (Membership.ValidateUser(viewModel.EMail, viewModel.Password))
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.EMail, viewModel.RememberMe);
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
                        ModelState.AddModelError("", "Incorrect login or password.");
                    }
                }
                catch (Exception e )
                {
                    logger.Info("Unhandled exception");
                    logger.Error(e.StackTrace);
                    Response.StatusCode = 503;
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            try
            {
                var userExists = userService.GetUserByEMail(viewModel.Email) != null;

                if (userExists)
                {
                    ModelState.AddModelError("", "User with this address already exists.");
                    return View(viewModel);
                }

                if (ModelState.IsValid)
                {
                    var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                        .CreateUser(viewModel.Email, viewModel.Password, viewModel.FirstName, viewModel.LastName);

                    if (membershipUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error occured while trying to registrate.");
                    }
                }
            }
            catch (Exception e)
            {
                logger.Info("Unhandled exception");
                logger.Error(e.StackTrace);
                Response.StatusCode = 503;
            }
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}