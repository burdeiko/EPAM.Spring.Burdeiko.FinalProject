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

namespace SocialNetwork.Mvc.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        // GET: Friends
        public ActionResult Index()
        {
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            IUserService userService = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            int id = userService.GetUserByEMail(User.Identity.Name).Id;
            return View(personService.GetFriends(id).Select(m => m.ToMvcPerson()));
        }

        public ActionResult IncomingRequests()
        {
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            IUserService userService = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            int id = userService.GetUserByEMail(User.Identity.Name).Id;
            return View("Index", personService.GetFriendRequestSenders(id).Select(m => m.ToMvcPerson()));
        }

        public ActionResult SendFriendRequest(int? receiverId, string returnUrl)
        {
            IUserService userService = System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>();
            IPersonService personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            int senderId = userService.GetUserByEMail(User.Identity.Name).Id;
            personService.SendFriendRequest(senderId, receiverId.Value);
            if (returnUrl == null)
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }
    }
}