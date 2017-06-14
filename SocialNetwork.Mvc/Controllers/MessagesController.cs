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
    public class MessagesController : Controller
    {

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        private readonly IMessageService messageService;
        private int currentUserId
        {
            get
            {
                return System.Web.Mvc.DependencyResolver.Current.GetService<IUserService>().GetUserByEMail(User.Identity.Name).Id;
            }
        }
        // GET: Messages
        public ActionResult Index()
        {
            
            return View();
        }
    }
}