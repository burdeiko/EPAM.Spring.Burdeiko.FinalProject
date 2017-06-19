using System.Web.Mvc;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Mvc.Models;
using System.Web.Security;
using SocialNetwork.Mvc.Providers;
using SocialNetwork.Mvc.Infrastructure;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using NLog;
using System;

namespace SocialNetwork.Mvc.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        #region Fields
        private readonly IUserService userService;
        private readonly IPersonService personService;
        private static readonly ILogger logger = System.Web.Mvc.DependencyResolver.Current.GetService<ILogger>();
        #endregion
        #region Constructor
        public FriendsController(IUserService userService, IPersonService personService)
        {
            this.userService = userService;
            this.personService = personService;
        }
        #endregion
        #region Action Methods
        // GET: Friends
        public ActionResult Index()
        {
            int id = -1;
            try
            {
                id = userService.GetUserByEMail(User.Identity.Name).Id;
            }
            catch (Exception e)
            {
                logger.Info("Unhandled exception");
                logger.Error(e.StackTrace);
                Response.StatusCode = 503;
            }
            return View(personService.GetFriends(id).Select(m => m.ToMvcPerson()));
        }

        public ActionResult IncomingRequests()
        {
            int id = -1;
            try
            {
                id = userService.GetUserByEMail(User.Identity.Name).Id;
            }
            catch (Exception e)
            {
                logger.Info("Unhandled exception");
                logger.Error(e.StackTrace);
                Response.StatusCode = 503;
            }
            return View("Index", personService.GetFriendRequestSenders(id).Select(m => m.ToMvcPerson()));
        }

        public ActionResult OutcomingRequests()
        {
            int id = -1;
            try
            {
                id = userService.GetUserByEMail(User.Identity.Name).Id;
            }
            catch (Exception e)
            {
                logger.Info("Unhandled exception");
                logger.Error(e.StackTrace);
                Response.StatusCode = 503;
            }
            return View("Index", personService.GetFriendRequestReceivers(id).Select(m => m.ToMvcPerson()));
        }

        public ActionResult SendFriendRequest(int? receiverId, string returnUrl)
        {
            try
            {
                int senderId = userService.GetUserByEMail(User.Identity.Name).Id;
                personService.SendFriendRequest(senderId, receiverId.Value);
            }
            catch (Exception e)
            {
                logger.Info("Unhandled exception");
                logger.Error(e.StackTrace);
                Response.StatusCode = 503;
            }
            if (returnUrl == null)
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }

        public ActionResult AcceptFriendRequest(int? senderId, string returnUrl)
        {
            try
            {
                int receiverId = userService.GetUserByEMail(User.Identity.Name).Id;
                personService.AcceptFriendRequest(senderId.Value, receiverId);
            }
            catch (Exception e)
            {
                logger.Info("Unhandled exception");
                logger.Error(e.StackTrace);
                Response.StatusCode = 503;
            }
            if (returnUrl == null)
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }
        #endregion
    }
}