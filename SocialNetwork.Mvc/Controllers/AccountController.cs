using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core;
using Ninject;
using Ninject.Web.Common;

namespace SocialNetwork.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IUserService userService, IService<Role> roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        private readonly IUserService userService;
        private readonly IService<Role> roleService;
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.LogOnViewModel model)
        {
            var ms = new Providers.CustomMembershipProvider(userService, roleService);
            if (ms.ValidateUser(model.EMail, model.Password))
                ViewBag.UserValidated = true;
            else
                ViewBag.UserValidated = false;
            return View();
        }
    }
}