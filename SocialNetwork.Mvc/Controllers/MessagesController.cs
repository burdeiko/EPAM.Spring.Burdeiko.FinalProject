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
using System;

namespace SocialNetwork.Mvc.Controllers
{
    [Authorize]
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
            var hasDialoguesWithIds = messageService.GetTalkersIds(currentUserId);
            var personService = System.Web.Mvc.DependencyResolver.Current.GetService<IPersonService>();
            var hasDialoguesWith = hasDialoguesWithIds.Select(m => personService.GetById(m));
            var model = new List<DialogueViewModel>();
            foreach (var person in hasDialoguesWith)
            {
                model.Add(new DialogueViewModel {
                    WithPersonId = person.Id,
                    WithPersonName = person.FirstName + person.LastName,
                    Messages = messageService.GetDialogueWith(currentUserId, person.Id).Select(m => m.ToMvcMessage())
                });
            }
            return View(model);
        }

        public ActionResult Dialogue(int withId)
        {
            var dialogue = new DialogueViewModel { Messages = messageService.GetDialogueWith(currentUserId, withId).Select(m => m.ToMvcMessage()), WithPersonId = withId };
            return PartialView("DialoguePartial", dialogue);
        }

        [HttpPost]
        public ActionResult SendMessage(string message, int? toId)
        {
            if (!toId.HasValue || message == null)
                return null;
            var messageViewModel = messageService.SendMessage(currentUserId, toId.Value, message).ToMvcMessage();
            return PartialView("MessagePartial", messageViewModel);
        }

        public ActionResult GetLatestMessages(DateTime? fromDate, int? fromUserId)
        {
            if (fromDate.HasValue && fromUserId.HasValue)
            {
                var result = messageService.GetLatestMessages(fromDate.Value.Add(new TimeSpan(0,0,1)), fromUserId.Value, currentUserId).Select(m => m.ToMvcMessage());
                if (result.Count() == 0)
                    return null;
                return PartialView("NewMessagesPartial", result);
            }
            return null;
        }
    }
}